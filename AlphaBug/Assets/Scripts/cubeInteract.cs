using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeInteract : MonoBehaviour
{
    public GameObject plCam;
    private Vector3 pos = new Vector3(0, -0.39f, 1.45f);
    public Rigidbody rb;
    public bool isHolding;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void EnableRagdoll()
    {
        gameObject.AddComponent<Rigidbody>();
        transform.SetParent(null, false);
        Vector3 newPos = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();


    }
    public void DisableRagdoll()
    {
        Destroy(rb);
        transform.SetParent(plCam.transform, false);
        transform.localPosition = pos;
    }

    private void Update()
    {
        plCam = GameObject.Find("Player Camera");
    }
    public void interact()
    {
        if (isHolding)
        {
            EnableRagdoll();
            isHolding = false;
        }
        else
        {
            DisableRagdoll();
            isHolding = true;
        }
    }
}
