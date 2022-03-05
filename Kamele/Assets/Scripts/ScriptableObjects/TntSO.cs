using System;
using Consts;
using UnityEngine;

[CreateAssetMenu(fileName = "NewExplosive", menuName = "Explosive", order = 0)]
public class TntSO : ScriptableObject
{
    [Header("Params of tnt")] 
    public float radius;
    public float explosionForce;

    [Header("GFX")] 
    public Material material;

    [Header("Explosion strength")] 
    public ExplosivesTypes explosiveType;
}
