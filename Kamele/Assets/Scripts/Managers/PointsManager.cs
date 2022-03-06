using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public static PointsManager Instance;
    
    public int playerBudget;
    public int  points;

    private int startBudget = 1000;
    
    private Dictionary<ExplosivesTypes, int> prices = new Dictionary<ExplosivesTypes, int>
        { {ExplosivesTypes.WEAK, 100}, {ExplosivesTypes.MID, 350}, {ExplosivesTypes.STRONG, 800} };
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else Instance = this;
        
    }

    private void Start()
    {
        playerBudget = startBudget;
    }

    public void AddPoints(int value) => points += value;

    public void BuyTnt(ExplosivesTypes type)
    {
        if (!CanBuyTnt(type)) return;

        if (type == ExplosivesTypes.WEAK) Payment(prices[ExplosivesTypes.WEAK]);
        if (type == ExplosivesTypes.MID) Payment(prices[ExplosivesTypes.MID]);
        if (type == ExplosivesTypes.STRONG) Payment(prices[ExplosivesTypes.STRONG]);
        HUDManager.Instance.UpdateTMP();
    }
    
    public void Payment(int value) => playerBudget -= value;
    
    public int GetStartBudget() => startBudget;
    
    public bool CanBuyTnt(ExplosivesTypes type) =>  playerBudget - prices[type] >= 0;

}
