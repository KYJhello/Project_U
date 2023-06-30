using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItmeCanvasControl : MonoBehaviour
{
    public void OnOffControl()
    {
        if (gameObject.active)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
