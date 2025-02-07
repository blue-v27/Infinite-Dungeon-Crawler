using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Vector2 Velocity;

    public float smoothTimeY;
    public float smoothTimeX;

    public GameObject Player;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        float posx = Mathf.SmoothDamp(transform.position.x, Player.transform.position.x, ref Velocity.x, smoothTimeX);
        float posy = Mathf.SmoothDamp(transform.position.y, Player.transform.position.y + 1, ref Velocity.y, smoothTimeY);

        transform.position = new Vector3(posx, posy, transform.position.z);
    }
}
