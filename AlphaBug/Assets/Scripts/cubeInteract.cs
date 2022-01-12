using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeInteract : MonoBehaviour
{
    private GameObject plCam;
    private Vector3 pos = new Vector3(0, -0.357f, 1.45f);
    private Quaternion qr = new Quaternion(0, 0, 0, 0);
    [SerializeField]
    [Range(1,10)]
    private float throwPower;
    [Space(2)]
    [SerializeField]
    [Range(1,10)]
    private float powerIncrese;
    private Rigidbody rb;
    [SerializeField]
    [Space(2)]
    private bool isHolding;
    [SerializeField] public ButtonScript onButton;

    private void Start()
    {
        bool isFound = false;
        while (!isFound)
        {
            plCam = GameObject.Find("Player Camera");
            if (plCam!=null)
            {
                isFound = true;
                break;
            }
        }
    }

    public void EnableRagdoll()
    {
        transform.SetParent(null);
        gameObject.AddComponent<Rigidbody>();
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward*throwPower*powerIncrese);
        onButton = null;

    }
    public void DisableRagdoll()
    {
        if (onButton != null)
        {
            onButton.takingObject(gameObject.tag);
        }
        rb = GetComponent<Rigidbody>();
        Destroy(rb);
        transform.SetParent(plCam.transform, false);

        transform.localPosition = pos;
        transform.localRotation = qr;
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
