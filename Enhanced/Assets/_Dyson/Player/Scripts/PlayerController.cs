using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enhanced.Dyson.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject player;

        [SerializeField] private float speed;

        [SerializeField] private Rigidbody rb;

        [SerializeField] private float mouseSensitivity = 100f;

        [SerializeField] private Transform camera;
        private float xRotation;

        private float yRotation;
        // Start is called before the first frame update
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        private void FixedUpdate()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);
            
            LookAroundMouse();
        }

        private void LookAroundMouse()
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * mouseSensitivity;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * mouseSensitivity;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            
            camera.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
            player.transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}
