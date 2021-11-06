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
    [Space(2)]
    [SerializeField]
    [Header("Max height")]
    [Range(0, 10)]
    private float maxHeight;

    private void Start()
    {
        y = transform.localPosition.y;
    }
    public void OnOpen()
    {
        if (transform.localPosition.y < maxHeight)
        {
            transform.localPosition += new Vector3(0, (float)(speed * Time.deltaTime), 0);
        }
    }

    public void OnClose()
    {
        if (transform.localPosition.y > y)
        {
            transform.localPosition -= new Vector3(0, (float)(speed * Time.deltaTime), 0);
        }
    }

}
