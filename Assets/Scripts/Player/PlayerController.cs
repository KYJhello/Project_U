using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour, IHittable
{
    private bool isHit = false;
    private float hitDelay = 3f;
    private float hitCool = 3f;
    private int curDamage;

    private PlayerData data;
    private PlayerMover mover;
    private PlayerAttacker attacker;
    private PlayerInteractor interactor;
    private ThirdCamController camController;

    public UnityEvent<int> OnHit;
    
    public Collider collider;
    private Coroutine hitRoutine;

    private void Awake()
    {
        data = GetComponent<PlayerData>();
        mover = GetComponent<PlayerMover>();
        attacker = GetComponent<PlayerAttacker>();
        interactor = GetComponent<PlayerInteractor>();
        camController = GetComponent<ThirdCamController>();

        collider = GetComponent<Collider>();
        hitRoutine = StartCoroutine(HitRoutine());
    }

    public void Update()
    {

    }

    private bool IsDie()
    {
        if(data.CurHP <= 0)
        {
            return true;
        }
        return false;
    }
    private void Die()
    {
        data.Anim.SetTrigger("Death");
        mover.enabled = false;
        attacker.enabled = false;
        interactor.enabled = false;
        camController.enabled = false;
        collider.enabled = false;
    }
    public void TakeHit(int damage)
    {
        if(data.isRoll)
        {
            curDamage = 0;
            return;
        }
        curDamage = damage;
        isHit = true;
    }

    IEnumerator HitRoutine()
    {
        while (true)
        {
            if (isHit && hitDelay > 0)
            {
                if(hitDelay == hitCool)
                {
                    Debug.Log("getDamage");
                    data.CurHP -= curDamage;
                    Debug.Log("Cur HP : " + data.CurHP);
                    if (IsDie())
                    {
                        Die();
                    }
                    else
                    {
                        data.Anim.SetTrigger("Hit");
                    }
                }
                hitDelay -= Time.deltaTime;
                if (hitDelay <= 0 && isHit)
                {
                    isHit = false;
                    hitDelay = hitCool;
                }
                
            }
            yield return null;
        }

    }

    //    private PlayerData data;
    //    private PlayerMover mover;
    //    private PlayerAttacker attacker;
    //    private PlayerInteractor interactor;

    //    public enum State { Idle = 0, Move, Jump, Roll, Attack, Damage, Die }
    //    StateMachine<State, PlayerController> stateMachine;

    //    public List<Coroutine> coroutines;

    //    public void NowChangeWeapon(WeaponType weaponType)
    //    {
    //        data.Anim.SetInteger("CurWeapon", (int)weaponType);
    //    }
    //    private void Awake()
    //    {
    //        data = GetComponent<PlayerData>();
    //        mover = GetComponent<PlayerMover>();
    //        attacker = GetComponent<PlayerAttacker>();
    //        interactor = GetComponent<PlayerInteractor>();

    //        coroutines = new List<Coroutine>();

    //        stateMachine = new StateMachine<State, PlayerController>(this);
    //        stateMachine.AddState(State.Idle,       new IdleState(this, stateMachine ));
    //        stateMachine.AddState(State.Move,       new MoveState(this, stateMachine));
    //        stateMachine.AddState(State.Jump,       new JumpState(this, stateMachine));
    //        stateMachine.AddState(State.Roll,       new RollState(this, stateMachine));
    //        stateMachine.AddState(State.Attack,     new AttackState(this, stateMachine));
    //        stateMachine.AddState(State.Damage,     new DamageState(this, stateMachine));
    //        stateMachine.AddState(State.Die,        new DieState(this, stateMachine));
    //    }
    //    private void Start()
    //    {
    //        stateMachine.Update();
    //    }
    //    protected void Die()
    //    {
    //        stateMachine.ChangeState(State.Die);
    //    }




    //    #region State
    //    private abstract class PlayerState : StateBase<State, PlayerController>
    //    {

    //        protected PlayerState(PlayerController owner, StateMachine<State, PlayerController> stateMachine) : base(owner, stateMachine)
    //        {

    //        }
    //    }
    //    private class IdleState : PlayerState
    //    {
    //        public IdleState(PlayerController owner, StateMachine<State, PlayerController> stateMachine) : base(owner, stateMachine)
    //        {

    //        }

    //        public override void Enter()
    //        {

    //        }

    //        public override void Exit()
    //        {
    //        }

    //        public override void Setup()
    //        {
    //        }

    //        public override void Transition()
    //        {
    //            if(owner.mover.MoveDir != Vector3.zero) {
    //                stateMachine.ChangeState(State.Move);
    //            }
    //        }

    //        public override void Update()
    //        {

    //        }
    //    }
    //    private class MoveState : PlayerState
    //    {
    //        public MoveState(PlayerController owner, StateMachine<State, PlayerController> stateMachine) : base(owner, stateMachine)
    //        {

    //        }

    //        public override void Enter()
    //        {
    //            owner.mover.Move();
    //        }

    //        public override void Exit()
    //        {
    //        }

    //        public override void Setup()
    //        {
    //        }

    //        public override void Transition()
    //        {

    //        }

    //        public override void Update()
    //        {
    //            return;
    //        }
    //    }
    //    private class JumpState : PlayerState
    //    {
    //        public JumpState(PlayerController owner, StateMachine<State, PlayerController> stateMachine) : base(owner, stateMachine)
    //        {

    //        }

    //        public override void Enter()
    //        {

    //        }

    //        public override void Exit()
    //        {
    //        }

    //        public override void Setup()
    //        {
    //        }

    //        public override void Transition()
    //        {

    //        }

    //        public override void Update()
    //        {
    //        }
    //    }
    //    private class RollState : PlayerState
    //    {
    //        public RollState(PlayerController owner, StateMachine<State, PlayerController> stateMachine) : base(owner, stateMachine)
    //        {

    //        }

    //        public override void Enter()
    //        {

    //        }

    //        public override void Exit()
    //        {
    //        }

    //        public override void Setup()
    //        {
    //        }

    //        public override void Transition()
    //        {

    //        }

    //        public override void Update()
    //        {
    //        }
    //    }
    //    private class AttackState : PlayerState
    //    {
    //        public AttackState(PlayerController owner, StateMachine<State, PlayerController> stateMachine) : base(owner, stateMachine)
    //        {

    //        }

    //        public override void Enter()
    //        {

    //        }

    //        public override void Exit()
    //        {
    //        }

    //        public override void Setup()
    //        {
    //        }

    //        public override void Transition()
    //        {

    //        }

    //        public override void Update()
    //        {
    //        }
    //    }
    //    private class DamageState : PlayerState
    //    {
    //        public DamageState(PlayerController owner, StateMachine<State, PlayerController> stateMachine) : base(owner, stateMachine)
    //        {

    //        }

    //        public override void Enter()
    //        {

    //        }

    //        public override void Exit()
    //        {
    //        }

    //        public override void Setup()
    //        {
    //        }

    //        public override void Transition()
    //        {

    //        }

    //        public override void Update()
    //        {
    //        }
    //    }
    //    private class DieState : PlayerState
    //    {
    //        public DieState(PlayerController owner, StateMachine<State, PlayerController> stateMachine) : base(owner, stateMachine)
    //        {

    //        }

    //        public override void Enter()
    //        {

    //        }

    //        public override void Exit()
    //        {
    //        }

    //        public override void Setup()
    //        {
    //        }

    //        public override void Transition()
    //        {

    //        }

    //        public override void Update()
    //        {
    //        }
    //    }
    //    #endregion

}
