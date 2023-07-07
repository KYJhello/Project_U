using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;


public class MonsterHPBarSlide : MonoBehaviour, IBarUI
{
    MonsterData myData;

    private Slider slider;
    private TMP_Text[] infoText;
    private Image[] images;


    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        infoText = GetComponentsInChildren<TMP_Text>();
        images = GetComponentsInChildren<Image>();
        SetEnable(false);
    }
    private void Update()
    {
        if(myData != null)
        {
            if (myData.CurHP <= 0)
            {
                SetEnable(false);
            }

            slider.maxValue = myData.MaxHP;
            slider.value = myData.CurHP;
            infoText[0].text = myData.Name;
            infoText[1].text = (myData.CurHP).ToString() + " / " + (myData.MaxHP).ToString();
        }
    }
    public void SetValue(int value)
    {
        slider.value = value;
    }
    public void SetData(MonsterData data)
    {
        SetEnable(true);
        myData = data;
    }
    public void SetEnable(bool state)
    {
        slider.enabled = state;
        foreach (var item in infoText)
        {
            item.enabled = state;
        }
        foreach (var item in images)
        {
            item.enabled = state;
        }
    }
}
