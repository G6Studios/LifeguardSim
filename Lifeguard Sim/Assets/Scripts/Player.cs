﻿using UnityEngine;

public class Player : MonoBehaviour
{
    private SwimmerManager s_manager;
    private UIManager uIManager;
    private EyeTracking tracker;

    private Vector3 diagnosisPoint;
    private Vector3 cameraDiagnosisPoint;
    private Camera sceneCamera;

    private GameObject gazeTarget;

    private GameObject grabbedSwimmer;

    // Start is called before the first frame update
    private void Start()
    {
        s_manager = gameObject.GetComponent<SwimmerManager>();
        uIManager = GameObject.FindObjectOfType<UIManager>();
        tracker = gameObject.GetComponent<EyeTracking>();
        diagnosisPoint = GameObject.Find("Diagnosis Point").GetComponent<Transform>().position;
        cameraDiagnosisPoint = GameObject.Find("Camera Diagnosis Point").GetComponent<Transform>().position;
        sceneCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GrabSwimmer();
        }
    }

    private void GrabSwimmer()
    {
        if (tracker.GetGazeTarget() != null) // To prevent errors
        {
            grabbedSwimmer = tracker.GetGazeTarget();
            if (grabbedSwimmer.transform.parent.tag.Equals("Swimmer")) // Check if the player is looking at a swimmer
            {
                if (grabbedSwimmer.GetComponent<EyeTrackingTarget>().GazeTimer() >= 0.6f) // If the player has been looking at the swimmer sufficiently long
                {
                    grabbedSwimmer.transform.parent.GetComponent<Swimmer>().ToggleAgent(); // Turn off swimmer's navmesh agent
                    grabbedSwimmer.transform.parent.GetComponent<Transform>().SetPositionAndRotation(diagnosisPoint, Quaternion.identity); // Teleport swimmer to diagnosis point
                    sceneCamera.transform.SetPositionAndRotation(cameraDiagnosisPoint, Quaternion.identity); // Teleport camera to diagnosis point
                    uIManager.diagnosisOptions.SetActive(true); // Open diagnosis menu in UImanager
                    Debug.Log("Gotcha");
                }
                else
                {
                    Debug.Log("You must stare at the swimmer longer!");
                }
            }
        }
    }

    public void DiagnoseSwimmer(string condition)
    {
        if (grabbedSwimmer.transform.parent.GetComponent<Swimmer>().GetCondition() == condition)
        {
            Debug.Log("Correct.");
        }
        else
        {
            Debug.Log("Wrong.");
        }
    }
}