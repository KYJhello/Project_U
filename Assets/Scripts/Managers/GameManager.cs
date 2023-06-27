using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private static SceneManager sceneManager;

    public static GameManager Instance { get { return instance; } }
    public static SceneManager Scene { get {  return sceneManager; } }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
        InitManager();
    }
    private void OnDestroy()
    {
        if(instance == this)
        {
            instance = null;    
        }
    }
    private void InitManager()
    {
        GameObject sceneObj = new GameObject();
        sceneObj.name = "SceneManager";
        sceneObj.transform.parent = transform;
        sceneManager = sceneObj.AddComponent<SceneManager>();
    }
}
