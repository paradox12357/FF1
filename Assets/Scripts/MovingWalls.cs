using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingWalls : MonoBehaviour
{
    public Transform platform;
    public Transform startPnt;
    public Transform endPnt;
    public float speed = 20.5f;

    public bool up = true;
    public float upTime = 10.0f;
    public float downTime = 15.0f;

    public int direction = 1;


    private void Start()
    {
        StartCoroutine(MoveObj());
    }

    /*private void Update()
    {
        Move();
    }*/

    IEnumerator MoveObj()
    {
        while (true)
        {
            while (platform.position.y < startPnt.position.y)
            {
                platform.transform.Translate(Vector3.up * speed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(upTime);

            while(platform.position.y > endPnt.position.y)
            {
                platform.transform.Translate(Vector3.down * speed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(downTime);
        }
    }

   /*private void Move()
    {
        Vector3 target = currentMvmtTarget();

        platform.position = Vector3.Lerp(platform.position, target, speed * Time.deltaTime);

        float distance = (target - (Vector3)platform.position).magnitude;
        print("Distance: " + distance);

        if (distance <= 0.1f)
        {

            direction *= -1;
        }
    } 

    //grabs the target position
    Vector3 currentMvmtTarget()
    {
        if (direction == 1)
        {
            return startPnt.position;
        }
        else
        {
            return endPnt.position;
        }
        
    }*/
    
    //shows a line on unity to see how object will move
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(platform.transform.position, startPnt.position);
        Gizmos.DrawLine(platform.transform.position, endPnt.position);
    }
}
