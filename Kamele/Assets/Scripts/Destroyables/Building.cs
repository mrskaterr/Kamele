using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField]
    private BuildingSO buildingType;

    public int GetPoints() => buildingType.pointsForDestruction;
}
