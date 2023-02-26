using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Main Variables")]
    public float sensitivity;
    public Transform player;
    public Transform cam;
    public Transform orientation;

    [Header("Debug Variables")]
    float xRotation;
    float yRotation;
    float mouseX;
    float mouseY;

    public PlayerShit playerShit;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!playerShit.dead)
        {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");

            yRotation += mouseX * sensitivity;
            xRotation -= mouseY * sensitivity;

            if (!PlayerController.instance.bossMode)
            {
                xRotation = Mathf.Clamp(xRotation, -15, 45);
            }
            if (PlayerController.instance.bossMode)
            {
                xRotation = Mathf.Clamp(xRotation, -85, 85);
            }

            cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}
