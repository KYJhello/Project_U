using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : MonoBehaviour
{
    private LoadingUI loadingUI;

    private BaseScene curScene;
    public BaseScene CurScene
    {
        get { 
            if(curScene == null)
                curScene = GameObject.FindObjectOfType<BaseScene>();
            
            return curScene; 
        }
    }
    private void Awake()
    {
        LoadingUI loadingUI = Resources.Load<LoadingUI>("UI/LoadingUI");   
        this.loadingUI = Instantiate(loadingUI);
        this.loadingUI.transform.SetParent(transform, false);
    }

    private void Start()
    {
        this.loadingUI.enabled = false;
    }
    public void LoadScene(string sceneName, bool isLoading)
    {
        if (isLoading)
        {
            StartCoroutine(LoadingRoutine(sceneName));
        }
        else
        {
            AsyncOperation oper = UnitySceneManager.LoadSceneAsync(sceneName);
        }
    }
    IEnumerator LoadingRoutine(string sceneName)
    {
        loadingUI.enabled = true;
        loadingUI.SetProgress(0.1f);
        loadingUI.FadeIn();
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0f;

        AsyncOperation oper = UnitySceneManager.LoadSceneAsync(sceneName);
        while(!oper.isDone)
        {
            loadingUI.SetProgress(Mathf.Lerp(0.0f, 0.5f, oper.progress));
            yield return null;
        }
        if (CurScene != null)
        {
            CurScene.LoadAsync();
            while (CurScene.progress < 1f)
            {
                loadingUI.SetProgress(Mathf.Lerp(0.0f, 0.5f, oper.progress));
                yield return null;
            }
        }

        loadingUI.SetProgress(1f);
        loadingUI.FadeOut();
        Time.timeScale = 1f;
        yield return new WaitForSeconds(3f);
    }

}
