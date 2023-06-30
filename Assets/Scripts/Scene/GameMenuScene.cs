using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuScene : BaseScene
{
    protected override IEnumerator LoadingRoutine()
    {
        progress = 0.0f;
        yield return null;
        progress = 1.0f;
    }
    public void OnGameButton()
    {
        GameManager.Scene.LoadScene("GameScene", false);
    }
    private void OnESC(InputValue value)
    {
        OnGameButton();
    }
}
