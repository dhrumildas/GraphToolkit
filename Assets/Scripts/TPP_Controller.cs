using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class TPP_Controller : MonoBehaviour
{
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 12f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float mouseSensitivity = 0.12f;
    [SerializeField] private float minPitch = -45f;
    [SerializeField] private float maxPitch = 70f;

    private CharacterController charCont;
    private float verticalVelocity;
    private float camYaw;
    private float camPitch;
    private bool cursorLocked;

    private void Awake()
    {
        charCont = GetComponent<CharacterController>();
        if (cameraTarget == null)
        {
            Debug.LogError(
                "ThirdPersonPlayerController: CameraTarget has not been assigned.",
                this);
        }
    }

    private void Start()
    {
        if (cameraTarget != null)
        {
            Vector3 startingRotation = cameraTarget.eulerAngles;

            camYaw = startingRotation.y;
            camPitch = NormaliseAngle(startingRotation.x);
        }

        SetCursorLocked(true);
    }

    public void SetCursorLocked(bool v)
    {
        cursorLocked = v;
        Cursor.lockState = v ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !v;
    }


    private void Update()
    {

        if (Keyboard.current != null && Keyboard.current.f1Key.wasPressedThisFrame)
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SceneManager.LoadScene("Menu");
            return;
        }

        HandleCursor();

        if (DialogueRunner.IsDialogueOpen)
            return;

        //HandleCursor();

        if (cameraTarget == null || Keyboard.current == null)
            return;

        ReadCameraInput();
        MovePlayer();
        RotateCameraTarget();
    }

    private void RotateCameraTarget()
    {
        //World rot is intentional since the CameraTarget is a child of the player
        cameraTarget.rotation = Quaternion.Euler(camPitch, camYaw, 0f);
    }

    private void MovePlayer()
    {
        float horizontalInput = 0f;
        float verticalInput = 0f;

        if(Keyboard.current.wKey.isPressed)
            verticalInput += 1f;
        if(Keyboard.current.sKey.isPressed)
            verticalInput -= 1f;
        if(Keyboard.current.aKey.isPressed)
            horizontalInput -= 1f;
        if(Keyboard.current.dKey.isPressed)
            horizontalInput += 1f;

        Vector2 input = new Vector2(horizontalInput, verticalInput);

        input = Vector2.ClampMagnitude(input, 1f);

        //Since movement is relative to the camera...
        Quaternion yawRot = Quaternion.Euler(0f, camYaw, 0f);

        Vector3 camFwd = yawRot * Vector3.forward;
        Vector3 camRight = yawRot * Vector3.right;
        Vector3 moveDir = camFwd * input.y + camRight * input.x;

        if(moveDir.sqrMagnitude > 0.001f)
        {
            moveDir.Normalize();
            Quaternion desiredRot = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, rotationSpeed * Time.deltaTime);
        }

        //if(charCont.isGrounded && verticalVelocity < 0f)
        //    verticalVelocity = -2f;

        //verticalVelocity += gravity * Time.deltaTime;

        if (charCont.isGrounded)
        {
            if (verticalVelocity < 0f)
                verticalVelocity = -2f;

            if(Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 velocity = moveDir * moveSpeed;

        velocity.y = verticalVelocity;

        charCont.Move(velocity * Time.deltaTime);
    }

    private void ReadCameraInput()
    {
        //if(Cursor.lockState != CursorLockMode.Locked || Mouse.current == null)
        //    return;

        if(!cursorLocked || Mouse.current == null)
            return;

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        camYaw += mouseDelta.x * mouseSensitivity;
        camPitch -= mouseDelta.y * mouseSensitivity;

        camPitch = Mathf.Clamp(camPitch, minPitch, maxPitch);
    }

    private void HandleCursor()
    {
        //if(Keyboard.current == null && Keyboard.current.escapeKey.wasPressedThisFrame)
        //{
        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;
        //}

        //if(Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame && Cursor.lockState != CursorLockMode.Locked)
        //{
        //    LockCursor();
        //}

        //if(Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        //{
        //    SetCursorLocked(false);
        //    return;
        //}

        //if(Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame && !cursorLocked)
        //{
        //    SetCursorLocked(true);
        //    return;
        //}

        if (Keyboard.current == null)
            return;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SetCursorLocked(!cursorLocked);
        }
    }

    //private void LockCursor()
    //{
    //    Cursor.lockState = CursorLockMode.Locked;
    //    Cursor.visible = false;
    //}

    private float NormaliseAngle(float x)
    {
        if (x > 180)
            x -= 360;
        return x;
    }
}
