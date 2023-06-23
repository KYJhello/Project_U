using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { Gun = 0, Knife, Throws }

[CreateAssetMenu (fileName = "WeaponData", menuName = "Data/Weapon")]
// 원본 데이터와 사용데이터 나누기 위한 ISerializationCallbackReceiver 인터페이스 사용
public class WeaponData : ScriptableObject, ISerializationCallbackReceiver
{
    // 기본 설정
    [SerializeField] protected WeaponType weaponType;
    [SerializeField] protected string weaponName;
    [SerializeField] protected int damage;
    [SerializeField] protected float range;
    [SerializeField, Range(0, 360)] protected float angle;

    // 실 사용을 위한 퍼블릭 변수들
    [SerializeField] public WeaponType copy_weaponType;
    [SerializeField] public string copy_weaponName;
    [SerializeField] public int copy_damage;
    [SerializeField] public float copy_range;
    [SerializeField, Range(0, 360)] public float copy_angle;

    public void OnBeforeSerialize()
    {
        
    }
    public void OnAfterDeserialize()
    {
        //copyData = trueData;
        copy_weaponType = weaponType;
        copy_weaponName = weaponName;
        copy_damage = damage;
        copy_range = range;
        copy_angle = angle;
    }

    
}
