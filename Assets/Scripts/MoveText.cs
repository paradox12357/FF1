using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveText : MonoBehaviour
{
    public Transform text;
    public Transform startPnt;
    public Transform endPnt;
    public float speed = 5.0f;

    public float secs = 28.0f;

    private void Start()
    {
        StartCoroutine(MoveObj());
    }

    IEnumerator MoveObj()
    {
        yield return new WaitForSeconds(secs);

        while (text.position.x > endPnt.position.x)
        {
            text.transform.Translate(Vector3.left * speed * Time.deltaTime);
            yield return null;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(text.transform.position, startPnt.position);
        Gizmos.DrawLine(text.transform.position, endPnt.position);
    }
}
