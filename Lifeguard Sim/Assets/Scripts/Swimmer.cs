using UnityEngine;
using UnityEngine.AI;

public class Swimmer : Character // The main NPC that the player will have to save
{
    public enum Condition
    {
        Normal,
        WeakSwimmer,
        Exhausted,
        BrokenBone
    }

    // 3 different swimmer materials to randomize between
    [SerializeField]
    private Material patron1;

    [SerializeField]
    private Material patron2;

    [SerializeField]
    private Material patron3;

    private Condition swimmerCondition;
    private NavMeshAgent agent; // Navmesh agent component
    private Renderer c_renderer; // Used for changing material in child mesh
    private Animator animator; // Animation controller
    public bool agentActive = true; // Boolean for whether agent should move around
    private float swimmerSpeed; // How fast the swimmer moves around
    private float bobFrequency; // How fast the swimmer bobs up and down while swimming
    private float bobMagnitude; // How high the swimmer bobs up and down while moving
    public bool isGrabbed = false; // Has the swimmer been taken out onto the dock?

    public override void Move(Vector3 dest) // Setting the swimmer to move via navmesh agent
    {
        agent.SetDestination(dest);
    }

    public override void SetFlag()
    {
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Assigning components
        c_renderer = gameObject.GetComponentInChildren<Renderer>();
        animator = gameObject.GetComponent<Animator>();

        InitComponents(); // Assigns components
        InitSwimmerCond(); // Gives swimmer a random condition upon being created
        InitSwimmerMovement(); // Assigns movement properties to swimmer based on their condition
        InitMaterial(); // Assigns random material to swimmer
    }

    private void InitSwimmerCond()
    {
        // Random.initstate was removed due to not playing nicely with instantiation of gameobjects
        int randCondition = Random.Range(1, 101); // Random number between 1 and 100

        if (randCondition >= 0 && randCondition <= 49) // 50% chance to be normal
        {
            swimmerCondition = Condition.Normal; // Set swimmer condition to normal
        }
        else if (randCondition >= 50 && randCondition <= 74) // 25% chance to be weak swimmer
        {
            swimmerCondition = Condition.WeakSwimmer; // Set swimmer condition to weak swimmer
        }
        else if (randCondition >= 75 && randCondition <= 89) // 15% chance to be exhausted
        {
            swimmerCondition = Condition.Exhausted; // Set swimmer condition to exhausted
        }
        else if (randCondition >= 90 && randCondition <= 100) // 10% chance to have broken bone
        {
            swimmerCondition = Condition.BrokenBone; // Set swimmer condition to broken bone
        }
        else
        {
            Debug.LogError("Error generating random condition."); // Failsafe
        }
    }

    private void InitSwimmerMovement()
    {
        if (swimmerCondition == Condition.Normal) // Movement and animation for normal swimmer
        {
            swimmerSpeed = 5f; // Baseline swimming speed
            bobFrequency = 5f; // Baseline bobbing speed
            bobMagnitude = 0.3f; // Baseline bobbing height
        }
        else if (swimmerCondition == Condition.WeakSwimmer) // Movement and animation for weak swimmer
        {
            swimmerSpeed = 4f; // Weak swimmers slightly slower than baseline
            bobFrequency = 4f; // Weak swimmers bob slightly slower than baseline
            bobMagnitude = 0.4f; // Weak swimmers bob higher and lower trying to stay afloat
            animator.SetBool("isStruggling", true); // Play struggling animation
        }
        else if (swimmerCondition == Condition.Exhausted) // Movement and animation for exhausted swimmer
        {
            swimmerSpeed = 3f; // Exhausted swimmers swim noticeably slower than baseline
            bobFrequency = 2f; // Exhausted swimmers bob much slower than baseline
            bobMagnitude = 0.2f; // Exhausted swimmers have a hard time staying afloat, let alone bobbing
            animator.SetBool("isStruggling", true); // Play struggling animation
        }
        else if (swimmerCondition == Condition.BrokenBone) // Movement and animation for injured swimmer
        {
            swimmerSpeed = 2f; // Injured swimmers swim much slower than baseline
            bobFrequency = 3f; // Injured swimmers bob slightly slower than baseline
            bobMagnitude = 0.2f; // Injured swimmers bob the same height as exhausted swimmers, as they are in pain
            animator.SetBool("isInjured", true); // Play injured animation
        }
    }

    public void InitMaterial()
    {
        int randMaterial = Random.Range(0, 101); // Random number between 1 and 100

        // 33% chance of each skin being picked
        if (randMaterial >= 0 && randMaterial <= 33)
        {
            c_renderer.material = patron1; // Patron 1 skin
        }
        else if (randMaterial >= 34 && randMaterial <= 66)
        {
            c_renderer.material = patron2; // Patron 2 skin
        }
        else if (randMaterial >= 67 && randMaterial <= 100)
        {
            c_renderer.material = patron3; // Patron 3 skin
        }
        else
        {
            Debug.LogError("Error choosing random swimmer material."); // Failsafe
        }
    }

    public bool ToggleAgent() // Toggle on/off state of navmesh agent
    {
        if (agent.enabled) // If agent is enabled, set to disabled
        {
            agent.enabled = false;
            return false;
        }
        else // Else set agent to enabled
        {
            agent.enabled = true;
            return true;
        }
    }

    public string GetCondition()
    {
        return swimmerCondition.ToString();
    }

    private void InitComponents()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.avoidancePriority = Random.Range(0, 100); // Having this randomized helps prevent agents getting stuck on each other
    }

    // Update is called once per frame
    private void Update()
    {
        IsGrabbed(); // Testing for if the swimmer has been grabbed
    }

    // Function in update to handle if swimmer has been grabbed by the player
    private void IsGrabbed()
    {
        if(isGrabbed) // If the swimmer has been grabbed by the player
        {
            // Cleaning animation flags
            animator.SetBool("isStruggling", false); 
            animator.SetBool("isInjured", false); 
            animator.SetBool("onDeck", true); 
        }

        else
        {
            // Swimmer should not be bobbing up and down while on the deck
            Vector3 newPosition = transform.position;
            newPosition += transform.up * Mathf.Sin(Time.time * bobFrequency) * bobMagnitude;
            transform.position = newPosition;
        }
    }
}