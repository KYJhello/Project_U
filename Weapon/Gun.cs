using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : WeaponBase
{
    [SerializeField] protected int curBulletNum;                      // �� ������ źâ ��
    [SerializeField] protected int magazineCapacity;                  // źâ�� ��ź��
    [SerializeField] protected int ammoMax;                           // ���������� �ִ� ź���
    [SerializeField] protected float lowDMG_distance;                 // ������ ���� ���� �Ÿ�



    

    public void Awake()
    {
        
    }

    public void Fire()
    {
        
    }


    // ������������ �� �������� ��Ÿ�ӿ��� ���� �Ǳ⿡ �ۺ����� ���� 
    public void SetWeaponType(WeaponType wt)
    {
        weaponType = wt;
    }
}
