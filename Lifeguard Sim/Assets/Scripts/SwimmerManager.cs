using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwimmerManager : MonoBehaviour
{
    public List<GameObject> swimmers; // List of swimmers in scene
    public List<Transform> swimPoints; // Points where swimmers will navigate to via NavMesh

    public int normalSwimmers; // Number of normal swimmers
    public int weakSwimmers; // Number of weak swimmers
    public int exhaustedSwimmers; // Number of exhausted swimmers
    public int injuredSwimmers; // Number of swimmers with broken bones


    // Start is called before the first frame update
    private void Start()
    {
        InitSwimPoints();
        InitSwimmers();
        ClassifySwimmers();
    }

    private void InitSwimPoints() // Adds pre-defined swim points tagged as such to list
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("SwimPoint"); // Find tagged swim points in the scene

        for (int i = 0; i < temp.Length; i++)
        {
            swimPoints.Add(temp[i].transform); // Add transforms of swim points to swim points list
        }
    }

    private void InitSwimmers() // Adds all objects tagged as swimmers to list
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Swimmer");

        for (int i = 0; i < temp.Length; i++)
        {
            swimmers.Add(temp[i]);
        }
    }

    // Classifying swimmers as normal, weak, exhausted, or injured
    private void ClassifySwimmers()
    {
        for (int i = 0; i < swimmers.Count; i++)
        {
            // GetCondition returns the status enum as a string
            if (swimmers[i].GetComponent<Swimmer>().GetCondition().Equals("Normal"))
            {
                normalSwimmers++;
            }
            else if (swimmers[i].GetComponent<Swimmer>().GetCondition().Equals("WeakSwimmer"))
            {
                weakSwimmers++;
            }
            else if (swimmers[i].GetComponent<Swimmer>().GetCondition().Equals("Exhausted"))
            {
                exhaustedSwimmers++;
            }
            else if (swimmers[i].GetComponent<Swimmer>().GetCondition().Equals("BrokenBone"))
            {
                injuredSwimmers++;
            }
        }
    }

    private void MoveSwimmers() // Gives each swimmer in the list a new position to move to
    {
        for (int i = 0; i < swimmers.Count; i++)
        {
            if (PathComplete(swimmers[i].GetComponent<NavMeshAgent>()))
            {
                swimmers[i].GetComponent<Swimmer>().Move(swimPoints[Random.Range(0, swimPoints.Count)].transform.position);
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        MoveSwimmers(); // TODO: Limiting conditions (this should not be called every frame)
    }

    // Function for testing if navmesh agent has reached destination
    private bool PathComplete(NavMeshAgent swimmer)
    {
        if (swimmer.enabled) // Making sure the component is enabled
        {
            if (!swimmer.pathPending) // If agent doesn't have a path pending
            {
                if (swimmer.remainingDistance <= swimmer.stoppingDistance) // If the remaining path distance is less than or equal to the agent's stopping distance
                {
                    if (!swimmer.hasPath || swimmer.velocity.sqrMagnitude == 0) // If the agent no longer has a path or has come to a complete stop
                    {
                        return true;
                    }
                }
            }
            return false; // If any of the conditions are not true, path is not complete
        }

        return false; // Path can't be complete if the component isn't enabled
    }
}