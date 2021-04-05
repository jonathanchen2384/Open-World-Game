using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public int fullHealth;
    private int health;

    public GameObject hurtEffect;
    public GameObject deathEffect;

    private bool isAlive;
    public Animator anim;

    void Start()
    {
        health = fullHealth;
    }

    void FixedUpdate()
    {
        if (health <= 0)
        {
            if (isAlive == true)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                GetComponent<moveScript>().canMove = false;
                isAlive = false;
            }
        }

        else {
            isAlive = true;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            playerHurt();
            health--;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            health = fullHealth;
            GetComponent<moveScript>().canMove = true;
        }
    }

    public void playerHurt()
    {
        anim.SetTrigger("hurt");
        Instantiate(hurtEffect, transform.position, Quaternion.identity);
    }
}
