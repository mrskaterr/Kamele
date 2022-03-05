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
        if (Input.GetKeyDown(KeyCode.E)) Explode();
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
        var colliders = Physics.OverlapSphere(pos, _radius, destroyable);

        Destroy(gameObject);

        #region points system

        foreach (var collider in colliders)
        {
            if (collider == null) return;
            if (collider.GetComponent<Building>())
            {
                Debug.Log(collider.name);
                PointsManager.Instance.AddPoints(collider.GetComponent<Building>().GetPoints());
                collider.GetComponent<Building>().SetDestroyedState();
            }
        }

        #endregion

        #region explosion system

        foreach (var collider in colliders)
        {
            if (collider == null) return;
            collider.GetComponent<Bot>()?.RagdollActivate();
            
            if (!collider.GetComponent<Rigidbody>()) collider.gameObject.AddComponent<Rigidbody>();

            collider.GetComponent<Rigidbody>().AddExplosionForce(_explForce, pos, _radius);
        }

        #endregion

        Debug.Log(PointsManager.Instance.GetPoints());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pos, _radius);
    }
}