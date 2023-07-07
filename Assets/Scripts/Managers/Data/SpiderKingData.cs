using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderKingData : BossMonsterData
{
    #region SpiderKing Data Init
    private void Awake()
    {
        name = "SpiderKing";        // 이름 
        HP = 5000;                     // 체력
        maxHP = 5000;                  // 최대 체력
        ATK = 30;                     // 공격 
        DEF = 10;                      // 방어 

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
