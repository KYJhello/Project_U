using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryData : MonoBehaviour
{
    protected List<ItemData> itemDatas;
    protected int maxCapacity = 28;
    public List<ItemData> ItemDatas { get { return itemDatas; }  }
    public int MaxCapacity { get { return maxCapacity; } }


    private void Awake()
    {
        itemDatas = new List<ItemData>();
    }
    public void AddItem(ItemData item)
    {
        itemDatas.Add(item);
    }
}
