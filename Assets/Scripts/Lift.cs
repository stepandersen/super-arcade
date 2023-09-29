using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public float Bottom = 2f;
    public float Top = 7f;
    public float Speed = 3f;
   
    IEnumerator Start()
    {
        
        Vector3 pointA = new Vector3(transform.position.x, Top, 0);
        Vector3 pointB = new Vector3(transform.position.x, Bottom, 0);

        var fullDistance = Top - Bottom;
        var startDistance = pointA.y - transform.position.y;

        yield return StartCoroutine(MoveObject(transform, transform.position, pointA, (startDistance/fullDistance) * Speed));

        while(true)
        {
            yield return StartCoroutine(MoveObject(transform, pointA, pointB, Speed));
            yield return StartCoroutine(MoveObject(transform, pointB, pointA, Speed));
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