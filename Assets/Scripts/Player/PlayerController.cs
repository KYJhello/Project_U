using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IHittable, IHealable
{
    private int curDamage;

    private PlayerData data;
    private PlayerMover mover;
    private PlayerAttacker attacker;
    private PlayerInteractor interactor;
    private PlayerInventory inventory;
    private ThirdCamController camController;
    public UnityEvent<int> OnHit;
    
    private Coroutine hitRoutine;
    //private Coroutine reloadRoutine;

    public void EnableInput()
    {
        gameObject.GetComponent<PlayerInput>().enabled = true;
        gameObject.GetComponent<ThirdCamController>().enabled = true;
    }

    private void Awake()
    {
        data = GetComponent<PlayerData>();
        mover = GetComponent<PlayerMover>();
        attacker = GetComponent<PlayerAttacker>();
        interactor = GetComponent<PlayerInteractor>();
        inventory = GetComponent<PlayerInventory>();
        camController = GetComponent<ThirdCamController>();

        data.hitDelay = data.hitCool;
    }
    private void OnEnable()
    {
        hitRoutine = StartCoroutine(HitRoutine());
    }
    private void OnDisable()
    {
        StopCoroutine(hitRoutine);
    }
    public void Heal(int heal)
    {
        data.CurHP = (data.CurHP + heal >= data.MaxHP) ? (data.MaxHP) : (data.CurHP + heal);
    }

    private bool IsDie()
    {
        if(data.CurHP <= 0)
        {
            return true;
        }
        return false;
    }
    private void Die()
    {
        data.Anim.SetTrigger("Death");
        mover.enabled = false;
        attacker.enabled = false;
        interactor.enabled = false;
        camController.enabled = false;

        OnDisable();
    }
    public void TakeHit(int damage)
    {
        if(data.isRoll)
        {
            curDamage = 0;
            return;
        }
        curDamage = CalDamage(damage);
        //Debug.Log(curDamage);
        data.isHit = true;
    }
    private int CalDamage(int damage)
    {
        if((damage - data.CurDEF) < 0)
        {
            return 0;
        }
        else
        {
            return damage - data.CurDEF;
        }
    }

    private void OnReload(InputValue value)
    {
        data.Anim.SetTrigger("Reload");
    }
    private void OnAlt(InputValue value)
    {
        if (value.isPressed)
        {
            camController.OnDisable();
            camController.enabled = false;
            attacker.enabled = false;
        }
        else
        {
            if (camController.enabled)
            {
                camController.OnEnable();
                camController.enabled = true;
                attacker.enabled = true;
            }
        }
    }

    IEnumerator HitRoutine()
    {
        while (true)
        {
            if (data.isHit && data.hitDelay > 0)
            {
                if(data.hitDelay == data.hitCool)
                {
                    //mover.enabled = false;
                    Debug.Log("getDamage");
                    data.CurHP -= curDamage;
                    Debug.Log("Cur HP : " + data.CurHP);
                    if (IsDie())
                    {
                        Die();
                    }
                    else
                    {
                        data.Anim.SetTrigger("Hit");
                    }
                }
                data.hitDelay -= Time.deltaTime;
                if (data.hitDelay <= 0 && data.isHit)
                {
                    data.isHit = false;
                    data.hitDelay = data.hitCool;
                    //mover.enabled = true;
                }
                
            }
            yield return null;
        }
    }

    



}
