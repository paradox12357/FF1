using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class rocketWallHit : MonoBehaviour
{
    

    GameObject shotOwner;
    //public ParticleSystem explosion;
    public GameObject explosion;

    private void Start()
    {
        //explosion = GetComponent<ParticleSystem>();
        explosion.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        //print("Wall's Coords: x: " + other.transform.position.x + " Rocket's Coords: x: " + transform.position.x);
        if (gameObject.tag == "rocketHoming")
        {
            
            print("ROCKET COLLISION!!!");
            if (other.gameObject.tag == "Player")
            {
                if (other.gameObject == shotOwner)
                {
                    //LMAO!!!
                    print("Self hit! (no dmg dealt)");
                }
                else
                {
                    /*print("PLAYER HIT");
                    ShipDrive HealthModifier = other.gameObject.GetComponent<ShipDrive>();
                    if (HealthModifier != null)
                    {
                        HealthModifier.HP -= 50;
                    }
                    CheckpointCounter CPC = shotOwner.GetComponent<CheckpointCounter>();
                    CPC.scoreCount += 500;
                    CPC.updateCheckpoint();
                    FindObjectOfType<SoundEffectPlayer>().Play("rocketExplode");
                    Destroy(gameObject);*/
                    transform.position = Vector3.MoveTowards(transform.position, other.transform.position, 1f);
                    transform.up = other.transform.position - transform.position;
                }

            }
        }
        if (gameObject.tag == "rocket")
        {
            
            //explosion.Play();
            print("ROCKET COLLISION!!!");
            if (other.gameObject.tag == "enemy")
            {
               
                transform.position = Vector3.MoveTowards(transform.position, other.transform.position, 1f);
                transform.up = other.transform.position - transform.position;
            

            }
        }
        if (Mathf.Abs(other.transform.position.x - transform.position.x) < 5 && Mathf.Abs(other.transform.position.y - transform.position.y) < 5 &&
            Mathf.Abs(other.transform.position.z - transform.position.z) < 5)
        {
            print("NEAR!!!!");
            if (other.gameObject.tag == "Player")
            {
                if (other.gameObject == shotOwner)
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
                    FindObjectOfType<SoundEffectPlayer>().Play("rocketExplode");
                    explosion.SetActive(true);
                    //StartCoroutine(ExampleCoroutine());
                    Invoke("die", 0.75f);
                    //gameObject.SetActive(false);
                    //Destroy(gameObject);
                }


            }
            if (other.gameObject.name.Contains("Wall"))
            {
                //print("LMAOOOO");
                FindObjectOfType<SoundEffectPlayer>().Play("rocketExplode");
                explosion.SetActive(true);
                Invoke("die", 0.75f);
                //gameObject.SetActive(false);
                //Destroy(gameObject);
            }
            if (other.gameObject.tag == "enemy")
            {
                print("Enemy Hit");
                CheckpointCounter CPC = shotOwner.GetComponent<CheckpointCounter>();
                CPC.scoreCount += 200;
                CPC.updateCheckpoint();
                FindObjectOfType<SoundEffectPlayer>().Play("rocketExplode");
                //explosion.Play();
                Destroy(other.gameObject);
                explosion.SetActive(true);
                //StartCoroutine(ExampleCoroutine());
                Invoke("die", 0.75f);
                //gameObject.
                //gameObject.SetActive(false);
                //Destroy(gameObject);
            }
        }
        
    }

    void OnTriggerStay(Collider other)
    {
        if (gameObject.tag == "rocketHoming")
        {
            print("ROCKET COLLISION!!!");
            if (other.gameObject.tag == "Player")
            {
                if (other.gameObject == shotOwner)
                {
                    //LMAO!!!
                    print("Self hit! (no dmg dealt)");
                }
                else
                {
                    /*print("PLAYER HIT");
                    ShipDrive HealthModifier = other.gameObject.GetComponent<ShipDrive>();
                    if (HealthModifier != null)
                    {
                        HealthModifier.HP -= 50;
                    }
                    CheckpointCounter CPC = shotOwner.GetComponent<CheckpointCounter>();
                    CPC.scoreCount += 500;
                    CPC.updateCheckpoint();
                    FindObjectOfType<SoundEffectPlayer>().Play("rocketExplode");
                    Destroy(gameObject);*/
                    transform.position = Vector3.MoveTowards(transform.position, other.transform.position, 10f);
                    transform.up = other.transform.position - transform.position;
                }

            }
        }
        if (gameObject.tag == "rocket")
        {
            print("ROCKET COLLISION!!!");
            if (other.gameObject.tag == "enemy")
            {
               
                transform.position = Vector3.MoveTowards(transform.position, other.transform.position, 1f);
                transform.up = other.transform.position - transform.position;
            

            }
        }
    }
    public void setShotOwner(GameObject shotOwnerPassedIn)
    {
        shotOwner = shotOwnerPassedIn;
    }

    public void die()
    {
        Destroy(gameObject);
    }
}
