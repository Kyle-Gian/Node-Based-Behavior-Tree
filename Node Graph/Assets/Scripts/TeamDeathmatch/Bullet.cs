using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
     private float _speed = 0.5f;
    [SerializeField]
     private int _bulletDamage = 10;

     private Vector3 _originalPosition;
    [SerializeField]
     private float _destroyTime = 3;
     
     

     void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        
        StartCoroutine(DestroyTime());

    }

     private void OnTriggerEnter(Collider other)
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
    

    IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(_destroyTime);
        DestroyBullet();
    }
}
