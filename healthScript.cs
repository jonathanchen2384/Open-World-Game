using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthScript : MonoBehaviour
{
    public float fullHealth;

    public float fullSize;
    public float size;
    public float health;

    public GameObject deathEffect;

    private void Start()
    {
        health = fullHealth;
        size = fullSize;
    }

    void Update()
    {
        size = (health / fullHealth) * fullSize;

        if (health <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
