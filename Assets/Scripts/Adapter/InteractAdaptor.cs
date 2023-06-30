using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractAdaptor : MonoBehaviour, IInteractable
{
    public UnityEvent OnInvoked;

    public void Interact()
    {
        Debug.Log("interact");
        OnInvoked?.Invoke();
    }
}
