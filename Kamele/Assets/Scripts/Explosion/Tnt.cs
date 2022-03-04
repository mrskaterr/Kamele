using System;
using System.Collections;
using System.Collections.Generic;
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
        if (Input.GetKeyDown(KeyCode.E)) Explode();
    }

    private void SetPrefab()
    {
        _radius = tntSO.radius;
        _material = tntSO.material;
        _explForce = tntSO.explosionForce;
    }

    private void Explode()
    {
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
