using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonScript : MonoBehaviour
{
    [SerializeField]
    [Header("Piston Platform")]
    private GameObject pistonObj;
    [Space(2)]
    [SerializeField]
    [Header("Max Height")]
    private float maxHeight;
    [Space(2)]
    [SerializeField]
    [Header("Start Y")]
    private float y;
    [Space(2)]
    [SerializeField]
    [Header("Speed")]
    [Range(0,5)]
    private float speed;
    void Start()
    {
        pistonObj = gameObject.transform.Find("Piston Platform").gameObject;
        y = pistonObj.transform.localPosition.y;
    }

    [ContextMenu("onPiston")]
    public void onPiston()
    {
        if (pistonObj.transform.localPosition.y < maxHeight)
        {
            pistonObj.transform.position += new Vector3(0, (float)(speed * Time.deltaTime), 0);
        }
    }

    public void offPiston()
    {
        if (pistonObj.transform.localPosition.y > y)
        {
            pistonObj.transform.position -= new Vector3(0, (float)(speed * Time.deltaTime), 0);
        }
    }
}
