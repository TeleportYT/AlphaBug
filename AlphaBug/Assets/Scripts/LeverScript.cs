using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverScript : MonoBehaviour
{
    [SerializeField] private bool isTrue;
    [SerializeField] private GameObject lever;
    [SerializeField] private UnityEvent onStatus, offStatus;
    void Start()
    {
        lever = gameObject.transform.Find("Lever").gameObject;
    }

    public void changeLever()
    {
        if (isTrue)
        {
            offStatus.Invoke();
            isTrue = false;
            if (lever.transform.localRotation.z == 0)
            {
                lever.transform.Rotate(0, 0, -45);
            }
            else
            {
                lever.transform.Rotate(0, 0, -90);
            }
        }
        else
        {
            onStatus.Invoke();
            isTrue = true;
            if (lever.transform.localRotation.z == 0)
            {
                lever.transform.Rotate(0, 0, 45);
            }
            else
            {
                lever.transform.Rotate(0, 0, 90);
            }
        }
    }

}
