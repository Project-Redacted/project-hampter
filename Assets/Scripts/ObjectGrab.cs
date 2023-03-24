using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrab : MonoBehaviour
{
    private Rigidbody objectRigidbody;
    private Transform objectGrabPointTransform;
    public float ThrowForce = 5;

    private Vector3 StartPosition;
    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();
        StartPosition = transform.position;
    }

    private void Update()
    {
        if(transform.position.y < -5)
        {
            ResetPosition();
        }
    }


    public void Grab(Transform objectGrabPointTransform)
    {
        //this.objectGrabPointTransform = objectGrabPointTransform;
        this.transform.parent = objectGrabPointTransform;
        this.transform.localPosition = new Vector3(0, 0, 0);

        objectRigidbody.useGravity = false;
        objectRigidbody.isKinematic = true;
        this.GetComponent<Collider>().enabled = false;
        Move m = GetComponentInChildren<Move>();
        if(m != null)
        {
            m.enabled = false;
        }
    }

    public void Throw(Vector3 direcction)
    {
        this.transform.parent = null;
        objectRigidbody.useGravity = true;
        objectRigidbody.isKinematic = false;
        this.GetComponent<Collider>().enabled = true;
        objectRigidbody.AddForce(direcction * ThrowForce, ForceMode.Impulse);
    }

    private void FixedUpdate(){
    
        if (objectGrabPointTransform != null){
        
            objectRigidbody.MovePosition(objectGrabPointTransform.position);
        }
    }

    public void ResetPosition()
    {
        transform.position = StartPosition;
        objectRigidbody.velocity = new Vector3(0, 0, 0);
    }

}
