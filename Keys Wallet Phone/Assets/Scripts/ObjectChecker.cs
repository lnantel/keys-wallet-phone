using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChecker : MonoBehaviour
{
    // Sphere cast settings
    public float sphereCastRadius;
    public float sphereCastDistance;

    // Objets layer mask
    public LayerMask objectsLayer;

    // TEMP
    public GameObject objectGUI;

    private void Update()
    {
        objectGUI.SetActive(CheckForObject());
    }

    private bool CheckForObject()
    {
        var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;
        Physics.SphereCast(ray, sphereCastRadius, out hitInfo, sphereCastDistance, objectsLayer);

        if (hitInfo.collider != null)
        {
            return true;
        }
        return false;
    }
}
