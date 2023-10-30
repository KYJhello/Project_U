using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class SpiderKing : BossMonster, IHittable
{
    // attack = 전방 물기, 좌측 공격, 우측공격 
    // 스킬공격(점프, 거미줄뱉기, 거미줄 원뿔모양 공격)
    public enum AttackSkill { Jump = 0, Web_Shoot, Web_Cone};
    // healSkill = 힐
    public enum State { Idle = 0, Trace, Returning, Attack, ATKSkill, HealSkill, Damage, Die}

    StateMachine<State, SpiderKing> stateMachine;

    [SerializeField] ShowReturnRange showRange;
    [SerializeField] LayerMask targetMask;
    private Transform target;
    private Vector3 rootPoint;
    private Vector3 moveDir;
    SpiderKingData data;
    MonsterHPBarSlide slide;
    float time = 2f;

    private bool isReturn;
    private bool isSkilling;
    private float cosResult;
    private float attackAngle = 90f;
    private int curSkillDamage = 0;
    private float skillRange = 10f;

    Coroutine lookAtRoutine;
    Coroutine traceRoutine;
    Coroutine returnRountine;
    Coroutine attackRoutine;
    Coroutine attackLookRoutine;
    Coroutine skillRoutine;
    Coroutine damageRoutine;
    Coroutine dieRoutine;
    Coroutine jumpAttackRoutine;

    protected override void Awake()
    {
        rootPoint = (transform.position + new Vector3(0,0.5f,0));
        slide = GameObject.FindGameObjectWithTag("MonsterInfoUI").GetComponent<MonsterHPBarSlide>();
        data = GetComponent<SpiderKingData>();
        cosResult = Mathf.Cos(attackAngle * 0.5f * Mathf.Deg2Rad);

        base.Awake();

        stateMachine = new StateMachine<State, SpiderKing>(this);
        stateMachine.AddState(State.Idle, new IdleState(this, stateMachine));
        stateMachine.AddState(State.Trace, new TraceState(this, stateMachine));
        // returning중에는 플레이어 무시, 피 회복
        stateMachine.AddState(State.Returning, new ReturningState(this, stateMachine));
        stateMachine.AddState(State.Attack, new AttackState(this, stateMachine));
        stateMachine.AddState(State.ATKSkill, new ATKSkillState(this, stateMachine));
        //stateMachine.AddState(State.HealSkill, new AttackState(this, stateMachine));
        stateMachine.AddState(State.Damage, new DamageState(this, stateMachine));
        stateMachine.AddState(State.Die, new DieState(this, stateMachine));

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        stateMachine.SetUP(State.Idle);
    }
    private void Update()
    {
        stateMachine.Update();
    }
    protected override void Die()
    {
        StopAllCoroutines();
        base.Die();
    }

    public void TakeHit(int damage)
    {
        slide.SetData(data);
        if (CalDamage(damage) > 0)
        {   
            if(!isReturn)
            {
                data.CurHP -= CalDamage(damage);
                if (data.CurHP <= 0)
                {
                    stateMachine.ChangeState(State.Die);
                }
                else
                {
                    stateMachine.ChangeState(State.Damage);
                }
            }
        }

    }
    private int CalDamage(int damage)
    {
        if ((damage - data.CurDEF) < 0)
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

    protected override void Attack()
    {
        animator.SetTrigger("Attack");
        // 1. 범위 안에 있는지
        Collider[] colliders = Physics.OverlapSphere(transform.position, data.AttackRange, targetMask);
        foreach (Collider collider in colliders)
        {
            //2. 앞에 있는지
            Vector3 dirTarget = (collider.transform.position - transform.position).normalized;
            if (Vector3.Dot(transform.forward, dirTarget) < cosResult)
            {
                continue;
            }
            else
            {
                IHittable hittable = collider.GetComponent<IHittable>();
                hittable?.TakeHit(data.CurATK + curSkillDamage);

                if (hittable != null)
                {
                    float totalTime = Vector2.Distance(transform.position, collider.transform.position) / 10f;
                    float rate = 0;
                    while (rate < 1)
                    {
                        rate += Time.deltaTime / totalTime;
                    }
                }
            }
        }
    }



    #region State
    private abstract class SpiderKingState : StateBase<State, SpiderKing>
    {
        protected GameObject gameObject => owner.gameObject;
        protected Transform tranform => owner.transform;
        protected Renderer renderer => owner.renderer;
        protected Animator animator => owner.animator;
        protected Collider collider => owner.collider;
        protected Transform target => owner.target;

        protected SpiderKingState(SpiderKing owner, StateMachine<State, SpiderKing> stateMachine) : base(owner, stateMachine)
        {

        }
    }

    private class IdleState : SpiderKingState
    {
        public IdleState(SpiderKing owner, StateMachine<State, SpiderKing> stateMachine) : base(owner, stateMachine)
        {

        }

        public override void Enter()
        {
            Debug.Log("StateIdle");
        }

        public override void Exit()
        {
        }

        public override void Setup()
        {
        }

        public override void Transition()
        {
            if(owner.DistanceToTarget(target.position) < owner.data.DetectRange)
            {
                stateMachine.ChangeState(State.Trace);

            }
        }

        public override void Update()
        {
        }
    }
    private class TraceState : SpiderKingState
    {
        public TraceState(SpiderKing owner, StateMachine<State, SpiderKing> stateMachine) : base(owner, stateMachine)
        {

        }

        public override void Enter()
        {
            Debug.Log("StateTrace");
            owner.lookAtRoutine = owner.StartCoroutine(owner.LookAtRoutine());
            owner.traceRoutine = owner.StartCoroutine(owner.TraceRoutine());
            owner.showRange.OnEnable();
            owner.slide.SetData(owner.data);
            if (owner.skillRoutine != null)
            {
                owner.StopCoroutine(owner.skillRoutine);
            }
        }

        public override void Exit()
        {
            owner.StopCoroutine(owner.lookAtRoutine);
            owner.StopCoroutine(owner.traceRoutine);
        }

        public override void Setup()
        {
        }

        public override void Transition()
        {
            float targetD = owner.DistanceToTarget(target.position);
            float rootD = owner.DistanceToTarget(owner.rootPoint);

            if (targetD < owner.skillRange &&
                targetD > owner.data.AttackRange &&
                owner.data.SkillCool == owner.data.SkillDelay)
            {
                stateMachine.ChangeState(State.ATKSkill);
            }
            else if (targetD <= owner.data.AttackRange)
            {
                stateMachine.ChangeState(State.Attack);
            }
            else if (rootD > owner.data.ReturnDistance)
            {
                stateMachine.ChangeState(State.Returning);
            }

        }

        public override void Update()
        {

        }
    }
    private class ReturningState : SpiderKingState
    {
        public ReturningState(SpiderKing owner, StateMachine<State, SpiderKing> stateMachine) : base(owner, stateMachine)
        {

        }

        public override void Enter()
        {
            Debug.Log("StateReturn");
            owner.returnRountine = owner.StartCoroutine(owner.ReturnRoutine());
            owner.isReturn = true;
        }

        public override void Exit()
        {
            owner.StopCoroutine(owner.returnRountine);
            owner.isReturn= false;
            owner.showRange.OnDisable();
            owner.slide.SetEnable(false);
        }

        public override void Setup()
        {
        }

        public override void Transition()
        {
            if(owner.DistanceToTarget(owner.rootPoint) < 0.1f)
            {
                owner.data.CurHP = owner.data.MaxHP;
                stateMachine.ChangeState(State.Idle);
            }
        }

        public override void Update()
        {
        }
    }
    private class AttackState : SpiderKingState
    {
        public AttackState(SpiderKing owner, StateMachine<State, SpiderKing> stateMachine) : base(owner, stateMachine)
        {

        }

        public override void Enter()
        {
            Debug.Log("StateAttack");
            owner.attackRoutine = owner.StartCoroutine(owner.AttackRoutine());
            owner.attackLookRoutine = owner.StartCoroutine(owner.AttackLookRoutine());
            owner.data.IsAttack = true;
            owner.data.AttackMoveDelay = owner.data.AttackMoveCool;
        }

        public override void Exit()
        {
            owner.StopCoroutine(owner.attackRoutine);
            owner.StopCoroutine(owner.attackLookRoutine);
            owner.data.IsAttack= false;
        }

        public override void Setup()
        {
        }

        public override void Transition()
        {
            if (owner.DistanceToTarget(target.position) > owner.data.AttackRange || !owner.data.IsAttack)
            {
                stateMachine.ChangeState(State.Trace);
            }
            //else if (owner.DistanceToTarget(target.position) < owner.skillRange && owner.data.SkillDelay == owner.data.SkillCool)
            //{
            //    stateMachine.ChangeState(State.ATKSkill);
            //}
        }

        public override void Update()
        {
        }
    }
    private class ATKSkillState : SpiderKingState
    {
        public ATKSkillState(SpiderKing owner, StateMachine<State, SpiderKing> stateMachine) : base(owner, stateMachine)
        {

        }

        public override void Enter()
        {
            Debug.Log("SkillState");
            owner.isSkilling = true;
            owner.data.SkillDelay = owner.data.SkillCool;
            if(owner.skillRoutine != null) { owner.StopCoroutine(owner.skillRoutine); }
            owner.skillRoutine = owner.StartCoroutine(owner.ATKSkillRoutine());
        }

        public override void Exit()
        {
            owner.StopCoroutine(owner.ATKSkillRoutine());
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

    private class DamageState : SpiderKingState
    {
        public DamageState(SpiderKing owner, StateMachine<State, SpiderKing> stateMachine) : base(owner, stateMachine)
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
            stateMachine.ChangeState(State.Trace);
        }

        public override void Update()
        {
        }
    }
    private class DieState : SpiderKingState
    {
        public DieState(SpiderKing owner, StateMachine<State, SpiderKing> stateMachine) : base(owner, stateMachine)
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
    #endregion State

    #region Enumerator Routines
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
            if (!animator.GetBool("Die") && DistanceToTarget(target.position) > data.AttackRange)
            {
                transform.Translate(moveDir * Time.deltaTime * data.MoveSpeed, Space.World);
            }
            data.AttackMoveDelay -= Time.deltaTime;


            yield return null;
        }
    }
    IEnumerator ReturnRoutine()
    {
        while (true)
        {
            moveDir = (rootPoint - transform.position).normalized;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), 0.1f);
            transform.Translate(moveDir * Time.deltaTime * data.MoveSpeed, Space.World);
            yield return null;
        }
    }
    IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (data.IsAttack && data.AttackMoveDelay > 0)
            {
                if(data.AttackMoveDelay == data.AttackMoveCool)
                {
                    Debug.Log("ATTACK!!!!");
                    Attack();
                }
                data.AttackMoveDelay -= Time.deltaTime;
                //Debug.Log(data.AttackMoveDelay);
                if (data.AttackMoveDelay <= 0)
                {
                    data.AttackMoveDelay = data.AttackMoveCool;
                    data.IsAttack = false;
                    yield return new WaitForSeconds(3f);
                }
            }
            yield return null;
        }
    }
    IEnumerator AttackLookRoutine()
    {
        while (true)
        {
            if (!animator.GetBool("Die") && data.IsAttack /*DistanceToTarget(target.position) > data.AttackRange*/)
            {
                transform.Translate(moveDir * Time.deltaTime * data.MoveSpeed, Space.World);
            }

            yield return null;
        }
    }


    IEnumerator ATKSkillRoutine()
    {
        while (true)
        {
            if (IsGrounded())
            {
                if (data.SkillCool == data.SkillDelay)
                {
                    if (jumpAttackRoutine != null)
                    {
                        StopCoroutine(jumpAttackRoutine);
                    }
                    else
                    {
                        jumpAttackRoutine = StartCoroutine(JumpAttackRoutine());
                    }
                    yield return new WaitForSeconds(time);
                    StopCoroutine(jumpAttackRoutine);
                }
            }
            data.SkillDelay -= Time.deltaTime;

            if (data.SkillDelay <= 0)
            {
                //int skillIDX = Random.Range(0, 3);
                //switch(skillIDX)
                //{
                //    case (int)AttackSkill.Jump:
                //        JumpAttack();
                //        break;
                //    case (int)AttackSkill.Web_Shoot:
                //        break;
                //    case (int)AttackSkill.Web_Cone:
                //        break;
                //}
                isSkilling = false;
                stateMachine.ChangeState(State.Trace);
            }
            yield return null;
        }
    }
    #endregion
    IEnumerator JumpAttackRoutine()
    {
        float xSpeed = (target.position.x - transform.position.x) / time;
        float zSpeed = (target.position.z - transform.position.z) / time;
        float ySpeed = -1 * (0.5f * Physics.gravity.y * time * time + transform.position.y) / time;

        float curTime = 0;
        while (curTime < time)
        {

            //if (IsGrounded())
            //{
            //    yield return null;
            //}
            curTime += Time.deltaTime;
            ySpeed += Physics.gravity.y * Time.deltaTime;

            if (transform.position.y < 0)
            {
                transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
            }
            else
            {
                transform.position += new Vector3(xSpeed, ySpeed, zSpeed) * Time.deltaTime;

            }

            yield return null;
        }
    }
    private bool IsGrounded()
    {
        RaycastHit hit;
        return Physics.SphereCast(transform.position + Vector3.up * 0.5f,
            0.5f, Vector3.down, out hit, 0.6f);
    }

}
