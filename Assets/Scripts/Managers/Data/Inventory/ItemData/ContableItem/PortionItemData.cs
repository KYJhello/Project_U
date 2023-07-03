using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemPortion", menuName = "Inventory/ItemData/Portion", order = 3)]
public class PortionItemData : CountableItemData
{
    [SerializeField] private float value;
    public float Value { get { return value; } }
    public override Item CreateItem()
    {
        return new PortionItem(this);
    }
}
