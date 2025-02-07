using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static float speed = 10f;
    float xAxis, yAxis, timeBetweenDashes, scaleX;

    int isFacingRight = 1;

    public static int health = 100;

    public Rigidbody2D rb;

    public GameObject dashEffect, dashAnimation, damgeEffect, bullet;

    public static Vector3 PlayerPosition;

    public static string currentSkin;

    private bool hasTakedDamage = false;

    ///camera Shake
    Vector3 cameraInitialPosition;
    public float shakeMagnetude = 0.05f, shakeTime, raycastDistance;
    public Camera mainCamera;
    public static bool shake_camera = false;


    public static float playerSpeed_x, playerSpeed_y, playerPos_X;

    public LayerMask ignoreLayer;

    RaycastHit2D hit;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scaleX = transform.localScale.x;


        switchSkin();
    }

    void FixedUpdate()
    {
        if (!GameManager.gameIsOver && !GameManager.isPaused)
        {
            /// basic movemet

            xAxis = Input.GetAxisRaw("Horizontal");
            yAxis = Input.GetAxisRaw("Vertical");

            rb.velocity = new Vector2(xAxis, yAxis) * speed;
            playerSpeed_x = xAxis * speed;
            playerSpeed_y = yAxis * speed;
            playerPos_X = transform.position.x;

            PlayerPosition = transform.position;

            /// dash

            if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time - timeBetweenDashes > 1f)
            {
                Dash();
            }

            /// Flip 

            if ((Input.GetKeyDown(KeyCode.A) && isFacingRight == 1) || (Input.GetKeyDown(KeyCode.D) && isFacingRight == -1))
            {
                FlipPlayer();
            }

            // Health check

            if (health <= 0)
                Die();
        }
    }

    void Dash()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(xAxis, yAxis), raycastDistance, ignoreLayer);

        if (hit.collider == null)
            transform.Translate(new Vector2(xAxis, yAxis) * speed / 3);

        timeBetweenDashes = Time.time;

        Instantiate(dashEffect, transform.position, Quaternion.identity);
        Instantiate(dashAnimation, transform.position, Quaternion.identity);

        ShakeCamera();
        SoundManager.PlaySound("dashSound");
    }

    void FlipPlayer()
    {
        scaleX *= -1;
        transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.x);
        isFacingRight *= -1;
    }

    void Die()
    {
        Instantiate(dashEffect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        GameManager.gameIsOver = true;
        SoundManager.PlaySound("gameOverSound");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            health -= 10;
            Instantiate(damgeEffect, transform.position, Quaternion.identity);
            ShakeCamera();
        }

        if (collision.collider.tag == "enemyBullet")
        {
            health -= 10;
            ShakeCamera();
            Instantiate(damgeEffect, transform.position, Quaternion.identity);
            SoundManager.PlaySound("hiitSound");
        }

        // switch skin

        if (collision.collider.tag == "skinChanger")
        {
            currentSkin = collision.collider.name;
            SoundManager.PlaySound("skinWitch");
            switchSkin();
        }
    }
    
    void Colors(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
        GetComponentInChildren<Light>().color = color;
        bullet.GetComponent<SpriteRenderer>().color = color;
        dashEffect.GetComponent<ParticleSystem>().startColor = color;
    }
    void switchSkin() 
    {

        switch (currentSkin) 
        {
            case "whiteSkin":
                Colors(Color.white);
                GetComponentInChildren<Light>().color = Color.gray;
                break;
            case "redSkin":
                Colors(Color.red);              
                break;
            case "graySkin":
                Colors(Color.gray);
                break;
            case "blackSkin":
                Colors(Color.gray);
                GetComponent<SpriteRenderer>().color = Color.black;       
                break;
            case "blueSkin":
                Colors(Color.blue);
                break;
            case "greenSkin":
                Colors(Color.green);
                break;
            case "magentaSkin":
                Colors(Color.magenta);
                break;
            case "yellowSkin":
                Colors(Color.yellow);
                break;
            case "cyanSkin":
                Colors(Color.cyan);
                break;
        }
    }
    public void ShakeCamera()
    {
        cameraInitialPosition = mainCamera.transform.position;
        InvokeRepeating("StartCameraShaking", 0f, 0.005f);
        Invoke("StopCameraShaking", shakeTime);
    }

    void StartCameraShaking()
    {
        float cameraShakingOffsetX = Random.value * shakeMagnetude * 2 - shakeMagnetude;
        float cameraShakingOffsetY = Random.value * shakeMagnetude * 2 - shakeMagnetude;
        Vector3 cameraIntermadiatePosition = mainCamera.transform.position;
        cameraIntermadiatePosition.x += cameraShakingOffsetX;
        cameraIntermadiatePosition.y += cameraShakingOffsetY;
        mainCamera.transform.position = cameraIntermadiatePosition;
    }

    void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");
        mainCamera.transform.position = cameraInitialPosition;
        shake_camera = false;
    }

}
