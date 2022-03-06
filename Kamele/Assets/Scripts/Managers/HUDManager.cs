using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

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
    private TextMeshProUGUI pointsSystemTMP;
    [SerializeField] 
    private TextMeshProUGUI gameStateTMP;
    
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
        UpdatePointsTMP(PointsManager.Instance.GetStartBudget());
        UpdateGameStateTMP();
    }

    public void UpdateTMP()
    {
        UpdateWeakTMP(GameManager.Instance.GetQuantity(ExplosivesTypes.WEAK));
        UpdateMidTMP(GameManager.Instance.GetQuantity(ExplosivesTypes.MID));
        UpdateStrongTMP(GameManager.Instance.GetQuantity(ExplosivesTypes.STRONG));
        UpdatePointsTMP(PointsManager.Instance.playerBudget);
    }
    public void UpdatePointsTMP(int value)
    {
        if(GameManager.Instance.GetGameSate() == States.BUYING_PHASE) pointsSystemTMP.text = "BUDGET $" + value;
        if(GameManager.Instance.GetGameSate() == States.GAMEPLAY) pointsSystemTMP.text = "Points: " + value;
    }
    public void UpdateGameStateTMP()
    {
        if (GameManager.Instance.GetGameSate() == States.BUYING_PHASE)
        {
            gameStateTMP.color = Color.blue;
            gameStateTMP.text = "Game in buying phase";
        }
        
        if (GameManager.Instance.GetGameSate() == States.PLANNING_PHASE)
        {
            gameStateTMP.color = Color.yellow;
            gameStateTMP.text = "Game in planning phase";
        }

        if (GameManager.Instance.GetGameSate() == States.GAMEPLAY)
        {
            gameStateTMP.color = Color.red;
            gameStateTMP.text = "Game in progress";
        }
    }
    
    private void UpdateWeakTMP(int value) => weakTMP.text = value.ToString();
    
    private void UpdateMidTMP(int value) => midTMP.text = value.ToString();
    
    private void UpdateStrongTMP(int value) => strongTMP.text = value.ToString();
    
   
}
