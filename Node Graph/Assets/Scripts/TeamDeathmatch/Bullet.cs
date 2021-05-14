using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
     int _speed = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    

    private void DestroyBullet()
    {
        Destroy(this);
    }

    public void ShootFrom(Vector3 position)
    {
        
    }

    private void CreateBullet(Vector3 startPosition, Vector3 direction)
    {
        Instantiate(this);
    }
}
