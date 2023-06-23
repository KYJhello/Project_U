using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Spider : Monster
{
    public enum State { Idle = 0, Trace, Returning, Patrol, Attack, Damage, Die }

    StateMachine<State, Spider> stateMachine;
    private Transform target;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float detectRange;
    [SerializeField]
    private float attackRange;

    private Vector3 moveDir;
    private NavMeshAgent agent;

    //private List<Vector3> patrolPoints;
    private int patrolIdx;
    protected override void Awake()
    {
        base.Awake();

        stateMachine = new StateMachine<State, Spider>(this);
        stateMachine.AddState(State.Idle,       new IdleState(this, stateMachine));
        stateMachine.AddState(State.Patrol,     new PatrolState(this, stateMachine));
        stateMachine.AddState(State.Trace,      new TraceState(this, stateMachine));
        // returning�߿��� �÷��̾� ����
        stateMachine.AddState(State.Returning,  new ReturningState(this, stateMachine));
        stateMachine.AddState(State.Attack,     new AttackState(this, stateMachine));
        stateMachine.AddState(State.Damage,     new DamageState(this, stateMachine));
        stateMachine.AddState(State.Die,        new DieState(this, stateMachine));

        agent = GetComponent<NavMeshAgent>();
        // ��Ʈ�� ��ġ��
        //patrolPoints.Add(new Vector3(0, 0, 1));
        //patrolPoints.Add(new Vector3(3, 0, 0));
        //patrolPoints.Add(new Vector3(0, 0, 3));
    }
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        stateMachine.SetUP(State.Idle);
    }
    private void Update()
    {
        stateMachine.Update();
    }
    protected override void Die()
    {
        stateMachine.ChangeState(State.Die);
    }


    #region State ����
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

    // ������ ����
    private class IdleState : SpiderState
    {
        private Transform target;
        private float detectRange;
        public IdleState(Spider owner, StateMachine<State, Spider> stateMachine) : base(owner, stateMachine)
        {

        }

        public override void Enter()
        {
            animator.SetBool("Idle", true);
            
        }

        public override void Exit()
        {
            animator.SetBool("Idle", false);
        }

        public override void Setup()
        {
            target = owner.target;
            detectRange = owner.detectRange;
        }

        public override void Transition()
        {
            if((target.position - tranform.position).sqrMagnitude < detectRange)
            {
                stateMachine.ChangeState(State.Trace);
            }
        }

        public override void Update()
        {
        }
    }
    // ������
    private class PatrolState : SpiderState
    {
        private Transform target;
        private float detectRange;
        
        public PatrolState(Spider owner, StateMachine<State, Spider> stateMachine) : base(owner, stateMachine)
        {

        }

        public override void Enter()
        {
            owner.patrolIdx = 0;
            //PatrolCoroutine = owner.StartCoroutine(PatrolMove());
        }


        public override void Exit()
        {

        }

        public override void Setup()
        {
            target = owner.target;
            detectRange = owner.detectRange;
        }

        public override void Transition()
        {

        }

        public override void Update()
        {
            
        }
        //Coroutine PatrolCoroutine;

        //IEnumerator PatrolMove()
        //{
        //    while (true)
        //    {
        //        owner.agent.destination = owner.patrolPoints[owner.patrolIdx];


        //        yield return new WaitForSeconds(0.2f);
        //    }
        //}
    }
    
    // ������
    private class TraceState : SpiderState
    {
        private Transform target;
        private float speed;
        private float detectRange;
        private float attackRange;
        public TraceState(Spider owner, StateMachine<State, Spider> stateMachine) : base(owner, stateMachine)
        {

        }

        public override void Enter()
        {
            owner.StartCoroutine(LookAtRoutine());
            owner.StartCoroutine(TraceRoutine());
        }

        public override void Exit()
        {
            owner.StopAllCoroutines();
        }

        public override void Setup()
        {
            target = owner.target;
            speed = owner.moveSpeed;
            detectRange = owner.detectRange;
            attackRange = owner.attackRange;
        }

        public override void Transition()
        {
            if((target.position - tranform.position).sqrMagnitude > detectRange + 5f )
            {
                stateMachine.ChangeState(State.Returning);
            }
            else if((target.position - tranform.position).sqrMagnitude < attackRange)
            {
                stateMachine.ChangeState(State.Attack);
            }
        }

        public override void Update()
        {
            owner.agent.destination = target.position;
        }
        IEnumerator LookAtRoutine()
        {
            while (true)
            {
                owner.moveDir = (target.position - tranform.position).normalized;
                owner.transform.rotation = Quaternion.Lerp(tranform.rotation, Quaternion.LookRotation(owner.moveDir), 0.1f);
                yield return null;
            }
        }
        IEnumerator TraceRoutine()
        {
            while (true)
            {
                owner.transform.Translate(owner.moveDir * Time.deltaTime * speed);
            }
        }
    }
    // idle(��Ʈ��[0]) ��ġ�� ���ư�
    private class ReturningState : SpiderState
    {
        public ReturningState(Spider owner, StateMachine<State, Spider> stateMachine) : base(owner, stateMachine)
        {

        }

        public override void Enter()
        {
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
    // ������
    private class AttackState : SpiderState
    {
        public AttackState(Spider owner, StateMachine<State, Spider> stateMachine) : base(owner, stateMachine)
        {

        }

        public override void Enter()
        {
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
    // �ǰݹ���
    private class DamageState : SpiderState
    {
        public DamageState(Spider owner, StateMachine<State, Spider> stateMachine) : base(owner, stateMachine)
        {

        }

        public override void Enter()
        {
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
    // ����
    private class DieState : SpiderState
    {
        public DieState(Spider owner, StateMachine<State, Spider> stateMachine) : base(owner, stateMachine)
        {

        }

        public override void Enter()
        {
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


    #endregion State ��
}
