using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterScript : MonoBehaviour
{
    [Header("Design Values")]
    public float moveAccel = 1f;
    public float moveDecel = 2f;
    public float moveFriction = 0.9f;
    public float maxMoveSpeed = 10f;
    public float jumpForce = 10f;
    public float jumpSpinTorque = 5f;
    [Header("References")]
    public PauseMenuScript pauseMenu;

    new Rigidbody2D rigidbody;
    Animator animator;
    GroundedSense groundedSense;
    bool jumpInput;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundedSense = GetComponent<GroundedSense>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
            pauseMenu.Pause();

        if (Input.GetButtonUp("Jump"))
            jumpInput = false;
        if (Input.GetButtonDown("Jump"))
            jumpInput = true;
    }

    void FixedUpdate()
    {
        // Moving
        float moveSpeed = rigidbody.velocity.x;
        if (Input.GetAxisRaw("Horizontal") > 0) // If attempting to move right
        {
            animator.SetBool("Walking", true);
            if (moveSpeed >= 0) // If already moving to the right
                moveSpeed += Input.GetAxisRaw("Horizontal") * moveAccel; // Accelerate
            else
                moveSpeed += Input.GetAxisRaw("Horizontal") * moveDecel; // Decelerate
            if (moveSpeed > maxMoveSpeed) // Don't move faster than max move speed
                moveSpeed = maxMoveSpeed;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0) // If attempting to move left
        {
            animator.SetBool("Walking", true);
            if (moveSpeed <= 0) // If already moving to the left
                moveSpeed += Input.GetAxisRaw("Horizontal") * moveAccel; // Accelerate
            else
                moveSpeed += Input.GetAxisRaw("Horizontal") * moveDecel; // Decelerate
            if (moveSpeed < -maxMoveSpeed) // Don't move faster than max move speed
                moveSpeed = -maxMoveSpeed;
        }
        else // Slow to a stop
        {
            animator.SetBool("Walking", false);
            moveSpeed *= moveFriction;
        }

        // Set new movement speed
        rigidbody.velocity = new Vector2(moveSpeed, rigidbody.velocity.y);

        // Jump if attempting to jump while on the ground
        if (jumpInput && groundedSense.IsGrounded)
        {
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            rigidbody.AddTorque(Random.Range(-jumpSpinTorque, jumpSpinTorque)); // Add some spin
            jumpInput = false;
        }
    }
}
