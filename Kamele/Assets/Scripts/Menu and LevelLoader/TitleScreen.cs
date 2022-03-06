using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public void OnPlayButton()
    {
        LevelLoader.Instance.LoadScenes();
    }
    public void SettingsButton()
    {
        
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
