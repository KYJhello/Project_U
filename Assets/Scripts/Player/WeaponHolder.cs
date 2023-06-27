using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] public WeaponBase weapon;
    private void Attack()
    {
        if (weapon.GetWeaponType == WeaponType.Gun)
        {
            Fire();
        }
        else
        {

        }
    }
    private void Fire()
    {
        Gun gun = (Gun)weapon;
        gun.Fire();
    }
}
