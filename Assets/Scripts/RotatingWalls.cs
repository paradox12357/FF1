using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingWalls : MonoBehaviour
{

    public Transform wall;
    public float speed = 20f;

    private void Start()
    {
        StartCoroutine(RotateWalls());
    }

    IEnumerator RotateWalls()
    {
        while (true)
        {
            wall.transform.Rotate(new Vector3(0, speed, 0) * Time.deltaTime);
            yield return null;
        }
    }
}
