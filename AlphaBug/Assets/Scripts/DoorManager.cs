using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public int onNum;
    public int needToOpen;
    public OpenDoor door;
    public bool open;

    private void Start()
    {
        onNum = 0;
    }
    void Update()
    {
        if (onNum == needToOpen)
        {
            door.OnOpen();
        }
        else
        {
            door.OnClose();
        }
    }

    public int buttonOn()
    {
        open = true;
        if (onNum<=needToOpen)
        {
            onNum++;
        }
        return onNum;
    }

    public void buttonOff()
    {
        onNum--;
    }
}
