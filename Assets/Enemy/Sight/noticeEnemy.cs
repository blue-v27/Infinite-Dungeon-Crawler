using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noticeEnemy : MonoBehaviour
{
    public int percentageToSee;
    int randomValue = 69;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            randomValue = Random.Range(0, percentageToSee);

            if (randomValue == 0)
            {
                EnemyScript.isFollowing = true;
            }

            EnemyScript.followTime = Time.time;

            if (PlayerScript.playerSpeed_x > 5f || PlayerScript.playerSpeed_y > 5f || EnemyScript.isFollowing == true)
                randomValue = 0;

        }
    }
}
