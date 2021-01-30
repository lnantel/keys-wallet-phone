using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour {
    public static PlayerInputManager instance;

    //CAMERA
    public float MouseSensitivity = 1.0f;
    private float xCamInput;
    private float yCamInput;
    public Vector3 CamInput { get; private set; }

    //MOVEMENT
    private float xMoveInput;
    private float zMoveInput;
    public Vector3 MoveInput { get; private set; }

    //JUMP
    public bool JumpInput;

    //GRAB
    public bool GrabInput;

    void Awake() {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        xCamInput = Input.GetAxis("Mouse X") * MouseSensitivity;
        yCamInput = Input.GetAxis("Mouse Y") * MouseSensitivity;
        CamInput = new Vector3(xCamInput, yCamInput, 0.0f);

        xMoveInput = Input.GetAxisRaw("Horizontal");
        zMoveInput = Input.GetAxisRaw("Vertical");
        MoveInput = new Vector3(xMoveInput, 0.0f, zMoveInput).normalized;

        if (!JumpInput) JumpInput = Input.GetButtonDown("Jump");
        //if (!GrabInput) GrabInput = Input.GetButtonDown("Grab");
    }
}
