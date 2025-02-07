using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject bullet;
    
    public static Vector3 mousePosition, enemyPosition;

    public float timeBetweenShots;
    float shotTime;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //Manual shhoting
        /*

        if(Input.GetMouseButtonDown(0) && Time.time - shotTime > timeBetweenShots)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(bullet, transform.position, Quaternion.identity);

            shotTime = Time.time;
            SoundManager.PlaySound("shootSound");
        }

        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyBody")
        {
            enemyPosition = collision.transform.position;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.time - shotTime > timeBetweenShots && collision.tag == "EnemyBody")
        {
            enemyPosition = collision.transform.position;

            Instantiate(bullet, transform.position, Quaternion.identity);

            shotTime = Time.time;

            SoundManager.PlaySound("shootSound");
        }
    }
}
