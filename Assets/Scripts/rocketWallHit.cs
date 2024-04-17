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
                HealthModifier.HP -= 20;
            }
        }
        if (other.gameObject.name.Contains("Wall"))
        {
            print("LMAOOOO");
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        //grounded = true;
        print("HIT WALL LOLOLOLOL!!");
        if (col.gameObject.name.Contains("Wall"))
        {
            print("WALLHIT!!!");
            //rb.AddRelativeForce(new Vector3(0, 10, -100));
            //transform.forward = new Vector3(-transform.forward.x, transform.forward.y, transform.forward.z);
            //Vector3 reflectionDirection = Vector3.Reflect(rb.velocity, col.contacts[0].normal);
            //print("Old Vel = " + rb.velocity);
            //print("New Vel = " + reflectionDirection);
            //print("Normal of Col = " + col.contacts[0].normal);
            //Vector3 reflectionDirection = Vector3.Reflect(rb.velocity, col.GetContact(0).normal);
            //Vector3 reflectionDirection = Vector3.Reflect(rb.velocity, );

            //rb.velocity = reflectionDirection;
            //Sound Effects for hitting walls
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                FindObjectOfType<SoundEffectPlayer>().Play("wallhitOne");
            }
            if (rand == 1)
            {
                FindObjectOfType<SoundEffectPlayer>().Play("wallhitTwo");
            }
            if (rand == 2)
            {
                FindObjectOfType<SoundEffectPlayer>().Play("wallhitThree");
            }
            //Destroy(col.gameObject);

            //rb.AddForce(Vector3.Reflect(rb.velocity.normalized, col.contacts[0].normal));

        }
        //if (col.gameObject.name.Contains("Cylinder"))
        //{
            //print("CYLINDERHIT!!");
        //}
        //if (col.gameObject.name.Contains("Wall"))
        //{
        //    print("HIT WALL LOLOLOLOL!!");
        //}
    }
}
