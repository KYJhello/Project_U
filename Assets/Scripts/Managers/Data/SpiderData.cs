using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderData : MonsterData
{
    #region Spider Data Init
    private void Awake()
    {
        name = "Spider";        // �̸� 
        HP  = 100;                     // ü��
        maxHP = 500;                  // �ִ� ü��
        ATK = 15;                     // ���� 
        DEF = 10;                      // ��� 

        isAttack = false;        // defalt false
        attackMoveDelay = 1f;   // defalt 1f
        attackMoveCool = 1f;    // defalt 1f

        moveSpeed = 2f;
        detectRange = 10f;
        attackRange = 3f;
    }
    #endregion
}
