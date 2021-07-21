using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAttack : MonoBehaviour
{
    public event Action Attacking;
    public bool IsAttack = true;
    
    public void AttackButton()
    {
        if(IsAttack) Attacking();
        else
        {
            Debug.Log("Нельзя Аттаковать");
        }
    }
}
