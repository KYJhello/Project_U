using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 플레이어 정보만 넣기!!!!
public class PlayerData : MonoBehaviour
{
    protected new string name;
    protected int HP;                       // 체력
    protected int maxHP;                    // 최대 체력
    protected int ATK;                      // 공격 
    protected int DEF;                      // 방어 
    
    protected string Name { get { return name; } }
    protected int CurHP { get { return HP; } }
    protected int MaxHP { get { return maxHP; } }
    protected int CurATK { get { return ATK; } }
    protected int CurDEF { get { return DEF; } }
    
}
