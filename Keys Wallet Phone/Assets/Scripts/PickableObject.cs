using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class PickableObject : MonoBehaviour
{
    public static Action<PickableObject> objecPickedUp;
    public string objectName;

    public Vector3[] spawnPosition; 

    public GameObject _Model;
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
        _Model.SetActive(false);
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

        _Model.SetActive(true);
        _Collider.enabled = true;
        _RigidBody.isKinematic = false;
        _isEnabled = true;
    }

    public void Drop()
    {
        _isPickedUp = false;
        if(spawnPosition.Length != 0) 
        {
            this.transform.position = spawnPosition[UnityEngine.Random.Range(0,9)];
            print("Object Dropped " + this.transform.position);
        }
    }

    public void Drop(Vector3 dropPosition)
    {
        _isPickedUp = false;
        this.transform.position = dropPosition;
    }
}
