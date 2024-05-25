using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool isGrounded;

    // [SerializeField] private float vMag;
    // bool velocityIsLow
    // {
    //     private float vThreshold = 2.0f;
    //     get { return rb.velocity.magnitude <= vThreshold; }
    // }
    //
    //
    // private void Update()
    // {
    //     vMag = rb.velocity.magnitude;
    // }
    
    
    private float lastGroundedTime;
    private float groundedDuration = 0.1f; // Duration to check for wasGrounded

    public bool wasGrounded
    {
        get
        {
            return (Time.time - lastGroundedTime) <= groundedDuration;
        }
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")
            // && velocityIsLow // REMOVED FOR PLATFORMS
            )
        {
            Debug.Log("Grounded");
            isGrounded = true;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground")
            // && velocityIsLow // REMOVED FOR PLATFORMS
            )
        {
            isGrounded = true;
        }
    }


    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}