using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortionItem : CountableItem
{
    public PortionItem(CountableItemData data, int amount = 1) : base(data, amount)
    {
    }

    protected override CountableItem Clone(int amount)
    {
        return new PortionItem(CountableData as PortionItemData, amount);
    }
}
