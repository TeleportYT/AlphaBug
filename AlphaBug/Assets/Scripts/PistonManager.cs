using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonManager : MonoBehaviour
{
    [Header("Piston Manager")]
    [SerializeField]
    private int onNum;
    [Space(2)]
    [SerializeField]
    private int needToOpen;
    [Space(2)]
    [SerializeField]
    private PistonScript pistonScript;

    void Update()
    {
        if (onNum == needToOpen)
        {
            pistonScript.onPiston();
        }
        else
        {
            pistonScript.offPiston();
        }
    }

    public int buttonOn()
    {
        if (onNum <= needToOpen)
        {
            onNum++;
        }
        return onNum;
    }

    public void buttonOff()
    {
        if (onNum - 1 >= 0)
        {
            onNum--;
        }

    }
}
