using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IHittable
{
    private int curDamage;

    private PlayerData data;
    private PlayerMover mover;
    private PlayerAttacker attacker;
    private PlayerInteractor interactor;
    private ThirdCamController camController;

    public UnityEvent<int> OnHit;
    
    private Coroutine hitRoutine;
    //private Coroutine reloadRoutine;

    public void EnableInput()
    {
        gameObject.GetComponent<PlayerInput>().enabled = true;
        gameObject.GetComponent<ThirdCamController>().enabled = true;
    }

    private void Awake()
    {
        data = GetComponent<PlayerData>();
        mover = GetComponent<PlayerMover>();
        attacker = GetComponent<PlayerAttacker>();
        interactor = GetComponent<PlayerInteractor>();
        camController = GetComponent<ThirdCamController>();

        data.hitDelay = data.hitCool;
    }
    private void OnEnable()
    {
        hitRoutine = StartCoroutine(HitRoutine());
    }
    private void OnDisable()
    {
        StopCoroutine(hitRoutine);
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

        OnDisable();
    }
    public void TakeHit(int damage)
    {
        if(data.isRoll)
        {
            curDamage = 0;
            return;
        }
        curDamage = CalDamage(damage);
        //Debug.Log(curDamage);
        data.isHit = true;
    }
    private int CalDamage(int damage)
    {
        if((damage - data.CurDEF) < 0)
        {
            return 0;
        }
        else
        {
            return damage - data.CurDEF;
        }
    }

    private void OnReload(InputValue value)
    {
        data.Anim.SetTrigger("Reload");
    }
    private void OnAlt(InputValue value)
    {
        if (value.isPressed)
        {
            camController.OnDisable();
            camController.enabled = false;
            attacker.enabled = false;
        }
        else
        {
            if (camController.enabled)
            {
                camController.OnEnable();
                camController.enabled = true;
                attacker.enabled = true;
            }
        }
    }

    IEnumerator HitRoutine()
    {
        while (true)
        {
            if (data.isHit && data.hitDelay > 0)
            {
                if(data.hitDelay == data.hitCool)
                {
                    //mover.enabled = false;
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
                data.hitDelay -= Time.deltaTime;
                if (data.hitDelay <= 0 && data.isHit)
                {
                    data.isHit = false;
                    data.hitDelay = data.hitCool;
                    //mover.enabled = true;
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
