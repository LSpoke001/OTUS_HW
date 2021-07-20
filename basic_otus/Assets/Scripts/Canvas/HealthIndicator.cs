using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Character.Health;
using UnityEngine;
using UnityEngine.UI;

public class HealthIndicator : MonoBehaviour
{
    private Text text;
    private HealthComponent healthComponent;

    private void Start()
    {
        text = GetComponent<Text>();
        healthComponent = GetComponentInParent<HealthComponent>();
        healthComponent.OnHealthChanged += HealthIndicatorUpdate;
        HealthIndicatorUpdate(healthComponent.Health);
    }

    private void HealthIndicatorUpdate(int value)
    {
        text.text = value.ToString();
    }
}
