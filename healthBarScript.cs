using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBarScript : MonoBehaviour
{
    private float getSize;

    public GameObject enemyLife;

    void Update()
    {
        getSize = enemyLife.GetComponent<healthScript>().size;

        Vector3 Scaler = transform.localScale;
        Scaler.x = getSize;
        transform.localScale = Scaler;
    }
}
