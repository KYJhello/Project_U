using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonCountableItem : Item
{
    public override bool IsCountable()
    {
        return false;
    }
}
