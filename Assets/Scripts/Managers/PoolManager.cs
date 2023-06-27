using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    Dictionary<string, ObjectPool<GameObject>> pooldic;
    Dictionary<string, Transform> poolContainer;
    Transform poolRoot;
    Canvas canvasRoot;

    private void Awake()
    {
        pooldic = new Dictionary<string, ObjectPool<GameObject>>();
        poolContainer = new Dictionary<string, Transform>();
        poolRoot = new GameObject("PoolRoot").transform;
        //canvasRoot = GameManager.Resource.Instantiate<Canvas>("UI/canvas");
    }

}
