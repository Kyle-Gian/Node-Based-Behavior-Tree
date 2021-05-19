//Author: Kyle Gian
//Date Created: 16/05/2021
//Last Modified: 16/05/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField] private int _healthPack = 20;

    public int GiveHealth()
    {
        return _healthPack;
    }

}
