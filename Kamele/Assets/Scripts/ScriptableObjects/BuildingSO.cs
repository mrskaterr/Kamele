using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuilding", menuName = "Building", order = 0)]
public class BuildingSO : ScriptableObject
{
    public int pointsForDestruction;
    public GameObject explosionFX;
}
