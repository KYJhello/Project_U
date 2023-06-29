using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �÷��̾� ������ �ֱ�!!!!
public class PlayerData : MonoBehaviour
{
    #region Player Info Data
    protected new string name = "Unity";        // �̸� 
    protected int HP = 100;                     // ü��
    protected int maxHP = 100;                  // �ִ� ü��
    protected int ATK = 10;                     // ���� 
    protected int DEF = 5;                      // ��� 

    protected List<WeaponBase> weapon;          // ���� �����
    protected Animator animator;                // �ִϸ�����
    #endregion

    #region PlayerMoveData
    // ���� �̵��ӵ�
    protected float moveSpeed;
    // ���� �̵��ӵ�
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
