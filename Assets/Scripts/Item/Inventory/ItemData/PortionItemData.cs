using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortionItemData : CountableItemData
{
    [SerializeField] private float value;
    public float Value { get { return value; } }

}
