using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameMenuControl : PopUpUI
{
    PlayerController player;
    PlayerData playerData;
    ContentArea contentArea;
    //Image[] images;
    //TMP_Text[] texts;
    

    private void Awake()
    {
        base.Awake();
        //images = GetComponentsInChildren<Image>();
        //texts = GetComponentsInChildren<TMP_Text>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();
        contentArea = GetComponentInChildren<ContentArea>();

        buttons["Inventory"].onClick.AddListener(() => { contentArea.ActiveControl(0); });
        buttons["Sound"].onClick.AddListener(() => { contentArea.ActiveControl(1); });
        //buttons["Map"].onClick.AddListener(() => { contentArea.ActiveControl(2); });
    }
    private void Start()
    {
        OnDisable();
    }
    public void CloseMenu()
    {
        Time.timeScale = 1.0f;

        player.EnableInput();

        OnDisable();
    }
    public override void OnEnable()
    {
        base.OnEnable();
        contentArea.AreaDisable();
    }
    public override void OnDisable()
    {
        base.OnDisable();
        contentArea.AreaDisable();
    }
}
