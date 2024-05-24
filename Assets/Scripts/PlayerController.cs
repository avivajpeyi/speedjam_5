using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;


    private DragManager dragManager;
    private GroundCheck _groundCheck;


    public float jumpHeight = 10f;
    public float gravityScale = 10;
    public float fallingGravityScale = 40;


    private float jumpForce
    {
        get
        {
            return Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));
        }
    }

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _groundCheck = GetComponent<GroundCheck>();
        dragManager = GetComponentInChildren<DragManager>();
        dragManager.OnMouseRelease.AddListener(TriggerJump);
    }


    public void TriggerJump()
    {
        if (_groundCheck.isGrounded)
        {
            Debug.Log("Jump!");

            rb.AddForce(
                jumpForce * -dragManager.DragVector * dragManager.Percent,
                ForceMode2D.Impulse
            );
            dragManager.Hide();
        }
    }


    void Update()
    {
        if (_groundCheck.isGrounded)
        {
            dragManager.Show();
            rb.gravityScale = gravityScale;
        }
        else
        {
            dragManager.Hide();
            if (rb.velocity.y > 0)
            {
                rb.gravityScale = gravityScale;
            }
            else if (rb.velocity.y < 0)
            {
                rb.gravityScale = fallingGravityScale;
            }
        }
    }
}