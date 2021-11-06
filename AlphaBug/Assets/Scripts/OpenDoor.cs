using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [Space(2)]
    [SerializeField]
    [Header("Start Y")]
    private float y;
    [SerializeField]
    [Header("Speed")]
    [Range(0, 5)]
    private float speed;

    private void Start()
    {
        y = transform.localPosition.y;
    }
    public void OnOpen()
    {
        if (transform.localPosition.y < (y+transform.lossyScale.y)*5)
        {
            transform.position += new Vector3(0, (float)(speed * Time.deltaTime), 0);
        }
    }

    public void OnClose()
    {
        if (transform.localPosition.y > y)
        {
            transform.position -= new Vector3(0, (float)(speed * Time.deltaTime), 0);
        }
    }

}
