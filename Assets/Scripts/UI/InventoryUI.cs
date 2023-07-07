using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : BaseUI
{
    InventoryData data;

    private void Awake()
    {
        base.Awake();
        data = GetComponent<InventoryData>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        gameObject.SetActive(true);
    }
    public override void OnDisable()
    {
        base.OnDisable();
        gameObject.SetActive(false);
    }
    private void ShowItems()
    {
        int index = 0;
        foreach(ItemData item in data.ItemDatas)
        {
            if(item != null)
            {
                images["ItemSlot" + index].sprite = item.GetSprite;
            }
        }
    }
    
}
