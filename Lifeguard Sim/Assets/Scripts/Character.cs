using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

[RequireComponent(typeof(GazeAware))] // Registers GazeAware component as dependency and automatically adds it when the script is attached to something
public abstract class Character : MonoBehaviour // Base class for in-game NPCs
{
    
    GazeAware aware; // GazeAware component

    public abstract void Move();

    public abstract void SetFlag();

}
