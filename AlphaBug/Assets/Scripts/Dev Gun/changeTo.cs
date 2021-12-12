using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] [Space(2)] private string[] changeToName;
    [SerializeField] [Space(2)] private GameObject[] changeToObject;
    private int objectNum = 0;
    #endregion

    #region Save Veriables
    private Vector3 savePosition;
    private Quaternion saveRotation;
    private Vector3 saveScale;
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
            int i = 0;
            bool isFound = false;
            while (i < interactableTags.Length && isFound == false)
            {
                Debug.Log("While Works");
                if (hit.collider.CompareTag(interactableTags[i]))
                {
                    Debug.Log("Interaction");
                    isFound = true;

                    if (Input.GetKeyDown(interactableKey))
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
                i++;
            }
        }
        #endregion

        #region Change object to changeOnInteract
        if (Input.GetKeyDown(changeObjectKey) && objectNum+1 < changeToObject.Length)
        {
            objectNum++;
        }
        else if(Input.GetKeyDown(changeObjectKey) && objectNum + 1 >= changeToObject.Length)
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


}
