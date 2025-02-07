using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public GameObject RestartPannel, PauseMenu;
    public TextMeshProUGUI maxScoreText, currentScore;

    public static bool gameIsOver, isPaused = false;
    private static int elenasugepula = 0;
    public static int maxScore;

    void Start()
    {
       
    }

    void Update()
    {
        if (gameIsOver)
        {
            Time.timeScale = 0.5f;
            RestartPannel.SetActive(true);

            if (RoomGeneraton.roomsSpawned > maxScore)
                maxScore = RoomGeneraton.roomsSpawned;

            currentScore.text = "You lost on room " + (RoomGeneraton.roomsSpawned);
            maxScoreText.text = "Most rooms survived: " + maxScore;
        }
        else
        {
            Time.timeScale = 1;
            RestartPannel.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            if (!isPaused)
                Puase();
            else
                Resume(); 
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Layer1");
        PlayerScript.health = 100;
        gameIsOver = false;
        isPaused = false;
        RoomGeneraton.roomsSpawned = 0;
    }

    public void returnToHub() 
    {
        SceneManager.LoadScene("Hub");
        PlayerScript.health = 100;
        gameIsOver = false;
        isPaused = false;
        RoomGeneraton.roomsSpawned = 0;
        Time.timeScale = 1;
    }

    void Puase() 
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }
    public void Resume() 
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        gameIsOver = false;
    }
}
