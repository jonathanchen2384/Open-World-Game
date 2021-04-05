using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveScript : MonoBehaviour
{

    public Rigidbody2D RB;
    public float speed;
    public float jumpForce;
    public float upVal;
    public float moveInput;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;

    private bool isClimb;
    public Transform wallCheck;
    public float SlideSpeed;
    public float climbForce;

    public int extrajumps;
    private int  jumps;

    private bool facingRight = true;

    public Animator anim;

    public GameObject feetDust;
    public float startTime;
    private float duration;

    public GameObject jumpEffect;

    private bool landing = true;
    public GameObject landEffect;

    public bool canMove = true;

    private bool jumpPressed;

    public Joystick joystick;

    void Start()
    {
        upVal = jumpForce;
        jumps = extrajumps;
        duration = startTime;
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,checkRadius,groundLayer);
        isClimb = Physics2D.OverlapCircle(wallCheck.position, checkRadius, groundLayer);

        if (canMove == true)
        {
            if (joystick.Horizontal > .3f)
            {
                moveInput = 1;
            }
            else if (joystick.Horizontal < -.3f)
            {
                moveInput = -1;
            }

            else {
                moveInput = 0;
            }

            if (joystick.Vertical > .3f)
            {
                jumpPressed = true;
            }

            else if (joystick.Vertical <= .3f)
            {
                jumpPressed = false;
            }

            RB.velocity = new Vector2(moveInput * speed, RB.velocity.y);
        }

        if (moveInput > 0 && facingRight == false)
        {
            flip();
        }

        if (moveInput < 0 && facingRight == true)
        {
            flip();
        }
    }

    void Update()
    {

        if (isGrounded == true)
        {
            anim.SetBool("climb", false);
            jumps = extrajumps;
            anim.SetBool("isJumping", false);

            if (landing == false)
            {
                Instantiate(landEffect, transform.position, Quaternion.identity);
                landing = true;
            }
        }

        if (isGrounded == false)
        {
            landing = false;
            if (isClimb == true)
            {
                RB.velocity = new Vector2(RB.velocity.x, Mathf.Clamp(RB.velocity.y, -SlideSpeed, float.MaxValue));
                jumps = extrajumps;
                upVal = climbForce;
                anim.SetBool("isJumping", false);
                if (jumpPressed == false)
                {
                    anim.SetBool("climb", true);
                }
            }
        }

        if (isClimb == false)
        {
            anim.SetBool("climb", false);
            anim.SetBool("ClimbUp", false);
            upVal = jumpForce;
        }

        if (jumpPressed == true)
        {
            anim.SetBool("climb", false);

            if (jumps > 0 && canMove == true)
            {

                
                RB.velocity = new Vector2(RB.velocity.x, upVal);
                jumps--;

                if (isClimb == false)
                {
                    anim.SetBool("isJumping", true);
                    Instantiate(jumpEffect, groundCheck.position, Quaternion.identity);
                }

                if (isClimb == true)
                {
                    anim.SetBool("ClimbUp", true);
                }
            }  
        }

        if (moveInput == 0)
        {
            anim.SetBool("isRunning", false);
        }

        if (isGrounded == true && moveInput != 0)
        {
            anim.SetBool("isRunning", true);
            if (duration <= 0)
            {
                Instantiate(feetDust, groundCheck.position, Quaternion.identity);
                duration = startTime;
            }
            else {
                duration -= Time.deltaTime;
            }
        }
    }

    private void flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
