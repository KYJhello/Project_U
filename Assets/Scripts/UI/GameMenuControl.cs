using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuControl : MonoBehaviour
{
    PlayerController player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void CloseMenu()
    {
        Time.timeScale = 1.0f;

        player.EnableInput();

        Destroy(gameObject);
    }
}
