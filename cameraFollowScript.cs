using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollowScript : MonoBehaviour
{
    public Transform playerPOS;

    public float smoothen;

    public float offsetX;
    public float offsetY;

    public float upperLimit;
    public float lowerLimit;
    public float leftLimit;
    public float rightLimit;

    private float targetPosX;
    private float targetPosY;

    private void Start()
    {
        targetPosX = playerPOS.position.x + offsetX;
        targetPosY = playerPOS.position.y + offsetY;
    }

    void FixedUpdate()
    {
        if (playerPOS.position.x < leftLimit || playerPOS.position.x > rightLimit)
        {
            targetPosX = transform.position.x;
        }

        else if (playerPOS.position.x >= leftLimit || playerPOS.position.x <= rightLimit)
        {
            targetPosX = playerPOS.position.x + offsetX;
        }

        if (playerPOS.position.y < lowerLimit || playerPOS.position.y > upperLimit)
        {
            targetPosX = transform.position.y;
        }

        else if (playerPOS.position.x >= lowerLimit || playerPOS.position.x <= upperLimit)
        {
            targetPosY = playerPOS.position.y + offsetY;
        }

        else {
            targetPosX = playerPOS.position.x + offsetX;
            targetPosY = playerPOS.position.y + offsetY;
        }

        Vector3 smoothPos = Vector3.Lerp(transform.position, new Vector3(targetPosX, targetPosY, -10), smoothen);

        transform.position = smoothPos;
        //Debug.Log("X: " + playerPOS.position.x + "  Y: " + playerPOS.position.y);
    }
}
