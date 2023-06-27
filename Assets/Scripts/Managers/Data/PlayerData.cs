using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 플레이어 정보만 넣기!!!!
public class PlayerData : MonoBehaviour
{
    #region Player Info Data
    protected new string name = "Unity";
    protected int HP = 100;                       // 체력
    protected int maxHP = 100;                    // 최대 체력
    protected int ATK = 10;                      // 공격 
    protected int DEF = 5;                      // 방어 

    protected Animator animator;                // 애니메이터
    protected List<WeaponBase> weapon;          // 보유 무기들
    #endregion

    #region PlayerMoveData
    // 현재 이동속도
    protected float moveSpeed;
    // 고정 이동속도
    protected float crouchSpeed = 2;
    protected float walkSpeed = 3;
    protected float runSpeed = 10;
    protected float jumpSpeed = 5;
    #endregion

    #region Get, Set
    public string Name { get { return name; } protected set { } }
    public int CurHP { get { return HP; } protected set { } }
    public int MaxHP { get { return maxHP; } protected set { } }
    public int CurATK { get { return ATK; } protected set { } }
    public int CurDEF { get { return DEF; } protected set { } }

    public float CurWalkSpeed { get { return crouchSpeed; } protected set { } }
    public float WalkSpeed { get { return walkSpeed;} protected set { } }
    public float RunSpeed { get { return runSpeed;} protected set { } }
    public float JumpSpeed { get { return jumpSpeed;} protected set { } }
    #endregion

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

}
