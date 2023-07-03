using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData : MonoBehaviour
{
    #region Monster Info Data
    protected new string name = "MonsterDefaltName";        // �̸� 
    protected int HP;                     // ü��
    protected int maxHP;                  // �ִ� ü��
    protected int ATK;                     // ���� 
    protected int DEF;                      // ��� 

    protected bool isAttack = false;        // defalt false
    protected float attackMoveDelay = 1f;   // defalt 1f
    protected float attackMoveCool = 1f;    // defalt 1f

    protected float moveSpeed;
    protected float detectRange;
    protected float attackRange;
    #endregion

    #region Get, Set
    public string Name { get { return name; } }
    public int CurHP { get { return HP; } set { HP = value; } }
    public int MaxHP { get { return maxHP; } }
    public int CurATK { get { return ATK; } }
    public int CurDEF { get { return DEF; } }

    public bool IsAttack { get { return isAttack; } set { isAttack = value; } }
    public float AttackMoveDelay { get { return attackMoveDelay; } set { attackMoveDelay = value; } }
    public float AttackMoveCool { get { return attackMoveCool; } }
    public float MoveSpeed { get { return moveSpeed; } }
    public float DetectRange { get { return detectRange; } }
    public float AttackRange { get { return attackRange; } }

    #endregion

}
