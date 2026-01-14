using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerMovementTutorial : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 6f;
    public float sprintSpeed = 12f;
    public float airMultiplier = 0.4f;

    [Header("Jumping")]
    public float jumpForce = 6f;
    public float jumpCooldown = 0.25f;
    public float gravity = -5;
    private bool readyToJump = true;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask whatIsGround;
    private bool grounded;

    public Transform orientation;

    private float moveSpeed;
    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;
    private Vector3 velocity;

    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        readyToJump = true;
    }

    private void Update()
    {
        GroundCheck();
        MyInput();
        MovePlayer();
        ApplyGravity();
    }

    private void GroundCheck()
    {
        grounded = Physics.CheckSphere(
            groundCheck.position,
            groundDistance,
            whatIsGround
        );

        // Stick player to ground
        if (grounded && velocity.y < 0)
            velocity.y = -2f;
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Sprint
        moveSpeed = Input.GetKey(sprintKey) ? sprintSpeed : walkSpeed;

        // Jump
        if (Input.GetKeyDown(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        moveDirection.Normalize();

        float currentSpeed = grounded ? moveSpeed : moveSpeed * airMultiplier;

        controller.Move(moveDirection * currentSpeed * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
