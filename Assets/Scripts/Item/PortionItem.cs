using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortionItem : CountableItem
{
    GameObject player;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void ReturnItem()
    {
        player.GetComponent<PlayerInventory>().AddItem(this);
        Destroy(this.gameObject);
    }
}
