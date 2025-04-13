using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody rb;
    private Transform objectGrabPointTransform;
    float lerpSpeed = 10f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        rb.useGravity = false;
    }
    
    public void Drop()
    {
        objectGrabPointTransform = null;
        rb.useGravity = true;
    }

    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            rb.MovePosition(newPosition);
        }
    }
}
