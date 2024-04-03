using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateC23 : MonoBehaviour
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
            Car.transform.Rotate(new Vector3(0, 0, speed) * Time.deltaTime);
            yield return null;
        }
    }
}
