using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuContentAreaUI : MonoBehaviour
{
    Dictionary<string, GameObject> contentDic = new Dictionary<string, GameObject>();

    private void Awake()
    {
        contentDic.Clear();
        contentDic.Add("InventoryUI",       GameManager.Resource.Instantiate<GameObject>("UI/InventoryUI", transform));
        contentDic.Add("SoundManagerUI",    GameManager.Resource.Instantiate<GameObject>("UI/SoundManagerUI", transform));
        contentDic.Add("MapUI",             GameManager.Resource.Instantiate<GameObject>("UI/MapUI", transform));
    
        
    }
}
