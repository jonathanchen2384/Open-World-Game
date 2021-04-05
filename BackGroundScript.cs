using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScript : MonoBehaviour
{
    public float speed;

    public Transform cameraPos;

    private float bound;
    public float threshold;

    public float offSet;
    public float YoffSet;
    public float smoothen;

    private void Start()
    {
        transform.position = new Vector2(cameraPos.position.x, transform.position.y);
    }

    void FixedUpdate()
    {
        bound = Mathf.Abs(transform.position.x - cameraPos.position.x);

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.Translate(Vector2.right * speed);
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.Translate(Vector2.left * speed);
        }

        //Debug.Log(bound);

        if (bound >= threshold)
        {
            transform.position = new Vector2(cameraPos.position.x, transform.position.y);
        }

        Vector3 smoothPos = Vector3.Lerp(transform.position, new Vector3(transform.position.x, (cameraPos.position.y*offSet)+YoffSet, transform.position.z), smoothen);
        transform.position = smoothPos;
    }
}
