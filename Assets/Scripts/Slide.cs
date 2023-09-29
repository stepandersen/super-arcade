using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    public float Left;
    public float Right;
    public float Speed = 2f;
   
    IEnumerator Start()
    {
        
        Vector3 pointA = new Vector3(Left, transform.position.y, 0);
        Vector3 pointB = new Vector3(Right, transform.position.y, 0);

        var fullDistance = Right - Left;
        var startDistance = transform.position.x - pointA.x;
        var adjustedSpeed = fullDistance / Speed;

        yield return StartCoroutine(MoveObject(transform, transform.position, pointA, (startDistance/fullDistance) * adjustedSpeed));

        while(true)
        {
            yield return StartCoroutine(MoveObject(transform, pointA, pointB, adjustedSpeed));
            yield return StartCoroutine(MoveObject(transform, pointB, pointA, adjustedSpeed));
        }
    }
   
    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        var i= 0.0f;
        var rate= 1.0f/time;
        while(i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }
    
}