using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentArea : MonoBehaviour
{
    InventoryUI inven;
    SoundUI sound;
    MapUI map;

    private void Awake()
    {
        inven = GetComponentInChildren<InventoryUI>();
        sound = GetComponentInChildren<SoundUI>();
        map = GetComponentInChildren<MapUI>();
    }
    private void Start()
    {
        AreaDisable();
    }

    public void ActiveControl(int key)
    {
        switch(key)
        {
            case 0:
                inven.OnEnable();
                sound.OnDisable();
                map.OnDisable();
                break;
            case 1:
                inven.OnDisable();
                sound.OnEnable();
                map.OnDisable();
                break;
            case 2:
                inven.OnDisable();
                sound.OnDisable();
                map.OnEnable();
                break;
        }
    }
    public void AreaEnable()
    {
        inven.OnEnable();
        sound.OnEnable();
        map.OnEnable();
    }
    public void AreaDisable()
    {
        inven.OnDisable();
        sound.OnDisable();
        map.OnDisable();
    }
}
