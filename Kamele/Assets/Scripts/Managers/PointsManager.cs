using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public static PointsManager Instance;

    private int _points;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else Instance = this;
        
    }

    public void AddPoints(int value)
    {
        _points += value;
    }

    public int GetPoints()
    {
        return _points;
    }
}
