using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int idCounter=0;
    public static GameManager Instance;

    [Serializable]
    public struct ExplosivePrefab
    {
        public ExplosivesTypes type;
        public GameObject prefab;
    }

    #region lists and dictionaries

        [SerializeField]
        private List<ExplosivePrefab> explosivePrefabsList = new List<ExplosivePrefab>();

        private Dictionary<ExplosivesTypes, GameObject> explosivesPrefabs = new Dictionary<ExplosivesTypes, GameObject>();
        private Dictionary<ExplosivesTypes, int> explosivesQuantity = new Dictionary<ExplosivesTypes, int>
            { {ExplosivesTypes.WEAK, 0}, {ExplosivesTypes.MID, 0}, {ExplosivesTypes.STRONG, 0} };

    #endregion

    
    private ExplosivesTypes currentExplosiveType;
    private GameObject currentExplosivePrefab;
    private States currentGameState;

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
        
        SetQuantityDict(0, 0, 0);
        SetPrefabDict();
    }

    private void Start()
    {
        currentExplosiveType = ExplosivesTypes.WEAK;
        currentExplosivePrefab = explosivesPrefabs[currentExplosiveType];
        currentGameState = States.BUYING_PHASE;
    }
    
    #region setters

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

    #endregion

    #region quantity management

    public void DecreaseQuantity()
    {
        idCounter++;
        explosivesQuantity[currentExplosiveType]--;
    }
        public int GetQuantity(ExplosivesTypes type) => explosivesQuantity[type];
        public int GetQuantity() => explosivesQuantity[currentExplosiveType];

    #endregion

    #region explosiveType management

    public void WeakButtonHandler()
    {
        ButtonHandlerInPlanningPhase(ExplosivesTypes.WEAK);
        ButtonHandlerInBuyingPhase(ExplosivesTypes.WEAK);
    }

    public void MidButtonHandler()
    {
        ButtonHandlerInPlanningPhase(ExplosivesTypes.MID);
        ButtonHandlerInBuyingPhase(ExplosivesTypes.MID);
    }

    public void StrongButtonHandler()
    {
        ButtonHandlerInPlanningPhase(ExplosivesTypes.STRONG);
        ButtonHandlerInBuyingPhase(ExplosivesTypes.STRONG);
    }

    private void ButtonHandlerInPlanningPhase(ExplosivesTypes type)
    {
        if (currentGameState == States.PLANNING_PHASE)
        {
            Debug.Log("PLANNING");
            currentExplosiveType = type;
            currentExplosivePrefab = explosivesPrefabs[currentExplosiveType];
        }
    }

    private void ButtonHandlerInBuyingPhase(ExplosivesTypes type)
    {
        if (currentGameState == States.BUYING_PHASE)
        {
            Debug.Log("BUYING");
            if (!PointsManager.Instance.CanBuyTnt(type)) return;

            explosivesQuantity[type]++;
            Debug.Log(explosivesQuantity[type]);
            PointsManager.Instance.BuyTnt(type);
        }
    }
    
    public GameObject GetCurrentPrefab()
    {
        return currentExplosivePrefab;
    }

    #endregion

    #region buildings management

    public void HitBuilding(Collider collider)
    {
        if (collider.gameObject.GetComponent<BuildingsHealthSystem>()?.GetHP() <= 0)
        {
            PointsManager.Instance.AddPoints(collider.GetComponent<Building>().GetPoints());
            HUDManager.Instance.UpdatePointsTMP(PointsManager.Instance.points);
            collider.GetComponent<Building>().SetDestroyedState();
        }
    }

    #endregion

    #region GameState management

    private void SetGameState(States state)
    {
        currentGameState = state;
    }
    public bool CheckIfTntCanBePlaced() => (GetQuantity() > 0 && GetGameSate() == States.PLANNING_PHASE);
    
    public void OnPlayButton()
    {
        SetGameState(States.GAMEPLAY);
        HUDManager.Instance.UpdateGameStateTMP();
    }

    public void OnPlanButton()
    {
        SetGameState(States.PLANNING_PHASE);
        HUDManager.Instance.UpdateGameStateTMP();
    }

    public void ResetGameState()
    {
        SetGameState(States.PLANNING_PHASE);
    }
    public States GetGameSate() => currentGameState;
    
    #endregion
}
