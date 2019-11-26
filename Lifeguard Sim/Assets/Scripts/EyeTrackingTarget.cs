using Tobii.Gaming; // Tobii library
using UnityEngine;
using TMPro;

public class EyeTrackingTarget : MonoBehaviour
{
    /* NOTE TO SELF: GazeAware objects need to have a mesh in order to be picked up by eye tracking! To achieve bigger hitboxes for eye tracking:
     * Use invisible material on mesh that represents hitbox, attach bigger hitbox as child to parent object
     * ALSO DON'T FORGET TO SET COLLISION LAYER TO EyeTracking!!!
     */
    [SerializeField]
    private Swimmer swimmer;
    private GazeAware aware; // Component for checking if object has gaze focus
    public Renderer objectMaterial; // Testing component
    private float gazeThreshold = 1.5f;
    private float gazeTimer;
    private float shiftPercentage;
    private TextMeshPro symptomText;
    public bool colorFader;

    // Start is called before the first frame update
    private void Start()
    {
        // Getting components from gameobject
        aware = GetComponent<GazeAware>();
        //objectMaterial = GetComponentInParent<Renderer>().material;
    }

    // Update is called once per frame
    private void Update()
    {
        ProcessTimer();
    }

    public float GazeTimer()
    {
        return gazeTimer / gazeThreshold; // Returns the percentage
    }

    private void ProcessTimer()
    {
        if (aware.HasGazeFocus) // If the player's eye focus is on this object
        {
            gazeTimer += Time.deltaTime / gazeThreshold; // Increment timer while user is looking at GazeAware target
        }
        else // If their eye focus is elsewhere
        {
            gazeTimer -= Time.deltaTime / gazeThreshold; // Decrement timer otherwise
        }

        if(colorFader)
        {
            gazeTimer = Mathf.Clamp(gazeTimer, 0.0f, gazeThreshold); // Make sure value does not go out of bounds

            objectMaterial.material.SetColor("_BaseColor", Color.Lerp(Color.white, Color.red, gazeTimer)); // Object material becomes more red as player looks at it
        }

        else
        {
            symptomText = GetComponentInChildren<TextMeshPro>();
            if (swimmer.GetCondition().Equals("Normal"))
            {
                symptomText.text = "Nothing seems to be wrong with him...";
            }
            else if (swimmer.GetCondition().Equals("WeakSwimmer"))
            {
                symptomText.text = "He doesn't look like a very strong swimmer";
            }
            else if (swimmer.GetCondition().Equals("Exhausted"))
            {
                symptomText.text = "He is out of breath and looks very tired";
            }
            else if (swimmer.GetCondition().Equals("BrokenBone"))
            {
                symptomText.text = "Looks like his leg is broken";
            }

            gazeTimer = Mathf.Clamp(gazeTimer, 0.0f, gazeThreshold); // Make sure value does not go out of bounds

            symptomText.color = Color.Lerp(new Color(1f, 1f, 1f, 0f), new Color(1f, 1f, 1f, 1f), gazeTimer);
        }
        
    }
}