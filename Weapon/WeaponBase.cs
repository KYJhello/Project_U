using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { Main = 0, Sub, Knife}
// 무기 정보만 넣기!!!
public class WeaponBase : MonoBehaviour
{
    [SerializeField] protected WeaponType weaponType;
    [SerializeField] protected string weaponName;
    [SerializeField] protected int damage;
    [SerializeField] protected float range;
    [SerializeField, Range(0, 360)] protected float angle;

    public WeaponType GetWeaponType { get { return weaponType; } }
    public string WeaponName { get { return weaponName; } }
    public int Damage { get { return damage; } }
    public float Range { get { return range; } }
    public float Angle { get { return angle; } }
}
