using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System.Transactions;

public class RoomGeneraton : MonoBehaviour
{
    public GameObject[] wall, doorWall, door;
    public GameObject enemy, room, deathEffect;

    public TextMeshPro roomText;

    private float xOffset, yOffset; 
    private int doorPosition;

    public static int enemiesInRoom, previousDoorPosition, roomsSpawned;

    private Vector2 nextRoomPosition;

    private bool hasCleardRoom;

    void Start()
    {
        //Vector2 newScale = new Vector2(Random.Range(1f, 2f), Random.Range(1f, 2f));
        //transform.localScale = newScale;
        
        xOffset = transform.localScale.x * 25f + .3f;           //never touch this numbers
        yOffset = transform.localScale.y * 14f + .096f;         //NEVER

        roomText.text = roomsSpawned + " ";

        for (int i = 0; i < 4; i++)
        {
            wall[i].SetActive(true);
            door[i].SetActive(true);
        }
        
        doorPosition = Random.Range(0, 3);

        if(roomsSpawned > 0)
        {
            wall[previousDoorPosition].SetActive(false);
            door[previousDoorPosition].SetActive(false);
        }

        do
        {
            doorPosition = Random.Range(0, 3);
        } 
        while 
            (doorPosition == previousDoorPosition);

        wall[doorPosition].SetActive(false);
        doorWall[doorPosition].SetActive(true);

        enemiesInRoom = Random.Range(1, 5);

        float xPos = transform.position.x, yPos = transform.position.y;

        for(int i = 1; i <= enemiesInRoom; i++)
            Instantiate(enemy, new Vector3(Random.Range(xPos - 7f, xPos + 7f), Random.Range(yPos - 4f, yPos + 4f), -3), Quaternion.identity);
    }

    // Update is called once per fram
    void Update()
    {
        if (enemiesInRoom < 1 && !hasCleardRoom)
        {
            Instantiate(deathEffect, door[doorPosition].transform.position, door[doorPosition].transform.rotation);
            
            door[doorPosition].SetActive(false);

            chooseNextPosition();

            Instantiate(room, nextRoomPosition, Quaternion.identity);

            hasCleardRoom = true;

            roomsSpawned++;

            EnemyScript.isFollowing = false;
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            EnemyScript.isFollowing = true;
        }
            
    }

    void chooseNextPosition()
    {
        switch (doorPosition) 
        {
            case 0:
                nextRoomPosition = new Vector3(transform.position.x - xOffset, transform.position.y, 3f);
                previousDoorPosition = 2;
                break;
            case 1:
                nextRoomPosition = new Vector3(transform.position.x, transform.position.y + yOffset, 3f);
                previousDoorPosition = 3;
                break;
            case 2:
                nextRoomPosition = new Vector3(transform.position.x + xOffset, transform.position.y, 3f);
                previousDoorPosition = 0;
                break;
            case 3:
                nextRoomPosition = new Vector3(transform.position.x, transform.position.y - yOffset, 3f);
                previousDoorPosition = 1;
                break;
        }
     }
}
