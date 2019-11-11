using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;


public abstract class Character : MonoBehaviour // Base class for in-game NPCs
{
    
    GazeAware aware; // GazeAware component

    public abstract void Move(Vector3 dest);

    public abstract void SetFlag();

}
