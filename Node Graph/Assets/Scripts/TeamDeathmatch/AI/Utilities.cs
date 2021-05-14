using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    public int _currentGrenades;
    [SerializeField]
    int _maxGrenades;

    [SerializeField]
    GameObject _grenade;
    public int _grenadeRadius;
    // Start is called before the first frame update
    void Start()
    {
        _currentGrenades = _maxGrenades;
    }


    public void PickUpGrenade()
    {

    }

    public void ThrowGrenade()
    {

    }

    private bool DoIHaveSpaceForGrenade()
    {
        if (_currentGrenades != _maxGrenades)
        {
            _currentGrenades = +1;
            return true;

        }
        return false;
    }

    private bool CanIThrowGrenade()
    {
        if (_currentGrenades != 0)
        {
            _currentGrenades -= 1;
            return true;
        }
        return false;
    }

    public void DestroyGrenade()
    {
        Destroy(_grenade);
    }

    public void CreateGrenade()
    {
        Instantiate(_grenade);
    }

}
