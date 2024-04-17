using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketWallHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("PLAYER HIT");
            ShipDrive HealthModifier = other.gameObject.GetComponent<ShipDrive>();
            if (HealthModifier != null )
            {
                HealthModifier.HP -= 50;
            }
            Destroy(gameObject);
        }
        if (other.gameObject.name.Contains("Wall"))
        {
            //print("LMAOOOO");
            Destroy(gameObject);
        }
    }
}
