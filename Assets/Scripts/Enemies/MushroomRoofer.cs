using System;
using Entity;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MushroomRoofer : Mushroom
{
    public Rigidbody2D Roof;
    
    public void FixedUpdate()
    {
        // Set the child game object's position and rotation to match the parent's
        Roof.position = transform.position;
    }
}