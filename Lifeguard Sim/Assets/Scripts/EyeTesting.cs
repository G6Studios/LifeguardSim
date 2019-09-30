using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Used for sprite tracking player's gaze position
using Tobii.Gaming; // This is necessary for using the Tobii SDK functions
using TMPro; // Used for TextMeshPro

public class EyeTesting : MonoBehaviour
{
    [SerializeField]
    private bool toggleTrackerWarning;

    [SerializeField]
    private Camera viewCamera; // Player's viewport camera
    private bool trackerConnected; // For checking if eye tracking is connected
    private Vector2 smoothPoint; // Smoothing player's gaze position
    [SerializeField]
    private Image gazePosImage; // Image showing where player is looking

    [SerializeField]
    private GameObject trackerWarning; // Canvas element that informs players when eye tracking is not working
    
    // Start is called before the first frame update
    void Start()
    {
        TobiiAPI.SetCurrentUserViewPointCamera(viewCamera); // Setting view camera for Tobii eye tracking
    }

    // Update is called once per frame
    void Update()
    {
        trackerConnected = TobiiAPI.IsConnected; // Status of tobii eye tracker
        
        if(trackerConnected) // Tracker is connected and functioning properly
        {
            EyeTracking(); // Update eye tracking every frame
            trackerWarning.SetActive(false);
        }

        else if(toggleTrackerWarning == false)
        {
            EyeTracking(); // Update eye tracking every frame
            trackerWarning.SetActive(false);
        }

        else // Something is wrong with tobii eye tracker
        { 
            Time.timeScale = 0f; // Pause game
            trackerWarning.SetActive(true); // Inform user that eye tracking is not functioning
        }

    }

    private void EyeTracking() // Function for updating eye tracking for player
    {
        Vector2 gazePoint = TobiiAPI.GetGazePoint().Screen; // Get where player is looking in viewport coordinates

        smoothPoint = Vector2.Lerp(smoothPoint, gazePoint, 0.5f); // Smoothing player's gaze point

        gazePosImage.transform.position = new Vector3(smoothPoint.x, smoothPoint.y, 0f);
    }
}
