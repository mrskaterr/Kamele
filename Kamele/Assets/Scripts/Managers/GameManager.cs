using System;
using System.Collections;
using System.Collections.Generic;
using Consts;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
        SetDict(5, 0, 0);
    }

    private void SetDict(int weakQuantity, int midQuantity, int strongQuantity)
    {
        explosivesQuantity[ExplosivesTypes.WEAK] = weakQuantity;
        explosivesQuantity[ExplosivesTypes.MID] = midQuantity;
        explosivesQuantity[ExplosivesTypes.STRONG] = strongQuantity;
    }

    public void ChangeQuantity(ExplosivesTypes explosiveType)
    {
        explosivesQuantity[explosiveType]--;
    }
    public int GetQuantity(ExplosivesTypes explosiveType) => explosivesQuantity[explosiveType];
    
}
