using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCam : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 2f;
    float cameraVerticalRotation = 0;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(InventorySystem.Instance.isOpen == false)
        {
            float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            cameraVerticalRotation -= inputY;
            cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
            transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

            player.Rotate(Vector3.up * inputX);
        }
    }
}
