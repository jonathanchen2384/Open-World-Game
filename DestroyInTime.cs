using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInTime : MonoBehaviour
{
    public float Duration;

    void Update()
    {
        if (Duration <= 0)
        {
            Destroy(gameObject);
        }

        else
        {
            Duration -= Time.deltaTime;
        }
    }
}
