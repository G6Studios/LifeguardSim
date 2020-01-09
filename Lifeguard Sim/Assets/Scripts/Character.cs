using Tobii.Gaming;
using UnityEngine;

public abstract class Character : MonoBehaviour // Base class for in-game NPCs
{
    private GazeAware aware; // GazeAware component

    public abstract void Move(Vector3 dest);

    public abstract void SetFlag();
}