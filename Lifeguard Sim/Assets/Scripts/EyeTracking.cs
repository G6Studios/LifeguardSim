using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Used for sprite tracking player's gaze position
using Tobii.Gaming; // This is necessary for using the Tobii SDK functions
using Tobii;
using TMPro; // Used for TextMeshPro

public class EyeTracking : MonoBehaviour
{
    public GameManager manager;

    // Eye tracking
    [SerializeField]
    private Camera viewCamera; // Player's viewport camera
    private bool trackerConnected; // For checking if eye tracking is connected
    private Vector2 smoothPoint; // Smoothing player's gaze position
    [SerializeField]
    private Image gazePosImage; // Image showing where player is looking

    // Tracker warning fields
    [SerializeField]
    private GameObject trackerWarning; // Canvas element that informs players when eye tracking is not working

    // User presence tracking
    UserPresence presence;
    private bool userIsPresent;
    
    // Start is called before the first frame update
    void Start()
    {
        TobiiAPI.SetCurrentUserViewPointCamera(viewCamera); // Setting view camera for Tobii eye tracking
        manager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        trackerConnected = TobiiAPI.IsConnected; // Status of tobii eye tracker


        
        if(trackerConnected) // Tracker is connected and functioning properly
        {
            ProcessTracking(); // Update eye tracking every frame
            ProcessUserPresence(); // Update user presence every frame
            trackerWarning.SetActive(false); // Make sure tracker warning is not shown
        }

        else if(!trackerConnected) // If tracker is not connected
        {
            if(manager.displayTrackerWarning) // If the tracker warning is set to be shown
            {
                trackerWarning.SetActive(true); // Show tracker not connected warning
            }

            else if(!manager.displayTrackerWarning) // If the tracker warning is set to NOT be shown
            {
                //ProcessTracking(); // Update eyetracking
                //ProcessUserPresence();
                trackerWarning.SetActive(false); // Don't show tracker warning
            }
        }

    }

    private void ProcessTracking() // Function for updating eye tracking for player
    {
        Vector2 gazePoint = TobiiAPI.GetGazePoint().Screen; // Get where player is looking in viewport coordinates

        smoothPoint = Vector2.Lerp(smoothPoint, gazePoint, 0.5f); // Smoothing player's gaze point

        gazePosImage.transform.position = new Vector3(smoothPoint.x, smoothPoint.y, 0f); // Updating sprite position to where player is looking
    }

    private void ProcessUserPresence()
    {
        presence = TobiiAPI.GetUserPresence(); // Update user presence every frame

        if(presence.IsUserPresent()) // If the user is present
        {
            userIsPresent = true;
        }

        else // If the user is not present
        {
            userIsPresent = false;
        }

    }

    // CHANGE THIS AROUND
    public bool TrackerConnected
    {
        get { return trackerConnected; }

        set
        {
            trackerConnected = value;
            if(trackerConnected == false)
            {
                manager.AutoPause();
            }
        }
    }

    public bool UserIsPresent
    {
        get { return userIsPresent; }

        set
        {
            userIsPresent = value;
        }
    }
}
