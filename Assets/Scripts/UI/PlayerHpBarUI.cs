using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHpBarUI : MonoBehaviour
{
    private PlayerData playerData;
    private int tempHP;

    private Slider slider;
    private TMP_Text[] infoText;

    private void Awake()
    {
        playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();

        slider = GetComponentInChildren<Slider>();
        infoText = GetComponentsInChildren<TMP_Text>();
    }

    private void Update()
    {
        if(playerData.CurHP != tempHP)
        {
            OnHPChanged();
            tempHP = playerData.CurHP;
        }
    }
    private void Start()
    {
        slider.maxValue = playerData.MaxHP;
        tempHP = playerData.CurHP;
        slider.value = tempHP;


        infoText[0].text = playerData.Name;
        infoText[1].text = playerData.CurHP + " / " + playerData.MaxHP;
    }
    public void OnHPChanged()
    {
        Debug.Log("플레이어 피 변경 호출됨");
        slider.value = playerData.CurHP;

        infoText[0].text = playerData.Name;
        infoText[1].text = playerData.CurHP + " / " + playerData.MaxHP;
    }
}
