using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camFollowTwo : MonoBehaviour
{
    public GameObject ship;
    public float shipX;
    public float shipY;
    public float shipZ; 

    // Update is called once per frame
    void Update()
    {
        shipX = ship.transform.eulerAngles.x;
        shipY = ship.transform.eulerAngles.y;
        shipZ = ship.transform.eulerAngles.z;

        transform.eulerAngles = new Vector3(shipX - shipX, shipY, shipZ - shipZ);
    }
}
