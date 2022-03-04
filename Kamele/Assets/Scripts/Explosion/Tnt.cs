using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
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
    private float _explForce;
    private float _radius;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _material = GetComponent<Material>();
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
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius, destroyable);
        Destroy(gameObject);
        foreach (var collider in colliders)
        {
            if (collider != null)
            {
                collider.GetComponent<Rigidbody>().AddExplosionForce(_explForce, transform.position, _radius);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
