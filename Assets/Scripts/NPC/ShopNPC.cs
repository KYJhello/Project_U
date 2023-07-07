using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : MonoBehaviour
{
    public RectTransform uiGroup;
    public Animator anim;

    PlayerInventory inventory;
    
    public void Enter()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        uiGroup.anchoredPosition = Vector3.zero;
    }
    public void Exit()
    {
        uiGroup.anchoredPosition = Vector3.down *1000;
    }
    
}
