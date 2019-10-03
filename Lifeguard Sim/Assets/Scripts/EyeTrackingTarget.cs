using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming; // Tobii library

public class EyeTrackingTarget : MonoBehaviour
{
    GazeAware aware; // Component for checking if object has gaze focus
    Material objectMaterial; // Testing component

    // Start is called before the first frame update
    void Start()
    {
        // Getting components from gameobject
        aware = GetComponent<GazeAware>();
        objectMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(aware.HasGazeFocus) // If the player's eye focus is on this object
        {
            objectMaterial.color = Color.red; // Turn red
        }

        else // If their eye focus is elsewhere
        {
            objectMaterial.color = Color.blue; // Turn blue
        }

    }
}
