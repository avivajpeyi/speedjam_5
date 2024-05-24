using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool isGrounded;

    [SerializeField] private float vMag;
    private float vThreshold = 2.0f;

    bool velocityIsLow
    {
        get { return rb.velocity.magnitude <= vThreshold; }
    }

    private void Update()
    {
        vMag = rb.velocity.magnitude;
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
        if (col.gameObject.CompareTag("Ground") && velocityIsLow)
        {
            Debug.Log("Grounded");
            isGrounded = true;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") && velocityIsLow)
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