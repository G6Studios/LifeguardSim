using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject swimmer; // Test variable

    public bool displayTrackerWarning;

    // Pause menu related
    private bool gamePaused = false; // If the game was manually paused by the user

    private bool swimmersSpawned = false;

    private void Awake()
    {
        SpawnSwimmers();
    }

    // Update is called once per frame
    void Update()
    {
        // Not needed with mainmenu script
        //if (SceneManager.GetActiveScene().name.Equals("Main Menu")) // Main menu scene
        //{
        //
        //}
        //
        //else if(SceneManager.GetActiveScene().name.Equals("Swimming Pool"))
        //{
        //    SpawnSwimmers();
        //}
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

    public void StartScene()
    {
        SceneManager.LoadScene("Swimming Pool");
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
