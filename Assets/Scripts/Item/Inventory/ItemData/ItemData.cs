using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public enum ItemType { Ammo, Coin, Grenade, Portion, Weapon}
    public ItemType Type;
    protected int id;
    protected new string name;
    protected string tooltip;
    protected Sprite iconSprite;
    protected GameObject dropItemPrefab;

    public int ID { get { return id; } }
    public string Name { get { return name; } }
    public string Tooltip { get { return tooltip; } }
    public Sprite GetSprite { get { return iconSprite; } }
    public GameObject DropItemPrefab { get { return dropItemPrefab; } }
}
