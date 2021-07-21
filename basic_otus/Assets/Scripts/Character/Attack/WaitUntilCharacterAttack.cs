using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitUntilCharacterAttack : CustomYieldInstruction
{
    private bool isKeepWaiting = true;

    public WaitUntilCharacterAttack(ButtonAttack button)
    {
        Action action = null;
        action = () =>
        {
            isKeepWaiting = false;
            button.Attacking -= action;
        };
        button.Attacking += action;
    }
    
    public override bool keepWaiting { get { return isKeepWaiting; } }
}
