using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Main Character Movement speed and controls

public class CharacterMovement : MonoBehaviour
{
    public Transform cam;
    public float moveSpeed = 5f; // Speed of the character
    private Vector2 movement;    // Movement vector

    private Rigidbody2D rb;      // Reference to Rigidbody2D component
    private bool isFacingRight = true; // Track whether the player is facing right
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get the horizontal and vertical input (WASD or arrow keys)
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Create movement vector based on input
        movement = new Vector2(moveX, moveY).normalized;

        // Call Flip() to handle character direction
        if (moveX != 0)
        {
            Flip(moveX);
        }

        if (GameManager.enemiesSpawnedIn == 0) {
            //print("moving");
            if (transform.position.x > cam.position.x)
                cam.position = new Vector3(transform.position.x, cam.position.y, cam.position.z);
        }
    }

    void FixedUpdate()
    {
        // Move the character by setting the Rigidbody2D velocity
        rb.velocity = movement * moveSpeed;
        
        if (Mathf.Abs(rb.velocity.y) != 0 || Mathf.Abs(rb.velocity.x) != 0) {
            animator.SetFloat("xVelocity", moveSpeed);
        } else {
            animator.SetFloat("xVelocity", 0);
        }
    }

    // Flip the character sprite based on movement direction
    void Flip(float horizontal)
    {
        if (horizontal > 0 && !isFacingRight)
        {
            // Moving right but currently facing left, so flip
            FlipSprite();
        }
        else if (horizontal < 0 && isFacingRight)
        {
            // Moving left but currently facing right, so flip
            FlipSprite();
        }
    }

    // Handles the actual flipping of the sprite
    void FlipSprite()
    {
        isFacingRight = !isFacingRight; // Toggle the facing direction

        // Multiply the player's x local scale by -1 to flip
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
