using System.Collections;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;
//using static UnityEngine.RuleTile.TilingRuleOutput;
using static UnityEngine.UI.GridLayoutGroup;

public class Spider : Monster, IHittable
{
    public enum State { Idle = 0, Trace, Returning, Patrol, Attack, Damage, Die }

    StateMachine<State, Spider> stateMachine;
    private Transform target;
    //private bool canHit = true;
    //private int curDamage;

    private Vector3 moveDir;
    private NavMeshAgent agent;

    SpiderData data;
    MonsterHPBarSlide slide;
    
    Coroutine traceRoutine;
    Coroutine returnRoutine;
    Coroutine patrolRoutine;
    Coroutine attackRoutine;
    Coroutine damageRoutine;
    Coroutine dieRoutine;

    private List<Vector3> patrolPoints = new List<Vector3>();
    private int patrolIdx;
    protected override void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        slide = GameObject.FindGameObjectWithTag("MonsterInfoUI").GetComponent<MonsterHPBarSlide>();
        data = GetComponent<SpiderData>();

        base.Awake();

        stateMachine = new StateMachine<State, Spider>(this);
        stateMachine.AddState(State.Idle,       new IdleState(this, stateMachine));
        stateMachine.AddState(State.Patrol,     new PatrolState(this, stateMachine));
        stateMachine.AddState(State.Trace,      new TraceState(this, stateMachine));
        // returning중에는 플레이어 무시
        stateMachine.AddState(State.Returning,  new ReturningState(this, stateMachine));
        stateMachine.AddState(State.Attack,     new AttackState(this, stateMachine));
        stateMachine.AddState(State.Damage,     new DamageState(this, stateMachine));
        stateMachine.AddState(State.Die,        new DieState(this, stateMachine));

