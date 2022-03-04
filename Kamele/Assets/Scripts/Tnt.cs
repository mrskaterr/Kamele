using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tnt : MonoBehaviour
{
    [SerializeField] 
    private float radius;
    [SerializeField] 
    private float explForce;
    [SerializeField] 
    private LayerMask destroyable;
    
    
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Explode();
        }
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, destroyable);
        Destroy(gameObject);
        foreach (var collider in colliders)
        {
            if (collider != null)
            {
                collider.GetComponent<Rigidbody>().AddExplosionForce(explForce, transform.position, radius);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
