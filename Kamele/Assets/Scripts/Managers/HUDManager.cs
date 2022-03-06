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

    [SerializeField] 
    private TextMeshProUGUI pointsTMP;
    
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
        UpdateTMP();
        UpdatePointsTMP(0);
    }

    public void UpdateTMP()
    {
        UpdateWeakTMP(GameManager.Instance.GetQuantity(ExplosivesTypes.WEAK));
        UpdateMidTMP(GameManager.Instance.GetQuantity(ExplosivesTypes.MID));
        UpdateStrongTMP(GameManager.Instance.GetQuantity(ExplosivesTypes.STRONG));
    }
    public void UpdatePointsTMP(int value)
    {
        pointsTMP.text = "Points: " + value;
    }
    
    private void UpdateWeakTMP(int value)
    {
        weakTMP.text = value.ToString();
    }
    private void UpdateMidTMP(int value)
    {
        midTMP.text = value.ToString();
    }
    private void UpdateStrongTMP(int value)
    {
        strongTMP.text = value.ToString();
    }
   
}
