using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackScript : MonoBehaviour
{
    public int attack;
    public int skyDmg;
    public int groundDmg;

    private float duration;
    public float startTime;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public float skyAttackRange;
    private float groundAttackRange;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;

    public GameObject attackEffect;
    public GameObject hitEffect;

    public Animator anim;

    public Animator screen;

    private float skyTime;
    public float startSkyTime;
    private bool inSky = false;
    private float gravity;

    private bool attackPressed;

    void Start()
    {
        duration = startTime;
        skyTime = startSkyTime;
        gravity = gameObject.GetComponent<moveScript>().RB.gravityScale;
        groundAttackRange = attackRange;
        attackPressed = false;
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        if (duration <= 0)
        {
            if (attackPressed == true)
            {
                if (isGrounded == true)
                {
                    //normal attack
                    attack = groundDmg;
                    anim.SetTrigger("attack");
                    screen.SetTrigger("shake");
                    
                    Instantiate(attackEffect, transform.position, Quaternion.identity);
                    Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        Instantiate(hitEffect, transform.position, Quaternion.identity);
                        enemies[i].GetComponent<healthScript>().health -= attack;
                    }
                }

                if (isGrounded == false)
                {
                    //sky attack
                    attack = skyDmg;
                    inSky = true;
                    attackRange = skyAttackRange;
                    anim.SetTrigger("skyAttack");
                    screen.SetTrigger("shake");
                    Instantiate(attackEffect, transform.position, Quaternion.identity);
                    Instantiate(attackEffect, transform.position, Quaternion.identity);
                    Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        Instantiate(hitEffect, transform.position, Quaternion.identity);
                        enemies[i].GetComponent<healthScript>().health -= attack;
                    }
                   
                }
                duration = startTime;
                attackPressed = false;
            }

            if (isGrounded == true)
            {
                skyTime = startSkyTime;
            }

            if (inSky == true) {
                if (skyTime > 0)
                {
                    gameObject.GetComponent<moveScript>().RB.gravityScale = 0f;
                    gameObject.GetComponent<moveScript>().RB.velocity = new Vector2(0f,0f);
                    gameObject.GetComponent<moveScript>().canMove = false;
                    skyTime -= Time.deltaTime;
                }

                else
                {
                    gameObject.GetComponent<moveScript>().RB.gravityScale = gravity;
                    gameObject.GetComponent<moveScript>().canMove = true;
                    attackRange = groundAttackRange;
                    attack /= 2;
                    inSky = false;
                }

            }
        }

        else {
            duration -= Time.deltaTime;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(attackPos.position,attackRange);
    }

    public void touchAttack()
    {
        attackPressed = true;
    }
}
