using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCars : MonoBehaviour
{
    public Transform Car;
    public float speed = 20f;

    private void Start()
    {
        StartCoroutine(RotateCar());
    }

    IEnumerator RotateCar()
    {
        while (true)
        {
            Car.transform.Rotate(new Vector3(speed, 0, 0) * Time.deltaTime);
            yield return null;
        }
    }
}
