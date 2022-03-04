using System;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewExplosive", menuName = "Explosives", order = 0)]
    public class TntSO : ScriptableObject
    {
        [Header("Params of tnt")]
        public float radius;

        [SerializeField]
        public float explosionForce;
        
        [Header("GFX")]
        public Material material;
        
    }
}