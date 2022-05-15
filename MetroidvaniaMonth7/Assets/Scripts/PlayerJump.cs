﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour, IJump, IGrounded
{
    [HideInInspector]
    public float jumpPower = 5f;
    [SerializeField]
    bool isGrounded;
    bool wasGrounded = true;
    Jump jump;
    public Transform groundCheck;
    public float checkRadius;
    [SerializeField]
    LayerMask groundLayerMask;
    bool isAbilityInUse = false;
    Animator animator;
    Rigidbody2D rb;

    public float JumpPower { get => jumpPower; set => jumpPower = value; }

    public bool IsAbilityInUse => isAbilityInUse;

    public bool IsGrounded => isGrounded;

    public LayerMask GroundLayerMask => groundLayerMask;

    // Start is called before the first frame update
    public void Initialize()
    {
        jump = GetComponent<Jump>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void JumpAbilityTick()
    {
        Jump();

        if(!isGrounded && rb.velocity.y < 0)
        {
            animator.SetBool("IsFalling", true);
            animator.SetBool("IsJumping", false);
        }
    }

    void OnJump()
    {
        if (isGrounded)
        {
            animator.SetBool("IsJumping", true);
            jump.JumpMove(jumpPower);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Jump");
        }
    }
    public void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayerMask);

        // if (Input.GetButtonDown("Jump") && isGrounded)
        // {
        //     animator.SetBool("IsJumping", true);
        //     jump.JumpMove(jumpPower);
        //     FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Jump");
        //
        // }

        if (isGrounded != wasGrounded)
        {
            wasGrounded = !wasGrounded;

            if (isGrounded == true)
            {
                animator.SetBool("IsJumping", false);
                animator.SetBool("IsFalling", false);
                FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Landed");
            }

        }
    }
}

