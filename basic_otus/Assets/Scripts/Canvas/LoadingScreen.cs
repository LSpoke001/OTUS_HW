using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public Image progressBar;
    private CanvasGroup canvasGroup;
    public static LoadingScreen instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
            return;
        }
        instance = this;
        //DontDestroyOnLoad(gameObject);
        canvasGroup = GetComponentInChildren<CanvasGroup>();
        Utility.SetCanvasGroupEnabled(canvasGroup, false);
    }

    IEnumerator Coroutine(int numLevel)
    {
        Utility.SetCanvasGroupEnabled(canvasGroup, true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(numLevel);
        while (!operation.isDone)
        {
            progressBar.fillAmount = operation.progress;
            yield return null;
        }
        Utility.SetCanvasGroupEnabled(canvasGroup, false);
    }

    public void LoadScene(int numLevel)
    {
        StartCoroutine(Coroutine(numLevel));
    }
}
