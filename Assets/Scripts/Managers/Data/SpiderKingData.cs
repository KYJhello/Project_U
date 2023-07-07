using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderKingData : BossMonsterData
{
    #region SpiderKing Data Init
    private void Awake()
    {
        name = "SpiderKing";        // �̸� 
        HP = 5000;                     // ü��
        maxHP = 5000;                  // �ִ� ü��
        ATK = 30;                     // ���� 
        DEF = 10;                      // ��� 

        isAttack = false;        // defalt false
        attackMoveDelay = 3f;   // defalt 1f
        attackMoveCool = 3f;    // defalt 1f
        skillCool = 5f;
        skillDelay = 5f;

        moveSpeed = 5f;
        detectRange = 15f;
        attackRange = 3f;
        returnDistance = 30f;
    }
    #endregion
}
