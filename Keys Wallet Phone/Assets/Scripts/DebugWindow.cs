using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugWindow : MonoBehaviour
{
    public static Action objectsReenable;
    public static Action dropObject;
    public GameObject debugCanvas;

    public PickableObject keys;
    public PickableObject wallet;
    public PickableObject phone;

    private void Start()
    {
        debugCanvas.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            debugCanvas.SetActive(!debugCanvas.activeSelf);
            if (debugCanvas.activeSelf)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }

    public void ReenableObjects()
    {
        if (objectsReenable != null)
            objectsReenable();
    }

    public void PickupObject(string objectName)
    {
        switch (objectName)
        {
            case "Keys":
                keys.Pickup();
                break;
            case "Wallet":
                wallet.Pickup();
                break;
            case "Phone":
                phone.Pickup();
                break;
            default:
                break;
        }
    }

    public void DropObject()
    {
        if (dropObject != null)
            dropObject();
    }
}
