using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    [SerializeField] protected int id;
    [SerializeField] protected string name;
    [Multiline]
    [SerializeField] protected string tooltip;
    [SerializeField] protected Sprite iconSprite;
    [SerializeField] protected GameObject dropItemPrefab;

    public int ID { get { return id; } }
    public string Name { get { return name; } }
    public string Tooltip { get { return tooltip; } }
    public Sprite GetSprite { get { return iconSprite; } }
    public GameObject DropItemPrefab { get { return dropItemPrefab; } }

    public abstract Item CreateItem();
}
