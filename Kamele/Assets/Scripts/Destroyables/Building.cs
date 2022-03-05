using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField]
    private BuildingSO buildingSO;

    private bool isDestroyed;

    public int GetPoints()
    {
        if (!isDestroyed) return buildingSO.pointsForDestruction;
        
        return 0;
    }
    public void SetDestroyedState() => isDestroyed = true;
    public Vector3 GetPosition() => transform.position;
    public Quaternion GetRotation() => transform.rotation;
    public Buildings GetBuildingType() => buildingSO.buildingType;
}
