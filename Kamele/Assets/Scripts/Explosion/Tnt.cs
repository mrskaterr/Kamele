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
    private int id;

    private void Awake()
    {
        pos = GetComponent<Transform>().position;
    }

    private void Start()
    {
        SetPrefab();
        id = GameManager.Instance.idCounter;
    }

    private void Update()
    {
        if (GameManager.Instance.GetGameSate() == States.GAMEPLAY)
        {
            StartCoroutine(Explode(id));
        }
    }

    private void SetPrefab()
    {
        _radius = tntSO.radius;
        _explForce = tntSO.explosionForce;
    }

    public IEnumerator Explode(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Debug.Log("explosion");
        var colliders = Physics.OverlapSphere(pos, _radius, destroyable);

        #region points system

         foreach (var collider in colliders)
                {
                    if (collider?.GetComponent<Building>())
                    {
                        PointsManager.Instance.AddPoints(collider.GetComponent<Building>().GetPoints());
                        HUDManager.Instance.UpdatePointsTMP(PointsManager.Instance.points);
                        collider.GetComponent<Building>().SetDestroyedState();
                    }
                }

        #endregion

        #region explosion system

                foreach (var collider in colliders)
                {
                    collider.GetComponent<Bot>()?.RagdollActivate();
                    if (!collider.GetComponent<Rigidbody>()) collider.gameObject.AddComponent<Rigidbody>();
                    collider.GetComponent<Rigidbody>().AddExplosionForce(_explForce, pos, _radius);
                }
                Destroy(gameObject);

        #endregion
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pos, _radius);
    }
}