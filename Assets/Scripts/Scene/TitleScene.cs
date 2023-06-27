using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    private void Awake()
    {
        
    }

    protected override IEnumerator LoadingRoutine()
    {
        progress = 0.0f;
        yield return null;
        progress = 1.0f;
    }
    private void OnDestroy()
    {
        
    }
    public void OnStartButton()
    {
        GameManager.Scene.LoadScene("GameScene");
    }
}
