using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
   public Button[] button;

   private void Start()
   {
      for (int i = 0; i < button.Length; i++)
      {
         int numLevel = i+1;
         button[i].onClick.AddListener(()=>ClickButton(numLevel));
      }
   }

   public void ClickButton(int numLevel)
   {
      SceneManager.LoadScene(numLevel);
   }
}
