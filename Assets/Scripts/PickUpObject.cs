using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public Transform hand;
    private Transform heldObject;
    private float pickupDistance = 5.0f;
    private bool isHoldingObject = false;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isHoldingObject) 
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, pickupDistance))
            {

                ObjectGrab OG = hit.transform.GetComponent<ObjectGrab>();
                if(OG != null)
                {
                    OG.Grab(hand);

                    isHoldingObject = true;
                    heldObject = hit.transform;
                }


                if (hit.transform.CompareTag("PickUpObject"))
                {
                    /*
                    heldObject = hit.transform;
                    heldObject.GetComponent<Rigidbody>().isKinematic = true;
                    heldObject.SetParent(transform);
                    heldObject.localPosition = Vector3.zero;
                    */
                    isHoldingObject = true;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.G) && isHoldingObject) 
        {
            heldObject.GetComponent<ObjectGrab>().Throw(Camera.main.transform.forward);
            heldObject = null;
            isHoldingObject = false;
        }

        if(heldObject != null)
        {
            heldObject.rotation = Quaternion.Lerp(heldObject.rotation, Camera.main.transform.rotation, 5 * Time.deltaTime);
        }
    }
}