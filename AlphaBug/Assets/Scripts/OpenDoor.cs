using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private float y;

    private void Start()
    {
        y = transform.localPosition.y;
    }
    public void OnOpen()
    {
        if (transform.localPosition.y < (y+transform.lossyScale.y)*5)
        {
            transform.position += new Vector3(0, (float)(0.1 * Time.deltaTime), 0);
        }
    }

    public void OnClose()
    {
        if (transform.localPosition.y > y)
        {
            transform.position -= new Vector3(0, (float)(0.1 * Time.deltaTime), 0);
        }
    }

}
