using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerData data;
    private PlayerMover mover;
    private PlayerAttacker attacker;
    private PlayerInteractor interactor;

    public enum State { Idle = 0, Move, Jump, Roll, Attack, Damage, Die }
    StateMachine<State, PlayerController> stateMachine;
    
    public List<Coroutine> coroutines;

    public void NowChangeWeapon(WeaponType weaponType)
    {
        data.Anim.SetInteger("CurWeapon", (int)weaponType);
    }
    private void Awake()
    {
        data = GetComponent<PlayerData>();
        mover = GetComponent<PlayerMover>();
        attacker = GetComponent<PlayerAttacker>();
        interactor = GetComponent<PlayerInteractor>();

        coroutines = new List<Coroutine>();

        stateMachine = new StateMachine<State, PlayerController>(this);
        stateMachine.AddState(State.Idle,       new IdleState(this, stateMachine ));
        stateMachine.AddState(State.Move,       new MoveState(this, stateMachine));
        stateMachine.AddState(State.Jump,       new JumpState(this, stateMachine));
        stateMachine.AddState(State.Roll,       new RollState(this, stateMachine));
        stateMachine.AddState(State.Attack,     new AttackState(this, stateMachine));
        stateMachine.AddState(State.Damage,     new DamageState(this, stateMachine));
        stateMachine.AddState(State.Die,        new DieState(this, stateMachine));
    }
    private void Start()
    {
        stateMachine.Update();
    }
    protected void Die()
    {
        stateMachine.ChangeState(State.Die);
    }




    #region State
    private abstract class PlayerState : StateBase<State, PlayerController>
    {

        protected PlayerState(PlayerController owner, StateMachine<State, PlayerController> stateMachine) : base(owner, stateMachine)
        {

        }
    }
    private class IdleState : PlayerState
    {
        public IdleState(PlayerController owner, StateMachine<State, PlayerController> stateMachine) : base(owner, stateMachine)
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
            if(owner.mover.MoveDir != Vector3.zero) {
                stateMachine.ChangeState(State.Move);
            }
        }

        public override void Update()
        {
            
        }
    }
    private class MoveState : PlayerState
    {
        public MoveState(PlayerController owner, StateMachine<State, PlayerController> stateMachine) : base(owner, stateMachine)
        {

        }

        public override void Enter()
        {
            owner.mover.Move();
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
            return;
        }
    }
    private class JumpState : PlayerState
    {
        public JumpState(PlayerController owner, StateMachine<State, PlayerController> stateMachine) : base(owner, stateMachine)
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
    private class RollState : PlayerState
    {
        public RollState(PlayerController owner, StateMachine<State, PlayerController> stateMachine) : base(owner, stateMachine)
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
    private class AttackState : PlayerState
    {
        public AttackState(PlayerController owner, StateMachine<State, PlayerController> stateMachine) : base(owner, stateMachine)
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
    private class DamageState : PlayerState
    {
        public DamageState(PlayerController owner, StateMachine<State, PlayerController> stateMachine) : base(owner, stateMachine)
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
    private class DieState : PlayerState
    {
        public DieState(PlayerController owner, StateMachine<State, PlayerController> stateMachine) : base(owner, stateMachine)
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
    #endregion

}
