using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming; // Tobii library

public class EyeTrackingTarget : MonoBehaviour
{
    /* NOTE TO SELF: GazeAware objects need to have a mesh in order to be picked up by eye tracking! To achieve bigger hitboxes for eye tracking:
     * Use invisible material on mesh that represents hitbox, attach bigger hitbox as child to parent object
     * ALSO DON'T FORGET TO SET COLLISION LAYER TO EyeTracking!!!
     */


    GazeAware aware; // Component for checking if object has gaze focus
    public Renderer objectMaterial; // Testing component
    private float gazeThreshold = 1.5f;
    private float gazeTimer;
    private float shiftPercentage;

    // Start is called before the first frame update
    void Start()
    {
        // Getting components from gameobject
        aware = GetComponent<GazeAware>();
        //objectMaterial = GetComponentInParent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTimer();


    }

    public float GazeTimer()
    {
        return gazeTimer / gazeThreshold; // Returns the percentage
    }

    void ProcessTimer()
    {
        if (aware.HasGazeFocus) // If the player's eye focus is on this object
        {
            gazeTimer += Time.deltaTime / gazeThreshold; // Increment timer while user is looking at GazeAware target
        }

        else // If their eye focus is elsewhere
        {
            gazeTimer -= Time.deltaTime / gazeThreshold; // Decrement timer otherwise

        }

        gazeTimer = Mathf.Clamp(gazeTimer, 0.0f, gazeThreshold); // Make sure value does not go out of bounds

        objectMaterial.material.SetColor("_BaseColor", Color.Lerp(Color.blue, Color.red, gazeTimer));
        
    }
}
