using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using Unity.VisualScripting.FullSerializer;

public class TestMonster : NonHumanMonster, IHittable
{
    private Transform player;
    private Vector3 moveDir;
    NavMeshAgent agent;

    private bool isAttack = false;
    private float attackMoveDelay = 1f;
    private float attackMoveCool = 1f;

    private Coroutine moveRoutine;
    private Coroutine attackRoutine;
    [SerializeField] LayerMask layerMask;

    private void Awake()
    {
        HP = 1000;
        maxHP = 1000;
        ATK = 10;
        DEF = 5;
        name = "Test";

        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        moveRoutine = StartCoroutine(MoveRoutine());
        attackRoutine = StartCoroutine(AttackRoutine());
    }
    private void Update()
    {
        if(HP < 0)
        {
            Destroy(gameObject);
        }

    }
    public new void TakeHit(int damage)
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
            agent.destination = player.position;
            moveDir = (player.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), 0.1f);
            transform.Translate(moveDir * Time.deltaTime * 0.2f);

            yield return new WaitForSeconds(0.2f);
        }
    }
    IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (isAttack && attackMoveDelay > 0)
            {
                if(attackMoveDelay == attackMoveCool)
                {
                    StopCoroutine(moveRoutine);
                }
                attackMoveDelay -= Time.deltaTime;
                if(attackMoveDelay <= 0)
                {
                    attackMoveDelay = attackMoveCool;
                    isAttack = false;
                    moveRoutine = StartCoroutine(MoveRoutine());
                }
            }
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IHittable hittable = other.GetComponent<IHittable>();
        hittable?.TakeHit(ATK);
        
        isAttack = true;
    }
    private void OnTriggerStay(Collider other)
    {
        IHittable hittable = other.GetComponent<IHittable>();
        hittable?.TakeHit(ATK);
        isAttack = true;
    }
    private void OnTriggerExit(Collider other)
    {

    }

}
