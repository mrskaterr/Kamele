using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    #region structs

    [Serializable]
    public struct ExplosivePrefab
        {
            public ExplosivesTypes type;
            public GameObject prefab;
        }
        
        [Serializable]
        public struct DestroyableBuilding
        {
            public Buildings type;
            public GameObject prefab;
        }

    #endregion
    
    #region Lists and dictionaries

    [SerializeField]
        private List<ExplosivePrefab> explosivePrefabsList = new List<ExplosivePrefab>();
        [SerializeField]
        private List<DestroyableBuilding> buildingPrefabsList = new List<DestroyableBuilding>();
 
        private Dictionary<ExplosivesTypes, GameObject> explosivesPrefabs = new Dictionary<ExplosivesTypes, GameObject>();
        
        private Dictionary<Buildings, GameObject> buildingPrefabs = new Dictionary<Buildings, GameObject>();
        
        private Dictionary<ExplosivesTypes, int> explosivesQuantity = new Dictionary<ExplosivesTypes, int>
            { {ExplosivesTypes.WEAK, 0}, {ExplosivesTypes.MID, 0}, {ExplosivesTypes.STRONG, 0} };

    #endregion
    
       
    private ExplosivesTypes currentExplosiveType;
    private GameObject currentExplosivePrefab;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        
        SetQuantityDict(5, 1, 1);
        SetPrefabDict();
    }

    private void Start()
    {
        currentExplosiveType = ExplosivesTypes.WEAK;
        currentExplosivePrefab = explosivesPrefabs[currentExplosiveType];
    }

    private void Update()
    {
        Debug.Log(currentExplosiveType);
    }

    private void SetQuantityDict(int weakQuantity, int midQuantity, int strongQuantity)
    {
        explosivesQuantity[ExplosivesTypes.WEAK] = weakQuantity;
        explosivesQuantity[ExplosivesTypes.MID] = midQuantity;
        explosivesQuantity[ExplosivesTypes.STRONG] = strongQuantity;
    }

    private void SetPrefabDict()
    {
        foreach (var element in explosivePrefabsList)
        {
            explosivesPrefabs.Add(element.type, element.prefab);
        }

        foreach (var element in buildingPrefabsList)
        {
            buildingPrefabs.Add(element.type, element.prefab);
        }
    }

    public void DecreaseQuantity() => explosivesQuantity[currentExplosiveType]--;
    public int GetQuantity(ExplosivesTypes type) => explosivesQuantity[type];
    public int GetQuantity() => explosivesQuantity[currentExplosiveType];
    public GameObject GetCurrentPrefab() => currentExplosivePrefab;
    public GameObject GetBuildingPrefab(Buildings type) => buildingPrefabs[type];
    public void SetCurrentTypeToWeak()
    {
        currentExplosiveType = ExplosivesTypes.WEAK;
        currentExplosivePrefab = explosivesPrefabs[currentExplosiveType];
    }
    public void SetCurrentTypeToMid()
    {
        currentExplosiveType = ExplosivesTypes.MID;
        currentExplosivePrefab = explosivesPrefabs[currentExplosiveType];
    }
    public void SetCurrentTypeToStrong()
    {
        currentExplosiveType = ExplosivesTypes.STRONG;
        currentExplosivePrefab = explosivesPrefabs[currentExplosiveType];
    }
    public void HitBuilding(Collider collider)
    {
        PointsManager.Instance.AddPoints(collider.GetComponent<Building>().GetPoints());
        HUDManager.Instance.UpdatePointsTMP(PointsManager.Instance.GetPoints());
        collider.GetComponent<Building>().SetDestroyedState();
    }
}
