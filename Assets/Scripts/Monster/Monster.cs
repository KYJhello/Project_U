using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public abstract class Monster : MonoBehaviour
{
    protected Animator animator;
    protected new Collider collider;
    protected new Renderer renderer;

    public UnityEvent<int> OnHpChanged;
    protected abstract void Attack();

    protected virtual void Die()
    {
        animator.SetBool("Die", true);
        collider.enabled = false;

        Destroy(gameObject, 3f);
    }

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
        renderer = GetComponent<Renderer>();
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
        }
    }



}
