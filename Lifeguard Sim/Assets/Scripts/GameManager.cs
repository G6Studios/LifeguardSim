﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public playerstats statistics;

    public GameObject swimmer; // Test variable

    public bool displayTrackerWarning;

    public float timerMax = 30f;

    public float gameTimer; // Timer for how long the user has left

    // Pause menu related
    private bool gamePaused = false; // If the game was manually paused by the user

    private bool swimmersSpawned = false;

    public UIManager ui_manager;

    private void Awake()
    {
        SpawnSwimmers();
    }

    private void Start()
    {
        
        gameTimer = timerMax;
        statistics.reset = false;
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateTimer();
    }


    private void SpawnSwimmers()
    {
        if (!swimmersSpawned)
        {
            for (int i = 0; i < 10; i++)
            {
                Vector3 random = new Vector3(Random.Range(-5, 5), 0f, Random.Range(-5, 5)); // Random spawn location

                Instantiate(swimmer, random, Quaternion.identity);
            }
        }

        swimmersSpawned = true;
    }

    public void UpdateTimer()
    {
        gameTimer = Mathf.Clamp(gameTimer - Time.deltaTime, 0f, timerMax); // Time ticking down

        // Formatting time into minutes and seconds
        string minutes = Mathf.Floor(gameTimer / 60).ToString("00");
        string seconds = (gameTimer % 60).ToString("00");

        string formattedTime = string.Format("{0}:{1}", minutes, seconds); // Formatted time for easy access

        ui_manager.TimerText(formattedTime);

        if (gameTimer <= 0f)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene("Results");
    }

    private void ProcessPause(bool trackerStatus, bool userStatus)
    {
        //if(trackerStatus)
    }

    internal void AutoPause()
    {
        Debug.Log("Autopause called!");
    }
}