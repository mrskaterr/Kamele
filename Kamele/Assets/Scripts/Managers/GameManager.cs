using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<GameObject> explosivePrefabs = new List<GameObject>();

    private ExplosivesTypes currentType;
    
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
    }

    private void Start()
    {
        currentType = ExplosivesTypes.WEAK;
        SetDict(5, 0, 0);
    }

    private void Update()
    {
        Debug.Log(currentType);
    }

    private void SetDict(int weakQuantity, int midQuantity, int strongQuantity)
    {
        explosivesQuantity[ExplosivesTypes.WEAK] = weakQuantity;
        explosivesQuantity[ExplosivesTypes.MID] = midQuantity;
        explosivesQuantity[ExplosivesTypes.STRONG] = strongQuantity;
    }

    public void DecreaseQuantity(ExplosivesTypes explosiveType)
    {
        explosivesQuantity[explosiveType]--;
    }
    public int GetQuantity() => explosivesQuantity[currentType];

    public void SetCurrentTypeToWeak() => currentType = ExplosivesTypes.WEAK;
    public void SetCurrentTypeToMid() => currentType = ExplosivesTypes.MID;
    public void SetCurrentTypeToStrong() => currentType = ExplosivesTypes.STRONG;

}
