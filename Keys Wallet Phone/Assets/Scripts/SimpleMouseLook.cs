using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SimpleMouseLook : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity;

    private float xRotation = 0f;
    private float yRotation = 0f;
    private Vector2 _BaseMouseRotation;

    private void Update()
    {
        MouseRotation();
    }

    private void MouseRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        yRotation -= mouseX;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.localRotation = Quaternion.Euler(0f, -yRotation, 0f);
    }
}
