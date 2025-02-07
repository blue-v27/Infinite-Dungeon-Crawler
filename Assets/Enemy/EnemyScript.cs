using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject deathEffect, bullet;
    
    public static bool isFollowing = false, isFollowingPlayer = false;
    bool playerIsInRange = false;
    public static float followTime;

    public float health, damage;

    int isFacingRight = 1;

    public Rigidbody2D rb;
    float freezTime, shotDelay;

    float xSclae;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        shotDelay = Random.Range(.5f, 1.5f) + Time.time;
    }

    void Update()
    {
        if (!GameManager.gameIsOver && !GameManager.isPaused)
        {
            // PlayerFollowing

            if (playerIsInRange)
            {
                transform.position = Vector3.Lerp(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, 1f * Time.deltaTime);

                /// shootPlayer

                if (Time.time - shotDelay > 1f)
                {
                    Instantiate(bullet, transform.position, Quaternion.identity);
                    shotDelay = Time.time;
                }
            }

            // CollisionBugFixing

            if (Time.time - freezTime > .1f)
            {
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

            if (health <= 0)
            {
                Destroy(gameObject);
                RoomGeneraton.enemiesInRoom--;
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                SoundManager.PlaySound("enemydes");
            }
        }
    }

    void Flip() 
    {
        xSclae *= -1;
        isFacingRight *= -1;
        transform.localScale = new Vector3(xSclae, transform.localScale.x, transform.localScale.z);
    }

    void OnTriggerEnter2D(Collider2D collision)
    { 
        // PlayerFollowing

       if (collision.tag == "Player")
       {
            isFollowing = true;
            followTime = Time.time;
            playerIsInRange = true;

            // CollisionBugFixing

            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            freezTime = Time.time;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bullet")
        {
            health -= damage;
            Destroy(collision.gameObject);
            GetComponentInChildren<TextMeshPro>().text = health + "";
            SoundManager.PlaySound("enemyDamage");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // CollisionBugFixing

        if(collision.tag == "Player") 
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            freezTime = Time.time;
            playerIsInRange = false;
        }
    }
}
