using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;

    private Rigidbody _RigidBody;

    private Vector3 _GlobalVelocity;

    private void Awake()
    {
        _RigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 fwd = transform.forward;
        fwd.Normalize();
        Vector3 move = transform.right * moveX + fwd * moveZ;

        Vector3 velocity = move * movementSpeed * Time.fixedDeltaTime;
        velocity.y = _RigidBody.velocity.y;

        _RigidBody.velocity = velocity;
    }
}
