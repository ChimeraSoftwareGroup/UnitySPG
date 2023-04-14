using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BobeeEnemy : ObjectsToSpawn
{
    [SerializeField] private Player _target;
    public float speed = 5f;
    [SerializeField] private float lifeTime = 5;
    [SerializeField] private float currentLife = 0;
    // public NavMeshAgent enemy;

    void Start()
    {
        _target = GameObject.FindObjectOfType<Player>();
    }

    private void OnCollisionEnter(Collision collision)
    {
    }

    void Update()
    {
        if (_target == null || _target.enabled == false)
        {
            return;
        }
        currentLife += Time.deltaTime;
        Vector3 dir = _target.transform.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        if (currentLife > lifeTime)
        {
            EnemyDeath();
        }
    }
    void EnemyDeath()
    {
        Destroy(gameObject);
    }
}
