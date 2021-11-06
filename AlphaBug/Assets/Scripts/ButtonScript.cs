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
    [SerializeField]
    private string[] tags;
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
        for (int i = 0; i < tags.Length; i++)
        {
            if (collision.gameObject.tag == tags[i])
            {
                isStay = true;
                Debug.Log("Stay");
                if (clickerObj.transform.localPosition.y > buttonHolder.transform.localPosition.y)
                {
                    clickerObj.transform.localPosition -= new Vector3(0, (float)(speed * Time.deltaTime), 0);
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
                i = tag.Length;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        for (int i = 0; i < tags.Length; i++)
        {
            if (collision.gameObject.tag == tags[i])
            {
                isStay = false;
                buttonId = -1;
                doorManager.buttonOff();
                i = tags.Length;
            }
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
