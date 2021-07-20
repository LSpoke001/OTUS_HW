using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    enum Screen
    {
        None,
        Main,
        Settings,
        Level,
    }

    public CanvasGroup mainScreen;
    public CanvasGroup settingsScreen;
    public CanvasGroup levelScreen;
    
    public Button[] button;

    void SetCurrentScreen(Screen screen)
    {
        Utility.SetCanvasGroupEnabled(mainScreen, screen == Screen.Main);
        Utility.SetCanvasGroupEnabled(settingsScreen, screen == Screen.Settings);
        Utility.SetCanvasGroupEnabled(levelScreen, screen == Screen.Level);
    }
    
    void Start()
    {
        SetCurrentScreen(Screen.Main);
        for (int i = 0; i < button.Length; i++)
        {
            int numLevel = i+1;
            button[i].onClick.AddListener(()=>ClickButton(numLevel));
        }
    }

    public void StartGame()
    {
        SetCurrentScreen(Screen.Level);
    }

    public void OpenSettings()
    {
        SetCurrentScreen(Screen.Settings);
    }

    public void CloseSettings()
    {
        SetCurrentScreen(Screen.Main);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void ClickButton(int numLevel)
    {
        SetCurrentScreen(Screen.None);
        LoadingScreen.instance.LoadScene(numLevel);
    }
}
