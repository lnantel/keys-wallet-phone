using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Doormat : MonoBehaviour
{
    public static Action playerOnDoormat;
    public CinemachineVirtualCamera doorCam;

    private bool _eventStarted;

    private void OnTriggerEnter(Collider other)
    {
        if (!_eventStarted)
        {
            StartDoorEvent();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            EndDoorEvent();
        }
    }

    private void StartDoorEvent()
    {
        _eventStarted = true;
        doorCam.enabled = true;

        if (playerOnDoormat != null)
            playerOnDoormat();
    }

    private void EndDoorEvent()
    {
        _eventStarted = false;
        doorCam.enabled = false;
    }
}
