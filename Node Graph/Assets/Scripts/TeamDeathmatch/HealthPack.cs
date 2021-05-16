using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Blue") || other.CompareTag("Red"))
        {
            Destroy(this.gameObject, 0.5f);

        }
    }
}
