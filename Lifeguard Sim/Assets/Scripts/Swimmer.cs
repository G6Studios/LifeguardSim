using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swimmer : Character // The main NPC that the player will have to save
{
    private enum Condition
    {
        Normal,
        WeakSwimmer,
        Exhausted,
        BrokenBone
    }

    Condition swimmerCondition;

    public override void Move()
    {

    }

    public override void SetFlag()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        InitSwimmerCond(); // Gives swimmer a random condition upon being created
    }

    private void InitSwimmerCond()
    {
        Random.InitState(System.Environment.TickCount); // Used for RNG seed as Random.seed is deprecated
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

        Debug.Log(swimmerCondition);
        Debug.Log(randCondition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
