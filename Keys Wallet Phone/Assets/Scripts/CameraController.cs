﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public static CameraController instance;

    public float m_CameraPitchSpeed;
    public float m_CameraYawSpeed;

    public float Pitch { get; private set; }
    public float Yaw { get; private set; }
    private Camera cam;
    private float defaultFOV;

    void Awake() {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    void Start() {
        Pitch = 0.0f;
        cam = GetComponent<Camera>();
        defaultFOV = cam.fieldOfView;
    }

    void LateUpdate() {
        //POSITION
        transform.position = Vector3.Lerp(transform.position, PlayerStateManager.instance.transform.position + 1.65f * Vector3.up, Time.deltaTime / Time.fixedDeltaTime);

        if (PlayerStateManager.instance.Alive) {
            //ROTATION
            //Pitch += PlayerInputManager.instance.CamInput.y * -m_CameraPitchSpeed * Time.deltaTime;
            Pitch += PlayerInputManager.instance.CamInput.y * -m_CameraPitchSpeed;
            Pitch = Mathf.Clamp(Pitch, -90.0f, 90.0f);
            Quaternion pitchRotation = Quaternion.Euler(Pitch, 0.0f, 0.0f);

            //Yaw += PlayerInputManager.instance.CamInput.x * m_CameraYawSpeed * Time.deltaTime;
            Yaw += PlayerInputManager.instance.CamInput.x * m_CameraYawSpeed;
            Quaternion yawRotation = Quaternion.Euler(0.0f, Yaw, 0.0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, yawRotation * pitchRotation, 1.0f);
        }
        else {
            transform.rotation = PlayerStateManager.instance.transform.rotation;
        }
    }

    public void SetMagnification(float factor, float time) {
        StartCoroutine(FOVTransition(cam.fieldOfView, defaultFOV / factor, time));
    }

    private IEnumerator FOVTransition(float initialFOV, float targetFOV, float time) {
        float timer = 0.0f;
        while (timer < time) {
            cam.fieldOfView = Mathf.Lerp(initialFOV, targetFOV, timer / time);
            timer += Time.deltaTime;
            yield return new WaitForSeconds(0.0f);
        }
        cam.fieldOfView = targetFOV;
    }
}