        target = GameObject.FindGameObjectWithTag("Player").transform;
        // 패트롤 위치들
    }
    private void Start()
    {
        patrolPoints.Add(transform.position);
        patrolPoints.Add(transform.position + new Vector3(5, 0, 0));
        patrolPoints.Add(transform.position + new Vector3(0, 0, 5));

        if (transform.position != patrolPoints[0])
        {
            agent.Warp(patrolPoints[0]);
        }

        stateMachine.SetUP(State.Idle);
    }
    private void Update()
    {
        stateMachine.Update();
    }
    protected override void Die()
    {
        StopAllCoroutines();
        agent.enabled = false;
        base.Die();
    }
    public void TakeHit(int damage)
    {
        slide.SetData(data);
        if (CalDamage(damage) > 0)
        {
            data.CurHP -= CalDamage(damage);
            if(data.CurHP <= 0)
            {
                stateMachine.ChangeState(State.Die);
            }
            else
            {
                stateMachine.ChangeState(State.Damage);
            }
            
        }

    }
    private int CalDamage(int damage)
    {
        if ((damage- data.CurDEF) < 0)
        {
            return 0;
        }
        else
        {
            return damage - data.CurDEF;
        }
    }
    private float DistanceToTarget()
    {
        return Vector3.Distance(target.position, transform.position);
    }
    private float DistanceToTarget(Vector3 target)
    {
        return Vector3.Distance(target, transform.position);
    }
    public bool IsInNear()
    {
        bool temp = (DistanceToTarget() <= data.DetectRange);
        return temp;
    }

    #region State 시작
    private abstract class SpiderState : StateBase<State, Spider>
    {
        protected GameObject gameObject => owner.gameObject;
        protected Transform tranform => owner.transform;
        protected Renderer renderer => owner.renderer;
        protected Animator animator => owner.animator;
        protected Collider collider => owner.collider;

        protected SpiderState(Spider owner, StateMachine<State, Spider> stateMachine) : base(owner, stateMachine)
        {

        }
    }

    // 가만히 있음
    private class IdleState : SpiderState
    {
        private Transform target;
        private float detectRange;
        public IdleState(Spider owner, StateMachine<State, Spider> stateMachine) : base(owner, stateMachine)
        {

        }

        public override void Enter()
        {
            //animator.SetTrigger("Idle");
        }

        public override void Exit()
        {
        }

        public override void Setup()
        {
            target = owner.target;
            detectRange = owner.data.DetectRange;
        }

        public override void Transition()
        {
            if(owner.IsInNear())
            {
                stateMachine.ChangeState(State.Trace);
            }
            else
            {
                stateMachine.ChangeState(State.Patrol);
            }   
        }

        public override void Update()
        {
        }
    }
    // 순찰함
    private class PatrolState : SpiderState
    {
        private Transform target;
        private float detectRange;
        
        public PatrolState(Spider owner, StateMachine<State, Spider> stateMachine) : base(owner, stateMachine)
        {

        }

        public override void Enter()
        {
            animator.SetBool("Move", true);
            owner.patrolIdx = 1;
            owner.agent.SetDestination(owner.patrolPoints[owner.patrolIdx]);
            owner.patrolRoutine = owner.StartCoroutine(owner.PatrolRoutine());
        }


        public override void Exit()
        {
            owner.StopCoroutine(owner.patrolRoutine);
            animator.SetBool("Move", false);
        }

        public override void Setup()
        {
            target = owner.target;
            detectRange = owner.data.DetectRange;
        }

        public override void Transition()
        {
            if (owner.IsInNear())
            {
                stateMachine.ChangeState(State.Trace);
            }
        }

        public override void Update()
        {
            
        }
    }
    
    // 추적함
    private class TraceState : SpiderState
    {
        private Transform target;
        private float speed;
        private float detectRange;
        private float attackRange;

        Coroutine lookroutine;
        Coroutine tracingroutine;

        public TraceState(Spider owner, StateMachine<State, Spider> stateMachine) : base(owner, stateMachine)
        {

        }

        public override void Enter()
        {
            animator.SetBool("Move", true);
            lookroutine =  owner.StartCoroutine(owner.LookAtRoutine());
            tracingroutine =  owner.StartCoroutine(owner.TraceRoutine());
        }

        public override void Exit()
        {
            owner.StopCoroutine(lookroutine);
            owner.StopCoroutine(tracingroutine);
            animator.SetBool("Move", false);
        }

        public override void Setup()
        {
            target = owner.target;
            speed = owner.data.MoveSpeed;
            detectRange = owner.data.DetectRange;
            attackRange = owner.data.AttackRange;
        }

        public override void Transition()
        {
            if (owner.DistanceToTarget(owner.patrolPoints[0]) > 15f)
            {
                stateMachine.ChangeState(State.Returning);
            }
            else if (owner.DistanceToTarget() < attackRange /*&& owner.data.AttackMoveDelay == owner.data.AttackMoveCool*/) 
            {
                stateMachine.ChangeState(State.Attack);
            }
        }

        public override void Update()
        {
            owner.agent.destination = target.position;
        }
        
    }
    // idle(페트롤[0]) 위치로 돌아감
    private class ReturningState : SpiderState
    {
        private Transform target;

        public ReturningState(Spider owner, StateMachine<State, Spider> stateMachine) : base(owner, stateMachine)
        {

        }

        public override void Enter()
        {
            animator.SetBool("Move", true);
            owner.returnRoutine = owner.StartCoroutine(owner.ReturnRoutine());
        }

        public override void Exit()
        {
            owner.StopCoroutine(owner.returnRoutine);
            animator.SetBool("Move", false);
            owner.slide.SetEnable(false);
        }

        public override void Setup()
        {
        }

        public override void Transition()
        {
            if(owner.DistanceToTarget(owner.patrolPoints[0]) < 1f)
            {
                stateMachine.ChangeState(State.Idle);
            }
        }

        public override void Update()
        {
        }
    }
    // 공격함
    private class AttackState : SpiderState
    {
        Coroutine atkRoutine;

        public AttackState(Spider owner, StateMachine<State, Spider> stateMachine) : base(owner, stateMachine)
        {

        }

        public override void Enter()
        {
            animator.SetTrigger("Bite");
            atkRoutine = owner.StartCoroutine(owner.AttackRoutine());
        }

        public override void Exit()
        {
            owner.StopCoroutine(atkRoutine);
        }

        public override void Setup()
        {
        }

        public override void Transition()
        {
            if (owner.data.AttackMoveDelay <= 0 )
            {
                stateMachine.ChangeState(State.Trace);
            }
            if(owner.DistanceToTarget() >= owner.data.AttackRange)
            {
                stateMachine.ChangeState(State.Trace);
            }
        }

        public override void Update()
        {
        }
    }
    // 피격받음
    private class DamageState : SpiderState
    {
        Coroutine damageRoutine;
        public DamageState(Spider owner, StateMachine<State, Spider> stateMachine) : base(owner, stateMachine)
        {

        }

        public override void Enter()
        {
            animator.SetTrigger("Damage");
            damageRoutine = owner.StartCoroutine(DamageRoutine());
        }

        public override void Exit()
        {
            owner.StopCoroutine(damageRoutine);
        }

        public override void Setup()
        {
        }

        public override void Transition()
        {
            if(owner.data.CurHP <= 0)
            {
                stateMachine.ChangeState(State.Die);
            }
        }
        public override void Update()
        {
        }
        IEnumerator DamageRoutine()
        {
            while (true)
            {
                owner.agent.destination = tranform.position;
                yield return new WaitForSeconds(1f);
                stateMachine.ChangeState(State.Idle);
            }
        }
    }
    // 죽음
    private class DieState : SpiderState
    {
        public DieState(Spider owner, StateMachine<State, Spider> stateMachine) : base(owner, stateMachine)
        {

        }

        public override void Enter()
        {
            owner.animator.SetTrigger("StartDie");
            
            owner.Die();
        }

        public override void Exit()
        {
        }

        public override void Setup()
        {
        }

        public override void Transition()
        {
        }

        public override void Update()
        {
        }
    }
    #endregion State 끝

    IEnumerator LookAtRoutine()
    {
        while (true)
        {
            moveDir = (target.position - transform.position).normalized;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), 0.1f);
            yield return null;
        }
    }
    IEnumerator TraceRoutine()
    {
        while (true)
        {
            if (!animator.GetBool("Die"))
            {
                transform.Translate(moveDir * Time.deltaTime * data.MoveSpeed, Space.World);
            }
            yield return null;
        }
    }
    IEnumerator ReturnRoutine()
    {
        while (true)
        {
            agent.destination = patrolPoints[0];
            yield return null;
        }
    }
    IEnumerator PatrolRoutine()
    {
        while (true)
        { 
            if (DistanceToTarget(patrolPoints[patrolIdx]) < 1f)
            {
                patrolIdx++;
                patrolIdx %= patrolPoints.Count;
                agent.destination = patrolPoints[patrolIdx];
                yield return new WaitForSeconds(0.2f);
            }
            yield return null;
        }
    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (data.IsAttack && data.AttackMoveDelay > 0)
            {
                if (data.AttackMoveDelay == data.AttackMoveCool)
                {
                    //StopCoroutine(traceRoutine);
                }
                data.AttackMoveDelay -= Time.deltaTime;
                if (data.AttackMoveDelay <= 0)
                {
                    data.AttackMoveDelay = data.AttackMoveCool;
                    data.IsAttack = false;
                    yield return new WaitForSeconds(1f);
                    //traceRoutine = StartCoroutine(TraceRoutine());
                }
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    protected override void Attack()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        IHittable hittable = other.GetComponent<IHittable>();
        hittable?.TakeHit(data.CurATK);
        data.IsAttack = true;
    }
    private void OnTriggerStay(Collider other)
    {
        IHittable hittable = other.GetComponent<IHittable>();
        hittable?.TakeHit(data.CurATK);
        data.IsAttack = true;
    }
}
