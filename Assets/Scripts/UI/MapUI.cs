using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUI :BaseUI
{
    public override void OnEnable()
    {
        base.OnEnable();
        gameObject.SetActive(true);
    }
    public override void OnDisable()
    {
        base.OnDisable();
        gameObject.SetActive(false);
    }
}
