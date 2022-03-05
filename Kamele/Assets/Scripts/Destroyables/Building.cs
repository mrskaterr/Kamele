using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField]
    private BuildingSO buildingType;

    private bool isDestroyed;

    public int GetPoints()
    {
        if (!isDestroyed) return buildingType.pointsForDestruction;
        
        return 0;
    }

    public void SetDestroyedState()
    {
        isDestroyed = true;
    }
}
