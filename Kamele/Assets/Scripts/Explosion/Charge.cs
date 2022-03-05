using UnityEngine;

public class Charge : MonoBehaviour
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

        #region points system

        foreach (var collider in colliders)
        {
            if (collider == null) return;
            if (collider.GetComponent<Building>())
            {
                Debug.Log(collider.name);
                PointsManager.Instance.AddPoints(collider.GetComponent<Building>().GetPoints());
                HUDManager.Instance.UpdatePointsTMP(PointsManager.Instance.GetPoints());
                collider.GetComponent<Building>().SetDestroyedState();
            }
        }

        #endregion

        #region explosion system

        foreach (var collider in colliders)
        {
            if (collider == null) return;

            if (!collider.GetComponent<Rigidbody>()) collider.gameObject.AddComponent<Rigidbody>();

            collider.GetComponent<Rigidbody>().AddExplosionForce(_explForce, pos, .1f);
        }

        #endregion
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pos, _radius);
    }
}