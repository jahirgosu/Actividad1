using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontalMove = 0f;
    private Vector3 currentVelocity;
    private float compensationSpeed = 10f;
    private bool facingRight;
    private bool jumpPressed = false;
    private bool isGrounded = false;

    [Header("Speed settings")]
    public float runSpeed = 2f;
    public float movementSmoothing = 0.05f;
    [Header("Jump settings")]
    public float jumpForce = 400f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal") * runSpeed * compensationSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true;
        }
    }

    private void FixedUpdate()
    {
        isGrounded = false;
        Collider2D[] groundColliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundLayer);
        for (int i = 0; i < groundColliders.Length; i++)
        {
            if (groundColliders[i].gameObject != this.gameObject) 
            {
                isGrounded = true;
            }
        }

        Vector3 targetVelocity = new Vector2(horizontalMove * Time.fixedDeltaTime, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, movementSmoothing);

        if (jumpPressed && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            jumpPressed = false;
            isGrounded = false;
        }

        if (horizontalMove > 0f && !facingRight)
        {
            facingRight = !facingRight;
            Vector3 targetScale = transform.localScale;
            targetScale.x *= -1;
            transform.localScale = targetScale;
        }
        else if(horizontalMove < 0f && facingRight)
        {
            facingRight = !facingRight;
            Vector3 targetScale = transform.localScale;
            targetScale.x *= -1;
            transform.localScale = targetScale;
        }
    }

}
