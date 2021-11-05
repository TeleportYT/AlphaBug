using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    private GameObject buttonObj;
    private GameObject clickerObj;
    private GameObject buttonHolder;
    public DoorManager doorManager;
    private float movePlace;
    private bool isStay;
    private int buttonId;
    public bool isBuggy;
    public float speed;

    void Start()
    {
        buttonId = -1;
        isBuggy = false;
        buttonObj = this.gameObject;
        clickerObj = buttonObj.transform.Find("ButtonClick").gameObject;
        buttonHolder = buttonObj.transform.Find("ButtonHolder").gameObject;
        movePlace = clickerObj.transform.localPosition.y;
        Debug.Log("MovePlace : " + movePlace);

    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isStay = true;
            Debug.Log("Stay");
            if (clickerObj.transform.localPosition.y > buttonHolder.transform.localPosition.y)
            {
                clickerObj.transform.localPosition -= new Vector3(0, (float)(speed * Time.deltaTime) , 0);
            }
            else
            {
                
                if (buttonId == -1)
                {
                    Debug.Log("Full and New");
                    buttonId = doorManager.buttonOn();
                }
                else
                {
                    Debug.Log("Full and Old");
                }
                

            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isStay = false;
            buttonId = -1;
            doorManager.buttonOff();
        }
    }

    private void Update()
    {
        if (isStay == false)
        {
            upButton();
        }
    }

    private void upButton()
    {
            Debug.Log("Exit");
            if (clickerObj.transform.localPosition.y < movePlace)
            {
              clickerObj.transform.localPosition += new Vector3(0, (float)(speed * Time.deltaTime), 0);
            }


    }


}
