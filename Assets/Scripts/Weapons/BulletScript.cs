using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Vector2 direction;
    void Start()
    {
        direction = WeaponScript.enemyPosition;

        Invoke("Delete", .33f);
    }

    void Update()
    {
        if (this.tag == "enemyBullet") direction = PlayerScript.PlayerPosition;
      
        transform.position = Vector2.Lerp(transform.position, direction, Time.deltaTime * 10f);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    void Delete() 
    {
        Destroy(gameObject);
    }
}
