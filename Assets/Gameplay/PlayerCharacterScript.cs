using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterScript : MonoBehaviour
{
    [Header("Design Values")]
    public float moveSpeed = 10f;
    public float jumpForce = 10f;
    public float jumpSpinTorque = 5f;
    [Header("References")]
    public PauseMenuScript pauseMenu;

    new Rigidbody2D rigidbody;
    GroundedSense groundedSense;
    bool jumpInput;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        groundedSense = GetComponent<GroundedSense>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
            pauseMenu.Pause();
        
        if (Input.GetButtonDown("Jump"))
            jumpInput = true;
    }

    void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rigidbody.velocity.y);

        if (jumpInput && groundedSense.IsGrounded)
        {
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            rigidbody.AddTorque(Random.Range(-jumpSpinTorque, jumpSpinTorque));
            jumpInput = false;
        }
    }
}
