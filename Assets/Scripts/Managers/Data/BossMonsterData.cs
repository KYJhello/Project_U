using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterData : MonsterData
{
    protected float skillDelay;
    protected float skillCool;
    protected float returnDistance;

    public float SkillDelay { get { return skillDelay; } set { skillDelay = value; } }
    public float SkillCool { get { return skillCool; } protected set { skillCool = value; } }
    public float ReturnDistance { get { return returnDistance; } }

}
