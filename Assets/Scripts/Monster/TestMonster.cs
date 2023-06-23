using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class TestMonster : NonHumanMonster, IHittable
{
    private Transform player;
    private Vector3 moveDir;
    NavMeshAgent agent;

    private void Awake()
    {
        HP = 100;
        maxHP = 100;
        ATK = 10;
        DEF = 5;
        name = "Test";

        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StartCoroutine(MoveRoutine());
    }
    private void Update()
    {
        if(HP < 0)
        {
            Destroy(gameObject);
        }
    }
    public void TakeHit(int damage)
    {
        HP -= damage;
    }

    protected override void Attack()
    {
        
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            agent.destination = player.position;
            moveDir = (player.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), 0.1f);
            transform.Translate(moveDir * Time.deltaTime * 0.5f);

            yield return new WaitForSeconds(0.2f);
        }
    }

}
