using System;
using System.Collections;
using System.Collections.Generic;
using Consts;
using UnityEngine;

public class Tnt : MonoBehaviour
{
    [Header("Which layers to destroy")]
    [SerializeField] 
    private LayerMask destroyable;

    [SerializeField] 
    private TntSO tntSO;
    
    private Rigidbody _rb;
    private Material _material;
    private Vector3 pos;
    private ExplosivesTypes _explosiveType;
    private float _explForce;
    private float _radius;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _material = GetComponent<Material>();
        pos = GetComponent<Transform>().position;
    }

    private void Start()
    {
        SetPrefab();
    }

    private void Update()
    {
        //TODO: MAKE INPUT HANDLER AND SWITCH TO EVENTS
        if (GameManager.Instance.GetQuantity(_explosiveType) <= 0) return;
        if (Input.GetKeyDown(KeyCode.E)) Explode();

        GameManager.Instance.GetQuantity(_explosiveType);
    }

    private void SetPrefab()
    {
        _radius = tntSO.radius;
        _material = tntSO.material;
        _explForce = tntSO.explosionForce;
        _explosiveType = tntSO.explosiveType;
    }

    private void Explode()
    {
        GameManager.Instance.ChangeQuantity(_explosiveType);
        Collider[] colliders = Physics.OverlapSphere(pos, _radius, destroyable);
        Debug.Log(colliders.Length);
        
        Destroy(gameObject);
        
        foreach (var collider in colliders)
        {
            if (collider == null) return;

            var points = collider.GetComponent<Building>().GetPoints();
            PointsManager.Instance.AddPoints(points);
            collider.GetComponent<Rigidbody>().AddExplosionForce(_explForce, pos, _radius);
        }
        
        Debug.Log(PointsManager.Instance.GetPoints());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pos, _radius);
    }
}
