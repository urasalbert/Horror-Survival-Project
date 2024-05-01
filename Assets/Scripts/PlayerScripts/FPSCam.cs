using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCam : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 2f;
    Vector2 cameraRotation = Vector2.zero;
    public float verticalRotationLimit = 90f;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (InventorySystem.Instance.isOpen == false && PauseScreenManager.Instance.isGamePaused == false)//if inventory closed and game not paused 
        {
            float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            cameraRotation.x += inputX;
            cameraRotation.y -= inputY;
            cameraRotation.y = Mathf.Clamp(cameraRotation.y, -verticalRotationLimit, verticalRotationLimit);

            transform.localRotation = Quaternion.Euler(cameraRotation.y, 0f, 0f);
            player.Rotate(Vector3.up * inputX);
        }
    }
}
