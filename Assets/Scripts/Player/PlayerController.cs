using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : PlayerData
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void NowChangeWeapon(WeaponType weaponType)
    {
        anim.SetInteger("CurWeapon", (int)weaponType);
    }

}
