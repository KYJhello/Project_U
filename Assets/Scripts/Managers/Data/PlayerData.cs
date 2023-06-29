using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 플레이어 정보만 넣기!!!!
public class PlayerData : MonoBehaviour
{
    #region Player Info Data
    protected new string name = "Unity";        // 이름 
    protected int HP = 100;                     // 체력
    protected int maxHP = 100;                  // 최대 체력
    protected int ATK = 10;                     // 공격 
    protected int DEF = 5;                      // 방어 

    protected List<WeaponBase> weapon;          // 보유 무기들
    protected Animator animator;                // 애니메이터
    #endregion

    #region PlayerMoveData
    // 현재 이동속도
    protected float moveSpeed;
    // 고정 이동속도
    protected float crouchSpeed = 2;
    protected float walkSpeed = 3;
    protected float runSpeed = 10;
    protected float jumpSpeed = 5;
    protected float rollSpeed = 10f;


    #endregion

    #region PlayerController
    [HideInInspector] public bool isRoll;
    [HideInInspector] public bool isHit = false;
    [HideInInspector] public float hitDelay;
    [HideInInspector] public float hitCool = 1f;
    [HideInInspector] public float reloadDelay;
    [HideInInspector] public float reloadCool = 1f;
    #endregion


    #region Get, Set
    public string Name { get { return name; }  }
    public int CurHP { get { return HP; } set { HP = value; } }
    public int MaxHP { get { return maxHP; } }
    public int CurATK { get { return ATK; } }
    public int CurDEF { get { return DEF; } }

    public Animator Anim { get { return animator; } }


    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public float CrouchSpeed { get { return crouchSpeed; } }
    public float WalkSpeed { get { return walkSpeed;} }
    public float RunSpeed { get { return runSpeed;} }
    public float JumpSpeed { get { return jumpSpeed;} }
    public float RollSpeed { get { return  rollSpeed;} }
    #endregion

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

}
