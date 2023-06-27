using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �÷��̾� ������ �ֱ�!!!!
public class PlayerData : MonoBehaviour
{
    #region Player Info Data
    protected new string name = "Unity";
    protected int HP = 100;                       // ü��
    protected int maxHP = 100;                    // �ִ� ü��
    protected int ATK = 10;                      // ���� 
    protected int DEF = 5;                      // ��� 

    protected Animator animator;                // �ִϸ�����
    protected List<WeaponBase> weapon;          // ���� �����
    #endregion

    #region PlayerMoveData
    // ���� �̵��ӵ�
    protected float moveSpeed;
    // ���� �̵��ӵ�
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
