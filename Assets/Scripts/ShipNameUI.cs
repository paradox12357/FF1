using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShipNameUI : MonoBehaviour
{
    public float speed = 20.5f;

    private int car = 0;

    public Transform Car1;
    public Transform Car2;
    public Transform Car3;
    public Transform offScreen;
    public Transform onScreen;

    private void Start()
    {
        StartCoroutine(MoveObj());
    }

    IEnumerator MoveObj()
    {
        while (true)
        {
            if (Input.GetKeyDown("a") || Input.GetKeyDown("s"))
            {
                if (car == 2)
                {
                    // car3 Name moving on > off
                    while (Car3.position.y < offScreen.position.y)
                    {
                        Car3.transform.Translate(Vector3.up * speed * Time.deltaTime);
                        yield return null;
                    }

                    car--; // car now index 1

                    // car2 name moving off > on
                    while (Car2.position.y > onScreen.position.y)
                    {
                        Car2.transform.Translate(Vector3.down * speed * Time.deltaTime);
                        yield return null;
                    }
                }
                else if (car == 1)
                {
                    // car2 Name moving on > off
                    while (Car2.position.y < offScreen.position.y)
                    {
                        Car2.transform.Translate(Vector3.up * speed * Time.deltaTime);
                        yield return null;
                    }

                    car--; // car now index 0

                    // car1 name moving off > on 
                    while (Car1.position.y > onScreen.position.y)
                    {
                        Car1.transform.Translate(Vector3.down * speed * Time.deltaTime);
                        yield return null;
                    }
                }

                yield return null;
            }

            if (Input.GetKeyDown("w") || Input.GetKeyDown("d"))
            {
                if (car == 0)
                {
                    //car1 name moving on > off
                    while (Car1.position.y < offScreen.position.y)
                    {
                        Car1.transform.Translate(Vector3.up * speed * Time.deltaTime);
                        yield return null;
                    }

                    car++; // car now index 1

                    // car2 name moving off > on
                    while (Car2.position.y > onScreen.position.y)
                    {
                        Car2.transform.Translate(Vector3.down * speed * Time.deltaTime);
                        yield return null;
                    }
                }
                else if (car == 1)
                {
                    // car2 Name moving on > off
                    while (Car2.position.y < offScreen.position.y)
                    {
                        Car2.transform.Translate(Vector3.up * speed * Time.deltaTime);
                        yield return null;
                    }

                    car++; // car now index 2

                    // car3 name moving off > on 
                    while (Car3.position.y > onScreen.position.y)
                    {
                        Car3.transform.Translate(Vector3.down * speed * Time.deltaTime);
                        yield return null;
                    }
                }

                yield return null;
            }

            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(Car1.transform.position, onScreen.position);
        Gizmos.DrawLine(Car1.transform.position, offScreen.position);
        Gizmos.DrawLine(Car2.transform.position, onScreen.position);
        Gizmos.DrawLine(Car2.transform.position, offScreen.position);
        Gizmos.DrawLine(Car3.transform.position, onScreen.position);
        Gizmos.DrawLine(Car3.transform.position, offScreen.position);
    }
}
