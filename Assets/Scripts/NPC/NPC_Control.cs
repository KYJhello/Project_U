using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Control : MonoBehaviour
{
    Canvas canvas;

    public void OnConversation()
    {
        if(canvas == null)
        {
            canvas = GameManager.Resource.Instantiate<Canvas>("UI/ConversationCanvas", false);
        }
    }
}
