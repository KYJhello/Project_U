using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationControl : MonoBehaviour
{
    public void OnDestroy()
    {
        Destroy(this.gameObject);
    }
}
