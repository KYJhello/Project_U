using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : WeaponBase
{
    [SerializeField] protected int curBulletNum;                      // 현 장전된 탄창 수
    [SerializeField] protected int magazineCapacity;                  // 탄창당 장탄수
    [SerializeField] protected int ammoMax;                           // 소유가능한 최대 탄약수
    [SerializeField] protected float lowDMG_distance;                 // 데미지 감소 시작 거리



    

    public void Awake()
    {
        
    }

    public void Fire()
    {
        
    }


    // 보조무기인지 주 무기인지 런타임에서 변경 되기에 퍼블릭으로 설정 
    public void SetWeaponType(WeaponType wt)
    {
        weaponType = wt;
    }
}
