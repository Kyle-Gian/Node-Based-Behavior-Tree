using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
     private int _speed = 5;
    [SerializeField]
     private int _bulletDamage = 10;

     private Vector3 _originalPosition;
    [SerializeField]
     private float _destroyDistance = 50;

     private Vector3 _shootDistanceFromAI = new Vector3(1, 0, 0);

    void Update()
    {
        transform.position += new Vector3(_speed,0,0) * Time.deltaTime;

        if (Vector3.Distance(_originalPosition, transform.position) > _destroyDistance)
        {
            DestroyBullet();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Red") ||other.gameObject.CompareTag("Blue"))
        {
            other.gameObject.GetComponent<AIHealth>().Hurt(_bulletDamage);
        }
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        Destroy(this.gameObject);
    }

    public void ShootBullet(Vector3 startPosition, Vector3 direction)
    {
        Instantiate(this, startPosition += _shootDistanceFromAI , Quaternion.LookRotation(direction));
    }
}
