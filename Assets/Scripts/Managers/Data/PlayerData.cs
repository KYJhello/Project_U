using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �÷��̾� ������ �ֱ�!!!!
public class PlayerData : MonoBehaviour
{
    protected new string name;
    protected int HP;                       // ü��
    protected int maxHP;                    // �ִ� ü��
    protected int ATK;                      // ���� 
    protected int DEF;                      // ��� 
    
    protected string Name { get { return name; } }
    protected int CurHP { get { return HP; } }
    protected int MaxHP { get { return maxHP; } }
    protected int CurATK { get { return ATK; } }
    protected int CurDEF { get { return DEF; } }
    
}
