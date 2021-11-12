using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeInteract : MonoBehaviour
{
    public GameObject plCam;
    private Vector3 pos = new Vector3(0, 0, 1.45f);
    public Rigidbody rb;
    public bool isHolding;
    void Start()
    {
    }
    public void EnableRagdoll()
    {
        transform.SetParent(null);
        //Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //transform.position = newPos;
        gameObject.AddComponent<Rigidbody>();
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward*300f);

    }
    public void DisableRagdoll()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(rb);
        transform.SetParent(plCam.transform, false);
        Quaternion qr = new Quaternion(0, 0, 0, 0);
        transform.localPosition = pos;
        transform.localRotation = qr;
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
