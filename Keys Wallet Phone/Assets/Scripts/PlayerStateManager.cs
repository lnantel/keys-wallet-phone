using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour {
    public static PlayerStateManager instance;

    public bool Jumping { get; private set; }

    public bool CanJump { get; private set; } = true;

    public bool Grounded { get; protected set; }
    public Vector3 GroundNormal { get; protected set; }

    public bool Moving { get; protected set; }
    public bool Alive { get; protected set; }

    public bool CanMove { get; protected set; } = true;
    public float SpeedFactor { get; protected set; } = 1.0f;

    void Awake() {
        if (instance != null)
            Destroy(gameObject);
        else {
            instance = this;
        }
    }

    void Start() {
        Alive = true;
    }

    void Update() {

    }

    public void StartJumping(float duration) {
        if (CanJump) {
            Jumping = true;
            StartCoroutine(JumpTimer(duration));
        }
    }

    private IEnumerator JumpTimer(float duration) {
        yield return new WaitForSeconds(duration);
        Jumping = false;
    }

    public void BecomeGrounded(Vector3 groundNormal) {
        Grounded = true;
        GroundNormal = groundNormal;
    }

    public void BecomeAirborne() {
        Grounded = false;
        GroundNormal = Vector3.up;
    }

    public virtual void SetSpeedFactor(float factor) {
        SpeedFactor = factor;
    }

    public virtual void StartMoving() {
        Moving = true;
    }

    public virtual void StopMoving() {
        Moving = false;
    }
}
