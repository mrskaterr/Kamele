using System;
using System.Collections;
using UnityEngine;

public class Tnt : MonoBehaviour
{
    [Header("Which layers to destroy")]
    [SerializeField]
    private LayerMask destroyable;
    private int id;

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
        id=GameManager.Instance.idCounter;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Wait(id));
        }
    }
    IEnumerator Wait(float Second)
    {
        yield return new WaitForSeconds(Second);
        Explode();
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
        
        foreach (var collider in colliders)
        {
            if (collider?.GetComponent<Building>())
            {
                Debug.Log(collider.name);
                GameManager.Instance.HitBuilding(collider);
            }
        }
        foreach (var collider in colliders)
        {
            collider.GetComponent<Bot>()?.RagdollActivate();
            if (!collider.GetComponent<Rigidbody>()) collider.gameObject.AddComponent<Rigidbody>();
            collider.GetComponent<Rigidbody>().AddExplosionForce(_explForce, pos, _radius);
        }
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pos, _radius);
    }
}