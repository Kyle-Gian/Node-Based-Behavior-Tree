//Author: Kyle Gian
//Date Created: 16/05/2021
//Last Modified: 20/05/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField] private int _healthPack = 20;
    private GameObject _seeker = null;

    public int GiveHealth()
    {
        return _healthPack;
    }

    public GameObject IsObjectTargeted()
    {
        return _seeker;
    }

    public void SetTargetingObject(GameObject seeker)
    {
        _seeker = seeker;
    }

}
