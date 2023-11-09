using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : Inventory
{
    public PortionItem por;

    private void Start()
    {
        items.Add(por);
    }
}
