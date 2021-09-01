using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void RestartBtn()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
