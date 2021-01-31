using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class PickableObject : MonoBehaviour
{
    public static Action<PickableObject> objecPickedUp;
    public string objectName;

    private MeshRenderer _MeshRenderer;
    private Collider _Collider;
    private Rigidbody _RigidBody;
    private bool _isEnabled;
    private bool _isPickedUp;

    private void OnEnable()
    {
        Doormat.playerOnDoormat += EnableObject;
    }

    private void OnDisable()
    {
        Doormat.playerOnDoormat -= EnableObject;
    }

    private void Awake()
    {
        _MeshRenderer = GetComponent<MeshRenderer>();
        _Collider = GetComponent<Collider>();
        _RigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _isEnabled = true;
        _isPickedUp = false;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
    //    {
    //        Pickup();
    //    }
    //}

    public void Pickup()
    {
        if (!_isEnabled)
            return;

        if (_isPickedUp)
            return;

        _isPickedUp = true;
        DisableObject();

        if (objecPickedUp != null)
        {
            objecPickedUp(this);
        }
    }

    private void DisableObject()
    {
        _MeshRenderer.enabled = false;
        _Collider.enabled = false;
        _RigidBody.isKinematic = true;
        _isEnabled = false;
    }

    private void EnableObject()
    {
        if (_isEnabled)
            return;

        if (_isPickedUp)
            return;

        _MeshRenderer.enabled = true;
        _Collider.enabled = true;
        _RigidBody.isKinematic = false;
        _isEnabled = true;
    }

    public void Drop(Vector3 dropPosition)
    {
        _isPickedUp = false;
        this.transform.position = dropPosition;
    }
}
