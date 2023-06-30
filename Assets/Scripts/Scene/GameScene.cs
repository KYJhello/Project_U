using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : BaseScene
{
    private Canvas gameMenuCanvas;
    protected override IEnumerator LoadingRoutine()
    {
        progress = 0.0f;
        yield return null;
        progress = 1.0f;
    }

    public void OnMenuButton()
    {
        if (gameMenuCanvas == null) {
            gameMenuCanvas = GameManager.Resource.Instantiate<Canvas>("UI/GameMenuCanvas");
        }
    }
    public void GamePause()
    {
        Time.timeScale = 0.0f;
    }
    public void GamePlay()
    {
        Time.timeScale = 1.0f;
    }
}
