using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class PickableObject : MonoBehaviour
{
    public static Action<PickableObject> objecPickedUp;
    public static Action<PickableObject> objectDropped;
    public string objectName;

    public Vector3[] spawnPosition; 

    public GameObject _Model;
    private Collider _Collider;
    private Rigidbody _RigidBody;
    private ParticleSystem _ParticleSystem;
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
        _ParticleSystem = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        _isEnabled = true;
        _isPickedUp = false;
        if (spawnPosition.Length != 0) {
            Vector3 spawnPos = spawnPosition[UnityEngine.Random.Range(0, 9)];
            this.transform.position = spawnPos;
            print(objectName + " spawned at " + this.transform.position);
        }
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
        _ParticleSystem.Stop();
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
        _ParticleSystem.Play();
    }

    public void Drop()
    {
        _isPickedUp = false;
        if(spawnPosition.Length != 0) 
        {
            Vector3 spawnPos = spawnPosition[UnityEngine.Random.Range(0, 9)];
            spawnPos.y = 3f;
            this.transform.position = spawnPos;
            print("Object Dropped " + this.transform.position);
        }
        if (objectDropped != null)
            objectDropped(this);
    }

    public void Drop(Vector3 dropPosition)
    {
        _isPickedUp = false;
        this.transform.position = dropPosition;
        objectDropped(this);
    }
}
