using UnityEngine;
using Quaternion = System.Numerics.Quaternion;

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
        if (Input.GetKeyDown(KeyCode.E)) Explode();
    }

    private void SetPrefab()
    {
        gameObject.GetComponent<Renderer>().sharedMaterial = tntSO.material;
        _radius = tntSO.radius;
        _explForce = tntSO.explosionForce;
    }

    private void Explode()
    {
        var colliders = Physics.OverlapSphere(pos, _radius, destroyable);
        Destroy(gameObject);

        foreach (var collider in colliders)
        {
            if (collider == null) return;
            var building = collider.GetComponent<Building>();
            var pos = building.GetPosition();
            var rot = building.GetRotation();
            Destroy(collider.gameObject);
            Instantiate(GameManager.Instance.GetBuildingPrefab(building.GetBuildingType()), pos, rot);
        }
        var colliders2 = Physics.OverlapSphere(pos, _radius, destroyable);

        #region points system

        /*foreach (var collider in colliders)
        {
            if (collider == null) return;
            if (collider.GetComponent<Building>())
            {
                Debug.Log(collider.name);
                GameManager.Instance.HitBuilding(collider);
            }
        }*/

        #endregion

        #region explosion system

        foreach (var collider in colliders2)
        {
            if (collider == null) return;
            if (collider.GetComponent<MeshCollider>())
            {
                collider.GetComponent<MeshCollider>().isTrigger = false;
            }
            if (!collider.GetComponent<Rigidbody>()) collider.gameObject.AddComponent<Rigidbody>();
            collider.GetComponent<Rigidbody>().AddExplosionForce(_explForce, pos, _radius);
        }

        #endregion
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pos, _radius);
    }
}