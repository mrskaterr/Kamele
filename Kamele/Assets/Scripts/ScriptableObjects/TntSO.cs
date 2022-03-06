using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewExplosive", menuName = "Explosive", order = 0)]
public class TntSO : ScriptableObject
{
    [Header("Params of tnt")] 
    public float radius;
    public float explosionForce;

    [Header("GFX")]
    public GameObject explosionFX;

    [Header("Explosion strength")] 
    public ExplosivesTypes explosiveType;
}
