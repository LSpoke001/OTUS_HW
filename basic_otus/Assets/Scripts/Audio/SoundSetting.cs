using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    public Slider slider;
    public AudioMixer audioMixer;
    public string nameGroup;
    
    void Start()
    {
        audioMixer.GetFloat(nameGroup, out var value);
        slider.value = value;
    }

    private void OnEnable()
    {
        slider.onValueChanged.AddListener(SliderValue);
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(SliderValue);
    }

    private void SliderValue(float value)
    {
        audioMixer.SetFloat(nameGroup, value);
    }
}
