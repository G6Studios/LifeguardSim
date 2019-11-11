using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwimmerManager : MonoBehaviour
{
    public List<GameObject> swimmers; // List of swimmers in scene
    public List<Transform> swimPoints; // Points where swimmers will navigate to via NavMesh


    // Start is called before the first frame update
    void Start()
    {
        InitSwimPoints();
        InitSwimmers();

    }

    void InitSwimPoints() // Adds pre-defined swim points tagged as such to list
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("SwimPoint"); // Find tagged swim points in the scene

        for (int i = 0; i < temp.Length; i++)
        {
            swimPoints.Add(temp[i].transform); // Add transforms of swim points to swim points list
        }
    }

    void InitSwimmers() // Adds all objects tagged as swimmers to list
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Swimmer");

        for (int i = 0; i < temp.Length; i++)
        {
            swimmers.Add(temp[i]);
        }
    }

    void MoveSwimmers() // Gives each swimmer in the list a new position to move to
    {
        for (int i = 0; i < swimmers.Count; i++)
        {
            if (!swimmers[i].GetComponent<NavMeshAgent>().hasPath)
            {
                swimmers[i].GetComponent<Swimmer>().Move(swimPoints[Random.Range(0, 4)].transform.position);

            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveSwimmers(); // TODO: Limiting conditions (this should not be called every frame)
    }
}
