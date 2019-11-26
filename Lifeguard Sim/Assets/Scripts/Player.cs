using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    playerstats statistics;

    private SwimmerManager s_manager;
    private UIManager uIManager;
    private EyeTracking tracker;

    private Vector3 diagnosisPoint;
    private Vector3 cameraDiagnosisPoint;
    private Vector3 cameraStartPoint;
    private Vector3 hell; // Where discarded swimmers go
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
        cameraStartPoint = GameObject.Find("Camera Start Point").GetComponent<Transform>().position;
        hell = GameObject.Find("Hell").GetComponent<Transform>().position;
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
                    grabbedSwimmer.transform.parent.GetComponent<Swimmer>().isGrabbed = true;
                    grabbedSwimmer.transform.parent.GetComponent<Swimmer>().ToggleAgent(); // Turn off swimmer's navmesh agent
                    grabbedSwimmer.transform.parent.GetComponent<Transform>().SetPositionAndRotation(diagnosisPoint, Quaternion.identity); // Teleport swimmer to diagnosis point
                    sceneCamera.transform.SetPositionAndRotation(cameraDiagnosisPoint, Quaternion.identity); // Teleport camera to diagnosis point
                    sceneCamera.transform.Rotate(90, 0, 0);
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
            statistics.playerCorrectAnswer++;
            grabbedSwimmer.transform.parent.GetComponent<Transform>().SetPositionAndRotation(hell, Quaternion.identity); // Teleport swimmer to hell
            Debug.Log("Correct.");
        }
        else
        {
            statistics.playerWrongAnswer++;
            grabbedSwimmer.transform.parent.GetComponent<Transform>().SetPositionAndRotation(s_manager.swimPoints[0].position, Quaternion.identity); // Teleport swimmer back into pool
            Debug.Log("Wrong.");
        }

        uIManager.diagnosisOptions.SetActive(false); // Open diagnosis menu in UImanager
        sceneCamera.transform.SetPositionAndRotation(cameraStartPoint, Quaternion.identity); // Teleport camera back to watchtower point
        sceneCamera.transform.Rotate(33, 0, 0); // Rotate camera back to initial position
        grabbedSwimmer.transform.parent.GetComponent<Swimmer>().isGrabbed = false; // Ungrab swimmer
    }
}