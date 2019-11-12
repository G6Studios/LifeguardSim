using System.Collections;
using System.Collections.Generic;
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

    private Condition swimmerCondition;
    NavMeshAgent agent;
    public bool agentActive = true; // Boolean for whether agent should move around


    public override void Move(Vector3 dest) // Setting the swimmer to move via navmesh agent
    {

        agent.SetDestination(dest);

    }

    public override void SetFlag()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        InitComponents(); // Assigns components
        InitSwimmerCond(); // Gives swimmer a random condition upon being created
    }

    private void InitSwimmerCond()
    {
        // Random.initstate was removed due to not playing nicely with instantiation of gameobjects
        int randCondition = Random.Range(1, 101); // Random number between 1 and 100

        if (randCondition >= 1 && randCondition <= 49) // 50% chance to be normal
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

        //Debug.Log(swimmerCondition);
        //Debug.Log(randCondition);
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

    void InitComponents()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.avoidancePriority = Random.Range(0, 100); // Having this randomized helps prevent agents getting stuck on each other
    }

    // Update is called once per frame
    void Update()
    {

    }
}
