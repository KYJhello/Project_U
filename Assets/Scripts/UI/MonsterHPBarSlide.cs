using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MonsterHPBarSlide : MonoBehaviour
{
    [SerializeField] MonsterBase monster;

    private Slider slider;
    private TMP_Text[] infoText;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        infoText = GetComponentsInChildren<TMP_Text>();
    }
    private void Start()
    {
        slider.maxValue = monster.GetMaxHP();
        slider.value = monster.GetHP();
        infoText[0].text = monster.GetName();
        infoText[1].text = (monster.GetHP()).ToString() + " / " + (monster.GetMaxHP()).ToString();

        monster.OnHpChanged.AddListener(SetValue);
    }
    private void Update()
    {
        if(monster == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
        slider.value = monster.GetHP();
        infoText[1].text = (monster.GetHP()).ToString() + " / " + (monster.GetMaxHP()).ToString();

        monster.OnHpChanged.AddListener(SetValue);
    }
    public void SetValue(int value)
    {
        slider.value = value;
    }
}
