using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHitDetection : MonoBehaviour
{
    // This method is called when a collision occurs
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is a child of this object
        print("Collision with child object detected: " + collision.gameObject.name);
        if (collision.transform.IsChildOf(transform))
        {
            // Handle the collision with the child object here
            print("Collision with child object detected: " + collision.gameObject.name);
        }
    }
}
