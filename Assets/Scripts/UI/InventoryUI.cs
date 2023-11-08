using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : BaseUI
{
    [SerializeField]
    PlayerInventory playerInventory;
    //InventoryData data;

    private void Awake()
    {
        base.Awake();
        //data = GetComponent<InventoryData>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        gameObject.SetActive(true);
        ShowItems();
    }
    public override void OnDisable()
    {
        base.OnDisable();
        gameObject.SetActive(false);
    }
    private void ShowItems()
    {
        int index = 0;
        foreach(Item item in playerInventory.items)
        {
            if(item != null)
            {
                images["ItemSlot (" + index + ")"].sprite = item.data.GetSprite;
            }
        }
    }
    
}
