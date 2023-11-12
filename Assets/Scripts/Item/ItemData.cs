using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//flyWeight ���� ����� ���� ��ũ���ͺ� ������Ʈ

// ��, ��, ����, ����, ����, ��Ÿ
public enum ItemType { Ammo = 0, Tool, Portion, Weapon, Etc }
[CreateAssetMenu(fileName = "Item Data", menuName = "Scriptable Object/Item Data", order = int.MaxValue)]
public class ItemData : ScriptableObject
{
    [SerializeField]
    private ItemType Type;
    [SerializeField]
    private int id;
    [SerializeField]
    private int maxAmount;
    [SerializeField]
    private new string name;
    [SerializeField]
    private string tooltip;
    [SerializeField]
    private Sprite iconSprite;
    [SerializeField]
    private GameObject dropItemPrefab;

    public ItemType CurItemType { get { return Type; } }
    public int ID { get { return id; } }
    public int MaxAmount { get { return maxAmount; } }
    public string Name { get { return name; } }
    public string Tooltip { get { return tooltip; } }
    public Sprite GetSprite { get { return iconSprite; } }
    public GameObject DropItemPrefab { get { return dropItemPrefab; } }
}
