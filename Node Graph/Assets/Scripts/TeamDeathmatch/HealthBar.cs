//Author: Kyle Gian
//Date Created: 17/05/2021
//Last Modified: 17/05/2021

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class HealthBar : MonoBehaviour
{
    public Slider _slider;

    public void SetMaxHealth(int health)
    {
        _slider.maxValue = health;
        _slider.value = health;
    }

    public void SetHealth(int health)
    {
        _slider.value = health;
    }

}
