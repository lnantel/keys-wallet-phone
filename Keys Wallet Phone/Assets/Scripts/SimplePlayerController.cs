using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{
    public float walkSpeed;
    public float sprintSpeed;
    public float rotationSpeed;

    private Rigidbody _RigidBody;
    private Vector3 _GlobalVelocity;

    public bool IsSprinting;

    private void Awake()
    {
        _RigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        IsSprinting = Input.GetKey(KeyCode.LeftShift);
    }

    private void FixedUpdate()
    {
        float currentMoveSpeed = IsSprinting ? sprintSpeed : walkSpeed;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 fwd = transform.forward;
        fwd.Normalize();
        Vector3 move = transform.right * moveX + fwd * moveZ;

        Vector3 velocity = move * currentMoveSpeed * Time.fixedDeltaTime;
        velocity.y = _RigidBody.velocity.y;

        _RigidBody.velocity = velocity;
    }
}
