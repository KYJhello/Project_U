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

    [SerializeField] LayerMask layerMask;

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
            player = GameObject.FindGameObjectWithTag("Player").transform;
            agent.destination = player.position;
            moveDir = (player.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), 0.1f);
            transform.Translate(moveDir * Time.deltaTime * 0.5f);

            yield return new WaitForSeconds(0.2f);
        }
    }
    Coroutine attackRoutine;
    private void OnTriggerEnter(Collider other)
    {
        IHittable hittable = other.GetComponent<IHittable>();
        hittable?.TakeHit(ATK);
    }
    private void OnTriggerStay(Collider other)
    {
        IHittable hittable = other.GetComponent<IHittable>();
        hittable?.TakeHit(ATK);
    }
    private void OnTriggerExit(Collider other)
    {

    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f, layerMask);
            
            
            foreach (Collider collider in colliders)
            {
                //if (Mathf.Abs(Vector3.Distance(collider.transform.position, transform.position)) >= 2f)
                //{
                //    yield return null;
                //}
                IHittable hittable = collider.GetComponent<IHittable>();
                hittable?.TakeHit(ATK);
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
