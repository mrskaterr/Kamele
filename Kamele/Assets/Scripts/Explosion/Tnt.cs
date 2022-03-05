using System;
using System.Collections;
using UnityEngine;

public class Tnt : MonoBehaviour
{
    [Header("Which layers to destroy")]
    [SerializeField]
    private LayerMask destroyable;

    [SerializeField] 
    private TntSO tntSO;
    
    private Vector3 pos;
    private float _explForce;
    private float _radius;

    private void Awake()
    {
        pos = GetComponent<Transform>().position;
    }

    private void Start()
    {
        SetPrefab();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Explode();
        }
    }

    private void SetPrefab()
    {
        gameObject.GetComponent<Renderer>().sharedMaterial = tntSO.material;
        _radius = tntSO.radius;
        _explForce = tntSO.explosionForce;
    }

    public void Explode()
    {
        Debug.Log("explosion");
        var colliders = Physics.OverlapSphere(pos, _radius, destroyable);

       // Destroy(gameObject);

        #region points system

        foreach (var collider in colliders)
        {
            if (collider.GetComponent<Building>())
            {
                Debug.Log(collider.name);
                GameManager.Instance.HitBuilding(collider);
            }
        }

        #endregion

        Debug.Log("huj");
        #region explosion system

        foreach (var collider in colliders)
        {
<<<<<<< HEAD
            if (collider == null) return;
            collider.GetComponent<Bot>()?.RagdollActivate();
            
=======
>>>>>>> wojtasxd
            if (!collider.GetComponent<Rigidbody>()) collider.gameObject.AddComponent<Rigidbody>();

            collider.GetComponent<Rigidbody>().AddExplosionForce(_explForce, pos, _radius);
        }

        #endregion
        Debug.Log("cipa");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pos, _radius);
    }
}