using System;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;
    
    [Header("Quantity txts")]
    [SerializeField]
    private TextMeshProUGUI weakTMP; 
    [SerializeField]
    private TextMeshProUGUI midTMP;
    [SerializeField]
    private TextMeshProUGUI strongTMP;

    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else Instance = this;
    }

    public void UpdateWeakTMP(int value)
    {
        weakTMP.text = value.ToString();
    }
    
    public void UpdateMidTMP(int value)
    {
        midTMP.text = value.ToString();
    }
    
    public void UpdateStrongTMP(int value)
    {
        strongTMP.text = value.ToString();
    }
}
