using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �÷��̾� ������ �ֱ�!!!!
public class PlayerData : MonoBehaviour
{
    protected new string name = "Unity";
    protected int HP = 100;                       // ü��
    protected int maxHP = 100;                    // �ִ� ü��
    protected int ATK = 10;                      // ���� 
    protected int DEF = 5;                      // ��� 

    
    
    protected string Name { get { return name; } }
    protected int CurHP { get { return HP; } }
    protected int MaxHP { get { return maxHP; } }
    protected int CurATK { get { return ATK; } }
    protected int CurDEF { get { return DEF; } }
    
}
