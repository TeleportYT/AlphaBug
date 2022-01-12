using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class changeTo : MonoBehaviour
{


    #region Ray Settings
    [Header("Ray Settings")]
    [Space(2)] [SerializeField] [Range(0, 10)] private int rayLength;
    [SerializeField] [Space(2)] private LayerMask layerMaskInteract;
    [SerializeField] [Space(2)] private string exludeLayerName = null;
    #endregion

    #region Interaction Settings
    [Space(5)]
    [Header("Intecation Settings")]
    [SerializeField] [Space(2)] private KeyCode interactableKey;
    [SerializeField] [Space(2)] private KeyCode changeObjectKey;
    [SerializeField] [Space(2)] private string[] interactableTags;
    #endregion

    #region Change to Settings
    [Header("Change to Settings")]
    [SerializeField] [Space(2)] private GameObject[] changeToObject;
    private int objectNum = 0;
    #endregion

    #region Save Veriables
    private Vector3 savePosition;
    private Quaternion saveRotation;
    private Vector3 saveScale;
    #endregion

    #region Connect To Settings
    [SerializeField] [Space(2)] private KeyCode connnectToKey;
    [SerializeField] [Space(2)] private UnityAction actionIn=null,actionOut=null;
    [SerializeField] [Space(2)] private UnityEvent eventIn =null,eventOut = null;
    [SerializeField] [Space(2)] private bool inConnection = false,eventConnected = false;
    [SerializeField] [Space(2)] private string[] connectingTags;
    #endregion
    private void Update()
    {
        #region Change To
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        int mask = 1 << LayerMask.NameToLayer(exludeLayerName) | layerMaskInteract.value;
        Debug.Log("mask : " + mask);
        Debug.Log("test : " + Physics.Raycast(transform.position, fwd, out hit, rayLength, mask));
        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, mask))
        {
            Debug.Log(hit.collider.tag);
            int i = 0,j=0;
            bool isFound = false;
            while ((i < interactableTags.Length || j<connectingTags.Length) && isFound == false)
            {
                Debug.Log("While Works");
                if (i < interactableTags.Length && hit.collider.CompareTag(interactableTags[i]))
                {
                    Debug.Log("Interaction");
                    isFound = true;

                    if (Input.GetKeyDown(interactableKey) && !inConnection)
                    {
                        Debug.Log("Change Object: {0}", hit.collider.gameObject);
                        if (hit.collider.gameObject.transform.parent == null)
                        {
                            changeOnInteract(hit.collider.gameObject.transform.gameObject);
                        }
                        else
                        {
                            changeOnInteract(hit.collider.gameObject.transform.parent.gameObject);
                        }

                    }





                }
                if(j < connectingTags.Length && Input.GetKeyDown(connnectToKey) && hit.collider.CompareTag(connectingTags[j]))
                {
                    Debug.Log("Trying to connect tag :" + hit.collider.tag);
                    connectToObject(hit.collider.gameObject);
                }
                i++;
                j++;
            }
        }
        else
        {
            Debug.Log("Test3");
        }
        #endregion

        #region Change object to changeOnInteract
        if (Input.GetKeyDown(changeObjectKey) && objectNum + 1 < changeToObject.Length)
        {
            objectNum++;
        }
        else if (Input.GetKeyDown(changeObjectKey) && objectNum + 1 >= changeToObject.Length)
        {
            objectNum = 0;
        }
        #endregion
    }

    private void changeOnInteract(GameObject hitObject)
    {
        savePosition = hitObject.transform.position;
        saveRotation = hitObject.transform.rotation;
        saveScale = hitObject.transform.localScale;
        Destroy(hitObject);
        GameObject newObject = changeToObject[objectNum];
        Instantiate(newObject, savePosition, saveRotation);
    }

    private void connectToObject(GameObject hitObject)
    {
        string tag = hitObject.tag;
        Debug.Log("Tag :" + tag);
        Debug.Log("Connecting Object to Event System");
        if (inConnection==false)
        {
            Debug.Log("Started Coroutine");
            StartCoroutine(connectionApprove());
        }

        switch (tag)
        {
            #region Interactables
            case "Button":
                ButtonScript bt = hitObject.GetComponent<ButtonScript>();
                if (bt == null)
                {
                    Debug.Log("Button is null");
                }
                eventIn = bt.onStay;
                eventOut = bt.onExit;
                eventConnected = true;
                break;
            case "Lever":
                LeverScript lv = hitObject.GetComponent<LeverScript>();
                if (lv == null)
                {
                    Debug.Log("Lever is null");
                }
                eventIn = lv.onStatus;
                eventOut = lv.offStatus;
                eventConnected = true;

                break;
            #endregion

            #region Connect to Interactables
            case "Piston":
                    PistonManager ps = hitObject.GetComponent<PistonManager>();
                    if (ps == null)
                    {
                        Debug.Log("Piston is null");
                    }
                    actionIn = ps.buttonOn;
                    actionOut = ps.buttonOff;
                break;
            case "Door":
                DoorManager dr = hitObject.GetComponent<DoorManager>();
                if (dr == null)
                {
                    Debug.Log("Piston is null");
                }
                actionIn = dr.buttonOn;
                actionOut = dr.buttonOff;
                break;
            #endregion

            default:
                Debug.LogError("Non of the objects");
                break;

        }
    }

    public static void AddListenerOnce(ref UnityEvent unityEvent, UnityAction unityAction)
    {
        for (int index = 0; index < unityEvent.GetPersistentEventCount(); index++)
        {
            Object curEventObj = unityEvent.GetPersistentTarget(index);
            string curEventName = unityEvent.GetPersistentMethodName(index);
            Debug.Log("curEventName: " + curEventName + ", unityAction: " + unityAction.Method.Name);
            if ((Object)unityAction.Target == curEventObj)
            {
                Debug.LogError("Event is already added: " + curEventName);
                return;
            }
        }
        unityEvent.AddListener(unityAction);
    }

    IEnumerator connectionApprove()
    {
        inConnection = true;
        string finish;
        Debug.Log("Waiting for Connection or Decline");
        
        //wait for space to be pressed
        while (!Input.GetKeyDown(KeyCode.Mouse1) && ((actionIn==null || (eventIn==null || eventConnected == false))))
        {
            yield return null;
        }
        if ((actionIn != null && eventIn != null))
        {
            AddListenerOnce(ref eventIn, actionIn);
            AddListenerOnce(ref eventOut, actionOut);
            finish = "Objects Connected";
        }
        else
        {
            finish = "User Declined the connection";
        }
        //do stuff once space is pressed
        Debug.Log(finish);
        actionOut = null;actionIn=null;
        eventOut = null;eventIn = null;
        inConnection = false;
    }
}