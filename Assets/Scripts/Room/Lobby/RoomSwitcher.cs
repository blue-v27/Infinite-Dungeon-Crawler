using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomSwitcher : MonoBehaviour
{
    public GameObject roomSwitchEffect;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Invoke("SwitchRoom", .35f);
            Instantiate(roomSwitchEffect, transform.position, Quaternion.identity);
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            Invoke("SwitchRoom", .35f);
            Instantiate(roomSwitchEffect, transform.position, Quaternion.identity);
        }
            
    }

    void SwitchRoom()
    {
        SceneManager.LoadScene("Layer1");
    }
}
