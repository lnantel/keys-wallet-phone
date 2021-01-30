using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public static PlayerController instance;

    public float m_MovementSpeed;
    public float m_RotationSpeed;
    public float m_JumpForce;

    private PlayerStateManager state;
    private PlayerInputManager input;
    private Rigidbody rb;

    private float fallingSpeed;
    private float jumpSpeed;

    void Awake() {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    void Start() {
        rb = GetComponent<Rigidbody>();
        state = PlayerStateManager.instance;
        input = PlayerInputManager.instance;
    }

    void Update() {
        rb.MoveRotation(Quaternion.Euler(0.0f, CameraController.instance.Yaw, 0.0f));

        Vector3 displacement = CalculateMovement();
        rb.velocity = displacement;

        if (!state.Alive && !rb.useGravity) {
            rb.useGravity = true;
            rb.mass = 1.0f;
            rb.angularDrag = 0.0f;
            rb.AddForceAtPosition(-transform.forward * 5.0f, transform.position + 0.5f * Vector3.up, ForceMode.Impulse);
            rb.constraints = RigidbodyConstraints.None;
        }

        Debug.Log(state.Grounded);
    }

    private Vector3 CalculateMovement() {

        Vector3 relativeMoveInput = Vector3.zero;

        if (state.CanMove)
            relativeMoveInput = (input.MoveInput.z * transform.forward + input.MoveInput.x * transform.right).normalized;

        if (relativeMoveInput.magnitude != 0.0f) state.StartMoving();
        else state.StopMoving();

        //Stick to ground
        Vector3 projectedMoveInput =
            Vector3.ProjectOnPlane(relativeMoveInput, state.GroundNormal).normalized * relativeMoveInput.magnitude;

        //Fall
        if (state.Alive) {
            if (state.Grounded && !state.Jumping) {
                fallingSpeed = 0.0f;
                jumpSpeed = 0.0f;
            }
            else {
                fallingSpeed += -9.81f * Time.fixedDeltaTime;
            }
        }

        //Jump
        if (input.JumpInput) {
            input.JumpInput = false;
            if (state.CanJump && state.Grounded && !state.Jumping) {
                jumpSpeed = m_JumpForce;
                state.StartJumping(0.1f);
            }
        }

        Vector3 displacement = projectedMoveInput * m_MovementSpeed /* * state.SpeedFactor */ * Time.fixedDeltaTime + Vector3.up * fallingSpeed + Vector3.up * jumpSpeed * Time.fixedDeltaTime;
        return displacement;
    }

    private Quaternion CalculateRotation() {
        return Quaternion.Euler(0.0f, CameraController.instance.Yaw, 0.0f);
    }
}
