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
    private float gazeThreshold = 3.0f;
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

    void ProcessTimer()
    {
        if (aware.HasGazeFocus) // If the player's eye focus is on this object
        {
            gazeTimer += Time.deltaTime; // Increment timer while user is looking at GazeAware target
        }

        else // If their eye focus is elsewhere
        {
            gazeTimer -= Time.deltaTime; // Decrement timer otherwise
            
        }

        gazeTimer = Mathf.Clamp(gazeTimer, 0.0f, gazeThreshold); // Make sure value does not go out of bounds

        if(gazeTimer >= gazeThreshold) 
        {
            objectMaterial.material.color = Color.red; // Turn red
        }

        else
        {
            objectMaterial.material.color = Color.blue; // Turn blue
        }
    }
}
