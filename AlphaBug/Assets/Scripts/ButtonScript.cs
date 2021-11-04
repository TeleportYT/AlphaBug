using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    private GameObject buttonObj;
    private GameObject clickerObj;
    private GameObject buttonHolder;
    private float movePlace;
    private bool isStay;
    public bool isBuggy;
    public enum direction { Left, Right, Up, Ground};
    public direction ButtonDirection;

    void Start()
    {
        isBuggy = false;
        buttonObj = this.gameObject;
        clickerObj = buttonObj.transform.Find("ButtonClick").gameObject;
        buttonHolder = buttonObj.transform.Find("ButtonHolder").gameObject;
        if ((ButtonDirection == direction.Ground) || (ButtonDirection == direction.Up))
        {
            movePlace = clickerObj.transform.position.y;
            Debug.Log("MovePlace : " + movePlace);
        }
        else if ((ButtonDirection == direction.Left) || (ButtonDirection == direction.Right))
        {
            movePlace = clickerObj.transform.position.z;
            Debug.Log("MovePlace : " + movePlace);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isStay = true;
            Debug.Log("Stay");
            if (ButtonDirection == direction.Ground && clickerObj.transform.position.y > buttonHolder.transform.position.y)
            {
                clickerObj.transform.position -= new Vector3(0, (float)(0.1 * Time.deltaTime) , 0);
            }
            else if (ButtonDirection == direction.Up && clickerObj.transform.position.y < buttonHolder.transform.position.y)
            {
                clickerObj.transform.position += new Vector3(0, (float)(0.1 * Time.deltaTime), 0);
            }
            else if (ButtonDirection == direction.Left && clickerObj.transform.position.z < buttonHolder.transform.position.z)
            {
                clickerObj.transform.position += new Vector3(0, 0, (float)(0.1 * Time.deltaTime));
            }

        }
        else
        {
            upButton();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isStay = false;
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
            if (ButtonDirection == direction.Ground || clickerObj.transform.position.y < movePlace)
            {
                clickerObj.transform.position += new Vector3(0, (float)(0.1 * Time.deltaTime), 0);
            }
            else if (ButtonDirection == direction.Up || clickerObj.transform.position.y > movePlace)
            { 
               clickerObj.transform.position -= new Vector3(0, (float)(0.1 * Time.deltaTime), 0);
            }
            else if (ButtonDirection == direction.Left && clickerObj.transform.position.z < movePlace)
            {
              clickerObj.transform.position -= new Vector3(0, 0, (float)(0.1 * Time.deltaTime));
            }
    }


}
