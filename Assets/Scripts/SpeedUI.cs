using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedUI : MonoBehaviour
{

    public TextMeshProUGUI speedText;
    public ShipDrive shipspeed;
    private float totalvelocity;
    private float xVelocity;
    private float yVelocity;
    private float zVelocity;

    // Update is called once per frame
    void Update()
    {
        xVelocity = shipspeed.GetComponent<Rigidbody>().velocity.x;
        yVelocity = shipspeed.GetComponent<Rigidbody>().velocity.y;
        zVelocity = shipspeed.GetComponent<Rigidbody>().velocity.z;
        totalvelocity = (float)Math.Sqrt(Math.Pow(xVelocity, 2) + Math.Pow(yVelocity, 2) + Math.Pow(zVelocity, 2));
        totalvelocity = (float)Math.Round(totalvelocity, 3);
        speedText.text = "Speed: " + totalvelocity.ToString();
    }
}
