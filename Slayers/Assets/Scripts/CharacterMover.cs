using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float jumpStrength;
    private Rigidbody2D rb;
    private bool isOnGround;
    private int jumpsLeft;
    private float jumpFactor;
    private CharacterAnimator animator;
    private bool canJump = true;

    public bool IsOnGround
    {
        get { return isOnGround; }
    }

    private void Awake()
    {
        animator = new CharacterAnimator(GetComponent<Animator>());
        rb = GetComponent<Rigidbody2D>();
        isOnGround = false;
        jumpsLeft = 2;
        this.jumpFactor = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }

        refreshJumps();
    }

    private void jump()
    {
        if(jumpsLeft > 0 && this.canJump)
        {
            animator.triggerJumpAnimation();
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpStrength * this.jumpFactor);
            this.isOnGround = false;
            jumpsLeft--;
        }
    }

    private void refreshJumps()
    {
        if (this.isOnGround) this.jumpsLeft = 2;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Floor>() != null)
        {
            this.animator.triggerRollAnimation();
            this.isOnGround = true;
        }
    }

    private void disableJump()
    {
        this.canJump = false;
    }

    private void enableJump()
    {
        this.canJump = true;
    }
}

