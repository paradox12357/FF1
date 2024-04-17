using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class rocketWallHit : MonoBehaviour
{

    GameObject shotOwner;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(other.gameObject == shotOwner)
            {
                //LMAO!!!
                print("Self hit! (no dmg dealt)");
            }
            else
            {
                print("PLAYER HIT");
                ShipDrive HealthModifier = other.gameObject.GetComponent<ShipDrive>();
                if (HealthModifier != null)
                {
                    HealthModifier.HP -= 50;
                }
                CheckpointCounter CPC = shotOwner.GetComponent<CheckpointCounter>();
                CPC.scoreCount += 500;
                CPC.updateCheckpoint();
                Destroy(gameObject);
            }
            
        }
        if (other.gameObject.name.Contains("Wall"))
        {
            //print("LMAOOOO");
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "enemy")
        {
            print("Enemy Hit");
            CheckpointCounter CPC = shotOwner.GetComponent<CheckpointCounter>();
            CPC.scoreCount += 200;
            CPC.updateCheckpoint();
            Destroy(other.gameObject);
        }
    }
    public void setShotOwner(GameObject shotOwnerPassedIn)
    {
        shotOwner = shotOwnerPassedIn;
    }
}
