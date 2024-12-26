using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    public List<PatrolPoint> connectedPoints;
    private void Start()
    {
        //StartCoroutine(testMove());
    }

    IEnumerator testMove()
    {
        Vector3 Target = Vector3.one;
        var x =0;
        float timer=0;
        while (true)
        {
            Target = connectedPoints[x].transform.position;
            if (connectedPoints.Count == x)
                x = 0;
            while (Vector3.Distance(Target, transform.position) > 0.1f) 
            { 
                timer += Time.deltaTime/2;
                transform.position = Vector3.Lerp(transform.position,Target,timer); 
                yield return null; 
            }
            timer = 0;
            
            x++;
            yield return null;
        }
    }
}
