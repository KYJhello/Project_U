using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountableItem : Item
{
    public int Amount { get; protected set; }
    public int MaxAmount => data.MaxAmount;
    public bool IsMax => Amount >= data.MaxAmount;
    public bool IsEmpty => Amount <= 0;

    //protected CountableItem(int amount = 1)
    //{
    //    SetAmount(amount);
    //}
    //public void SetAmount(int amount)
    //{
    //    Amount = Mathf.Clamp(amount, 0, MaxAmount);
    //}
    //public int AddAmount(int amount)
    //{
    //    int nextAmount = Amount + amount;
    //    SetAmount(nextAmount);

    //    return (nextAmount > MaxAmount) ? (nextAmount - MaxAmount) : 0;
    //}
    //protected abstract CountableItem Clone(int amount);
    public override bool IsCountable()
    {
        return true;
    }
}
