using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMoveBasic : MonoBehaviour
{
    public Vector3 direction = new Vector3(0, 0, 0);
    public float speed = 1;
    public int timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(0, 100);
        direction = new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer > 0)
        {
            timer--;
        }
        else
        {
            timer = Random.Range(0, 100);
            direction = new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2));
        }

        //add a force to the enemy
        GetComponent<Rigidbody>().AddForce(direction * speed);
    }
}