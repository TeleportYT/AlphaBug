using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public int onNum;
    public int needToOpen;
    public OpenDoor door;

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

    public void buttonOn()
    {
        if (onNum<=needToOpen)
        {
            onNum++;
        }
    }

    public void buttonOff()
    {
        if (onNum-1>=0)
        {
            onNum--;
        }
      
    }
}
