using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int idCounter=0;
    public static GameManager Instance;
    
    public event Action OnPlayButtonPressed;

    [Serializable]
    public struct ExplosivePrefab
    {
        public ExplosivesTypes type;
        public GameObject prefab;
    }

    #region lists and dictionaries

        [SerializeField]
        private List<ExplosivePrefab> explosivePrefabsList = new List<ExplosivePrefab>();
        public List<GameObject> explosivesQueue = new List<GameObject>();
        
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
        
        SetQuantityDict(5, 1, 1);
        SetPrefabDict();
    }

    private void Start()
    {
        currentExplosiveType = ExplosivesTypes.WEAK;
        currentExplosivePrefab = explosivesPrefabs[currentExplosiveType];
        currentGameState = States.PLANNING_PHASE;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            explosivesQueue[0].GetComponent<Tnt>().Explode();
            
        }
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
        AddExplosiveToQue(currentExplosivePrefab);
    }
        public int GetQuantity(ExplosivesTypes type) => explosivesQuantity[type];
        public int GetQuantity() => explosivesQuantity[currentExplosiveType];

    #endregion

    #region explosiveType management

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
        public GameObject GetCurrentPrefab()
        {
            return currentExplosivePrefab;
        }

    #endregion

    #region buildings management

    public void HitBuilding(Collider collider)
    {
        if (collider.gameObject.GetComponent<BuildingsHealthSystem>()?.GetHP() > 0)
        {
            PointsManager.Instance.AddPoints(collider.GetComponent<Building>().GetPoints());
            HUDManager.Instance.UpdatePointsTMP(PointsManager.Instance.GetPoints());
            collider.GetComponent<Building>().SetDestroyedState();
        }
    }

    #endregion

    #region GameState management

    private void SetGameState(States state)
    {
        currentGameState = state;
    }
    public void AddExplosiveToQue(GameObject obj)
    {
        explosivesQueue.Add(obj);
    }

    public bool CheckIfTntCanBePlaced() => (GetQuantity() > 0 && GetGameSate() == States.PLANNING_PHASE);
    
    public async void OnPlayButton()
    {
        foreach (GameObject element in explosivesQueue)
        {
            element.GetComponent<Tnt>().Explode();

        }
    }


    public List<GameObject> GetExplosivesQue() => explosivesQueue;
    public States GetGameSate() => currentGameState;
    #endregion
}
