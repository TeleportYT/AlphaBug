using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class activateOnInteract : MonoBehaviour
{

    [SerializeField] private UnityEvent onInteraction;

    public void onInteract()
    {
        if (onInteraction!=null)
        {
            Debug.Log("Invokeing " + onInteraction.ToString());
            onInteraction.Invoke();
        }
    }

}
