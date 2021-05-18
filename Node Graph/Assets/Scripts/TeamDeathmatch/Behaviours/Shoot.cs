//Author: Kyle Gian
//Date Created: 13/05/2021
//Last Modified: 13/05/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shoot : Behaviour
{
    NavMeshAgent _agent;
    [SerializeField]
    private GameObject _bullet;

    private GameObject _gun;

    private bool _canFire = true;
    private GameObject _target;

    private float _fireRate = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _gun = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        _target = GetComponent<Target>().GetTarget();
        
        if (_target != null)
        {
            _agent.transform.LookAt(_target.transform.position);
        }

        if (_canFire)
        {
            StartCoroutine("BulletSpawnTime");
        }
    }
    
    private IEnumerator BulletSpawnTime()
    {
        _canFire = false;
        yield return new WaitForSeconds(_fireRate);

        GameObject newBullet = Instantiate(_bullet, _gun.transform.position, _gun.transform.rotation);

        _canFire = true;


    }

    public override Behaviour GetBehaviour()
    {
        return GetComponent<Shoot>();
    }

    public override TreeNode.Status ReturnBehaviorStatus()
    {
        return TreeNode.Status.PROCESSING;
    }

    public override void SetBehaviourStatus(TreeNode.Status status)
    {
        _currentStatus = status;

    }

    public override Vector3 GetObjectPosition()
    {
        return transform.position;
    }
}
