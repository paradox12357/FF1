using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCarSelect : MonoBehaviour
{
    public int car = 0;

    public GameObject Car1;
    public GameObject Car2;
    public GameObject Car3;
    public Transform offScreen;
    public Transform onScreen;


    private void Update()
    {
        if (Input.GetKeyDown("a") || Input.GetKeyDown("s"))
        {
            if (car == 2)
            {
                car--; // car now 1
                Car3.transform.position = offScreen.transform.position;
                Car2.transform.position = onScreen.transform.position;
            }
            else if (car == 1)
            {
                car--; // car now 0
                Car2.transform.position = offScreen.transform.position;
                Car1.transform.position = onScreen.transform.position;
            }
        }

        if (Input.GetKeyDown("w") || Input.GetKeyDown("d"))
        {
            if (car == 0)
            {
                car++; // car now 1
                Car1.transform.position = offScreen.transform.position;
                Car2.transform.position = onScreen.transform.position;
            }
            else if (car == 1)
            {
                car++; // car now 2
                Car2.transform.position = offScreen.transform.position;
                Car3.transform.position = onScreen.transform.position;
            }
        }
    }

    /*private void Start()
    {
        StartCoroutine(CarSwap());
    }
    IEnumerator CarSwap()
    {
        while (true)
        {
            if (Input.GetKeyDown("a") || Input.GetKeyDown("s"))
            {
                if (car == 2)
                {
                    car--; // car now 1
                    Car3.transform.position = offScreen.transform.position;
                    Car2.transform.position = onScreen.transform.position;
                }
                else if(car == 1)
                {
                    car--; // car now 0
                    Car2.transform.position = offScreen.transform.position;
                    Car1.transform.position = onScreen.transform.position;
                }

                yield return null;
            }

            if(Input.GetKeyDown("w") || Input.GetKeyDown("d"))
            {
                if (car == 0)
                {
                    car++; // car now 1
                    Car1.transform.position = offScreen.transform.position;
                    Car2.transform.position = onScreen.transform.position;
                }
                else if (car == 1)
                {
                    car++; // car now 2
                    Car2.transform.position = offScreen.transform.position;
                    Car3.transform.position = onScreen.transform.position;
                }

                yield return null;
            }
        }
    }*/
}
