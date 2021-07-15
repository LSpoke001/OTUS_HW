using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCanvasController : MonoBehaviour
{
    [SerializeField] private GameController controller;
    enum Screen
    {
        Game,
        Pause,
        PlayerDead,
        EnemyDead,
    }

    private void Awake()
    {
        controller.PlayerDied += PlayerDead;
        controller.EnemyDied += EnemyDead;
    }

    public CanvasGroup gameScreen;
    public CanvasGroup pauseScreen;
    public CanvasGroup playerDeadScreen;
    public CanvasGroup enemyDeadScreen;

    void SetCurrentScreen(Screen screen)
    {
        Utility.SetCanvasGroupEnabled(gameScreen, screen == Screen.Game);
        Utility.SetCanvasGroupEnabled(pauseScreen, screen == Screen.Pause);
        Utility.SetCanvasGroupEnabled(playerDeadScreen, screen == Screen.PlayerDead);
        Utility.SetCanvasGroupEnabled(enemyDeadScreen, screen == Screen.EnemyDead);
    }
    
    void Start()
    {
        SetCurrentScreen(Screen.Game);
    }

    public void PauseGame()
    {
        SetCurrentScreen(Screen.Pause);
        Time.timeScale = 0f;
    }

    public void Continue()
    {
        SetCurrentScreen(Screen.Game);
        Time.timeScale = 1f;
    }

    public void Restart(string nameLevel)
    {
        SceneManager.LoadScene(nameLevel);
        Time.timeScale = 1f;
    }

    public void BackToMenu(string nameLevel)
    {
        SceneManager.LoadScene(nameLevel);
        Time.timeScale = 1f;
    }

    private void PlayerDead()
    {
        SetCurrentScreen(Screen.PlayerDead);
    }
    private void EnemyDead()
    {
        SetCurrentScreen(Screen.EnemyDead);
    }
}
