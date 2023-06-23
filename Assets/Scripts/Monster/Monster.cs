using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    protected Animator animator;
    protected new Collider collider;
    protected new Renderer renderer;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
        renderer = GetComponent<Renderer>();
    }
    protected virtual void Die()
    {
        animator.SetBool("Die", true);
        collider.enabled = false;

        Destroy(gameObject, 3f);
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            Die();
        }
    }



}
