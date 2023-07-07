using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class MonsterBase : MonoBehaviour, IHittable
{

    protected new string name;

    protected int HP;                       // ü��
    protected int maxHP;                    // �ִ� ü��
    protected int ATK;                      // ���ݷ�
    protected int DEF;                  // ����

    public string GetName() { return name; }
    public int GetHP() { return HP; }
    public int GetMaxHP() { return maxHP; }
    public int GetATK() { return ATK; }
    public int GetDefense() { return DEF; }

    public UnityEvent<int> OnHpChanged;


    protected abstract void Attack();
    public void TakeHit(int damage)
    {
        HP -= (damage - DEF);
        if (HP <= 0)
        {

        }
    }







}
