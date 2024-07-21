using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Character's movement speed
    private Rigidbody rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody2D component attached to the character
    }

    void Update()
    {
        // Get input from horizontal axis (A/D keys or Left/Right arrow keys)
        float moveX = Input.GetAxis("Horizontal");

        // Set movement vector based on input
        movement = new Vector2(moveX * moveSpeed, rb.velocity.y);
    }

    void FixedUpdate()
    {
        // Apply movement to the character
        rb.velocity = movement;
    }
}