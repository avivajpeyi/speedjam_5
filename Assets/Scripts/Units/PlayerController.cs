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

    
    public event Action<bool, float> GroundedChanged;



    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _groundCheck = GetComponent<GroundCheck>();
        dragManager = GetComponentInChildren<DragManager>();
        dragManager.OnMouseRelease.AddListener(TriggerJump);
    }
    
    //
    // protected virtual void HandleCollisions() {
    //     // Hit a Ceiling
    //     if (_speed.y > 0 && _ceilingHitCount > 0) _speed.y = 0;
    //
    //     // Landed on the Ground
    //     if (!_grounded && _groundHitCount > 0) {
    //         _grounded = true;
    //         ResetDash();
    //         ResetJump();
    //         GroundedChanged?.Invoke(true, Mathf.Abs(_speed.y));
    //     }
    //     // Left the Ground
    //     else if (_grounded && _groundHitCount == 0) {
    //         _grounded = false;
    //         _frameLeftGrounded = _fixedFrame;
    //         GroundedChanged?.Invoke(false, 0);
    //     }
    // }

    private float jumpForce
    {
        get
        {
            return Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));
        }
    }
    
    
    

    public void TriggerJump()
    {
        if (_groundCheck.isGrounded)
        {
            Debug.Log("Jump!");
            // _playerFx.PlayJumpFX();

            rb.AddForce(
                jumpForce * -dragManager.DragDirection * dragManager.Percent,
                ForceMode2D.Impulse
            );
            dragManager.Hide();
        }
    }


    void Update()
    {
        if (_groundCheck.isGrounded)
        {
            // _playerFx.PlayLandFX();
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