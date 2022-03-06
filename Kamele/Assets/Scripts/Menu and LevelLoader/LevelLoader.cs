using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance;
        
    [SerializeField] 
    GameObject loadingScreen;
    [SerializeField]
    ProgressBar progressBar;
        

    float totalSceneProgress;
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    private void Awake()
    {
        Instance = this;

        SceneManager.LoadSceneAsync(SceneNames.TITLE_SCREEN, LoadSceneMode.Additive);
    }
        
    public void LoadScenes()
    {
        loadingScreen.gameObject.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync(SceneNames.TITLE_SCREEN));
        scenesLoading.Add(SceneManager.LoadSceneAsync(SceneNames.GAMEPLAY_SCENE, LoadSceneMode.Additive));
        StartCoroutine(GetSceneLoadProgress());
    }

    public IEnumerator GetSceneLoadProgress()
    {
        for(int i = 0; i<scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                totalSceneProgress = 0;
                foreach (var operation in scenesLoading)
                {
                    totalSceneProgress += operation.progress;
                }

                progressBar.current = Mathf.RoundToInt((totalSceneProgress / scenesLoading.Count) * 100f);
                yield return null;
            }
        }

        loadingScreen.gameObject.SetActive(false);
    }
}
