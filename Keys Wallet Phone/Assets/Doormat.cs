using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;

public class Doormat : MonoBehaviour
{
    public static Action keysCheck;
    public static Action phoneCheck;
    public static Action walletCheck;
    public static Action missingObject;
    public static Action success;

    public static Action playerOnDoormat;
    public static Action playerExitDoormat;
    public CinemachineVirtualCamera doorCam;

    public ParticleSystem burstParticles;
    public Transform burstPos1;
    public Transform burstPos2;
    public Transform burstPos3;

    // Button references
    public GameObject keysImage;
    public GameObject walletImage;
    public GameObject phoneImage;

    public GameObject k_Key;
    public GameObject w_Key;
    public GameObject p_Key;

    private bool _eventStarted;
    private DoormatEventState _eventState;

    private IEnumerator _currentEndTimer;

    private void OnTriggerEnter(Collider other)
    {
        if (!_eventStarted)
        {
            StartDoorEvent();
        }
    }

    private void Start()
    {
        _eventState = DoormatEventState.Default;
        keysImage.SetActive(false);
        walletImage.SetActive(false);
        phoneImage.SetActive(false);

        k_Key.SetActive(false);
        w_Key.SetActive(false);
        p_Key.SetActive(false);
    }

    private void Update()
    {
        if (!_eventStarted)
            return;

        // Doormat button inputs
        CheckForEventControls();

        // Temp ending event
        if (Input.GetKeyDown(KeyCode.Return))
        {
            EndDoorEvent();
        }
    }

    private void StartDoorEvent()
    {
        _eventStarted = true;
        doorCam.enabled = true;

        k_Key.SetActive(true);

        if (playerOnDoormat != null)
            playerOnDoormat();
    }

    private void EndDoorEvent()
    {
        if (_currentEndTimer != null)
            return;

        _eventState = DoormatEventState.Default;
        keysImage.SetActive(false);
        walletImage.SetActive(false);
        phoneImage.SetActive(false);

        k_Key.SetActive(false);
        w_Key.SetActive(false);
        p_Key.SetActive(false);

        _eventStarted = false;
        doorCam.enabled = false;

        if (playerExitDoormat != null)
            playerExitDoormat();
    }

    private void CheckForEventControls()
    {
        switch (_eventState)
        {
            case DoormatEventState.Default:
                {
                    if (Input.GetKeyDown(KeyCode.K))
                    {
                        k_Key.SetActive(false);
                        keysImage.SetActive(true);
                        if (InventoryManager.instance.CheckForObject("Keys"))
                        {
                            w_Key.SetActive(true);
                            _eventState = DoormatEventState.Keys;
                            burstParticles.transform.position = burstPos1.position;
                            burstParticles.Play();
                            keysCheck();
                        }
                        else
                        {
                            StartEndTimer();
                            missingObject();
                        }
                        break;
                    }
                    break;
                }

            case DoormatEventState.Keys:
                {
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        w_Key.SetActive(false);
                        walletImage.SetActive(true);
                        if (InventoryManager.instance.CheckForObject("Wallet"))
                        {
                            p_Key.SetActive(true);
                            _eventState = DoormatEventState.Wallet;
                            burstParticles.transform.position = burstPos2.position;
                            burstParticles.Play();
                            walletCheck();
                        }
                        else
                        {
                            StartEndTimer();
                            missingObject();
                        }
                        break;
                    }
                    break;
                }

            case DoormatEventState.Wallet:
                {
                    if (Input.GetKeyDown(KeyCode.P))
                    {
                        p_Key.SetActive(false);
                        phoneImage.SetActive(true);
                        if (InventoryManager.instance.CheckForObject("Phone"))
                        {
                            _eventState = DoormatEventState.Phone;
                            burstParticles.transform.position = burstPos3.position;
                            burstParticles.Play();
                            phoneCheck();
                        }
                        else
                        {
                            StartEndTimer();
                            missingObject();
                        }
                        break;
                    }
                    break;
                }

            case DoormatEventState.Phone:
                {
                    LevelManager.instance.LoadScene("WinScreen", Color.white);
                    success();
                    break;
                }
        }
    }

    private enum DoormatEventState
    {
        Default,
        Keys,
        Wallet,
        Phone
    }

    private void StartEndTimer()
    {
        if (_currentEndTimer != null)
            return;

        _currentEndTimer = EndEventTimer();
        StartCoroutine(_currentEndTimer);
    }

    private IEnumerator EndEventTimer()
    {
        TextMeshProUGUI keysText = keysImage.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI walletText = walletImage.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI phoneText = phoneImage.GetComponentInChildren<TextMeshProUGUI>();

        keysImage.SetActive(true);
        walletImage.SetActive(true);
        phoneImage.SetActive(true);

        if (!InventoryManager.instance.CheckForObject("Keys"))
        {
            keysText.color = Color.red;
        }

        if (!InventoryManager.instance.CheckForObject("Wallet"))
        {
            walletText.color = Color.red;
        }

        if (!InventoryManager.instance.CheckForObject("Phone"))
        {
            phoneText.color = Color.red;
        }

        yield return new WaitForSeconds(1f);
        _currentEndTimer = null;
        EndDoorEvent();

        keysText.color = Color.white;
        walletText.color = Color.white;
        phoneText.color = Color.white;
    }
        
}
