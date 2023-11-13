using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private static SceneManager sceneManager;
    private static ResourceManager resourceManager;
    private static PoolManager poolManager;

    public static GameManager Instance { get { return instance; } }
    public static SceneManager Scene { get {  return sceneManager; } }
    public static ResourceManager Resource {  get { return resourceManager; } }
    public static PoolManager Pool { get { return poolManager; } }

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

        GameObject resourceObj = new GameObject();
        resourceObj.name = "ResourceManager";
        resourceObj.transform.parent = transform;
        resourceManager = resourceObj.AddComponent<ResourceManager>();

        GameObject poolObj = new GameObject();
        poolObj.name = "PoolManager";
        poolObj.transform.parent = transform;
        poolManager = poolObj.AddComponent<PoolManager>();

    }
}
