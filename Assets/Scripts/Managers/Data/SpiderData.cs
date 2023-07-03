using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderData : MonsterData
{
    #region Spider Data Init
    private void Awake()
    {
        name = "Spider";        // 이름 
        HP  = 100;                     // 체력
        maxHP = 500;                  // 최대 체력
        ATK = 15;                     // 공격 
        DEF = 10;                      // 방어 

        isAttack = false;        // defalt false
        attackMoveDelay = 1f;   // defalt 1f
        attackMoveCool = 1f;    // defalt 1f

        moveSpeed = 2f;
        detectRange = 10f;
        attackRange = 3f;
    }
    #endregion
}
