using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Serializable]
    public struct ExplosivePrefab
    {
        public ExplosivesTypes type;
        public GameObject prefab;
    }
    
    [SerializeField]
    private List<ExplosivePrefab> explosivePrefabsList = new List<ExplosivePrefab>();

    private ExplosivesTypes currentType;
    private GameObject currentPrefab;

    private Dictionary<ExplosivesTypes, GameObject> explosivesPrefabs = new Dictionary<ExplosivesTypes, GameObject>();
    private Dictionary<ExplosivesTypes, int> explosivesQuantity = new Dictionary<ExplosivesTypes, int>
        { {ExplosivesTypes.WEAK, 0}, {ExplosivesTypes.MID, 0}, {ExplosivesTypes.STRONG, 0} };
    
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
        currentType = ExplosivesTypes.WEAK;
        currentPrefab = explosivesPrefabs[currentType];
    }

    private void Update()
    {
        Debug.Log(currentType);
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
    }

    public void DecreaseQuantity() => explosivesQuantity[currentType]--;
    public int GetQuantity(ExplosivesTypes type) => explosivesQuantity[type];
    public int GetQuantity() => explosivesQuantity[currentType];
    public void SetCurrentTypeToWeak()
    {
        currentType = ExplosivesTypes.WEAK;
        currentPrefab = explosivesPrefabs[currentType];
    }
    public void SetCurrentTypeToMid()
    {
        currentType = ExplosivesTypes.MID;
        currentPrefab = explosivesPrefabs[currentType];
    }
    public void SetCurrentTypeToStrong()
    {
        currentType = ExplosivesTypes.STRONG;
        currentPrefab = explosivesPrefabs[currentType];
    }

    public GameObject GetCurrentPrefab()
    {
        return currentPrefab;
    }
}
