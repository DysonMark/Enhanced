using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Enhanced.Dyson.Player
{
    public class PickUp : MonoBehaviour
    {
        public GameObject player;

        public Transform holdPos;

       [SerializeField] private Transform cameraTransform;

        [SerializeField] private LayerMask pickUpLayerMask;

         private ObjectGrabbable _objectGrabbable;
        private void Update()
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (_objectGrabbable == null)
                {
                    float pickUpDistance = 2f;
                    if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit raycastHit,
                            pickUpDistance, pickUpLayerMask))
                    {
                        Debug.Log(raycastHit.transform);
                    }

                    if (raycastHit.transform.TryGetComponent(out _objectGrabbable))
                    {
                        _objectGrabbable.Grab(holdPos);
                        Debug.Log(_objectGrabbable);
                    }
                }
                else
                {
                    _objectGrabbable.Drop();
                    _objectGrabbable = null;
                }


            }
        }
    }
}
