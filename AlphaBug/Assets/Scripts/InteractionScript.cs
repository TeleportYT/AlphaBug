using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionScript : MonoBehaviour
{
    #region Ray Settings
    [Header("Ray Settings")]
    [Space(2)] [SerializeField] [Range(0,10)] private int rayLength;
    [SerializeField] [Space(2)] private LayerMask layerMaskInteract;
    [SerializeField] [Space(2)] private string exludeLayerName = null;
    #endregion

    #region Crosshair Settings
    [Header("Crosshair Settings")]
    [SerializeField] [Space(2)] private Image Crosshair;
    [SerializeField] [Space(2)] private Sprite Intercatcrosshair = null;
    [SerializeField] [Space(2)] private Sprite OriginalCrosshair = null;
    private bool isCrosshair = false;
    #endregion

    #region Interaction Settings
    [Space(5)]
    [Header("Intecation Settings")]
    [SerializeField] [Space(2)] private KeyCode interactableKey;
    [SerializeField] [Space(2)] private string[] interactableTags;
    private activateOnInteract onInteract;
    #endregion

    private void Update()
    {
        Crosshair = GameObject.Find("Crosshair").GetComponent<Image>();
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        int mask = 1 << LayerMask.NameToLayer(exludeLayerName) | layerMaskInteract.value;
        Debug.Log("mask : " + mask);
        Debug.Log("test : " + Physics.Raycast(transform.position, fwd, out hit, rayLength, mask));
        if (Physics.Raycast(transform.position,fwd,out hit, rayLength, mask))
        {
            Debug.Log(hit.collider.tag);
            int i = 0;
            bool isFound = false;
            while (i<interactableTags.Length && isFound == false)
            {
                Debug.Log("While Works");
                if (hit.collider.CompareTag(interactableTags[i]))
                {
                    Debug.Log("Interaction");
                    isFound = true;
                    //Change to Interactable Crosshair
                    CrosshairChange(true);
                    isCrosshair = true;

                    if (Input.GetKeyDown(interactableKey))
                    {
                        hit.collider.gameObject.GetComponent<activateOnInteract>().onInteract();
                    }

                }
                i++;
            }
        }
        else
        {
            if (isCrosshair)
            {
                //Change to Original Crosshair
                CrosshairChange(false);
                isCrosshair = false;
            }
        }
    }

    void CrosshairChange(bool on)
    {
        if (on)
        {
            //Change to Interactable Crosshair
            Crosshair.sprite = Intercatcrosshair;
        }
        else
        {
            //Change to Original Crosshair
            Crosshair.sprite = OriginalCrosshair;
        }
    }

}
