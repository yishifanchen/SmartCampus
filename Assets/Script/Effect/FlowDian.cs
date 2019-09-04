using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlowDian : MonoBehaviour
{
    private Vector3 startPos;
    private Transform[] endPoint;
    

    float distance = 0;
    float timer = 0;
    float speed = 10;

    int index = 0;
    int allCount = 0;

    public void Move(Transform trans, Transform[] endPoint)
    {
        this.endPoint = new Transform[endPoint.Length];
        this.endPoint = endPoint;
        allCount = endPoint.Length;
        startPos = trans.position;
        transform.SetParent(trans);
        distance = Vector3.Distance(startPos, endPoint[index].position);
        timer = distance / speed;
        iTween.MoveTo(gameObject, iTween.Hash("position", endPoint[index].position,
                                          "time", timer,
                                          "onComplete", "Complete",
                                          "easetype", "linear"));
    }
    void Complete()
    {
        if (index < allCount-1)
        {
            index++;
            distance = Vector3.Distance(endPoint[index-1].position, endPoint[index].position);
            timer = distance / speed;
            iTween.MoveTo(gameObject, iTween.Hash("position", endPoint[index].position,
                                              "time", timer,
                                              "onComplete", "Complete",
                                              "easetype", "linear"));
        }
        else
        {
            transform.GetComponent<TrailRenderer>().enabled = false;
            StartCoroutine(Delay());
        }
    }
    
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = startPos;
        index = 0;
        distance = Vector3.Distance(startPos, endPoint[index].position);
        timer = distance / speed;
        iTween.MoveTo(gameObject, iTween.Hash("position", endPoint[index].position,
                                          "time", timer,
                                          "onComplete", "Complete",
                                          "easetype", "linear"));
        yield return new WaitForSeconds(0.2f);
        transform.GetComponent<TrailRenderer>().enabled = true;
    }
}
