using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 流淌特效顺序
/// </summary>
public enum FlowSequence
{
    xyz,
    xzy,
    yxz,
    yzx,
    zxy,
    zyx,
}

public class Flow : MonoBehaviour {
    private Vector3 startPos;
    private Transform endPoint;

    Vector3 targetPos1=Vector3.zero;
    Vector3 targetPos2 = Vector3.zero;
    Vector3 targetPos3 = Vector3.zero;

    float distance = 0;
    float timer = 0;
    float speed=10;

    public void Move(Transform trans,Transform endPoint)
    {
        this.endPoint = endPoint;
        switch (GameManager.instance.flowSequence)
        {
            case FlowSequence.xyz:
                targetPos1 = new Vector3(endPoint.position.x, transform.position.y, transform.position.z);
                break;
            case FlowSequence.xzy:
                targetPos1 = new Vector3(endPoint.position.x, transform.position.y, transform.position.z);
                break;
            case FlowSequence.yxz:
                targetPos1 = new Vector3(transform.position.x, endPoint.position.y, transform.position.z);
                break;
            case FlowSequence.yzx:
                targetPos1 = new Vector3(transform.position.x, endPoint.position.y, transform.position.z);
                break;
            case FlowSequence.zxy:
                targetPos1 = new Vector3(transform.position.x, transform.position.y, endPoint.position.z);
                break;
            case FlowSequence.zyx:
                targetPos1 = new Vector3(transform.position.x, transform.position.y, endPoint.position.z);
                break;
        }
        transform.SetParent(trans);
        startPos = trans.position;
        distance = Vector3.Distance(targetPos1, transform.position);
        timer = distance / speed;
        iTween.MoveTo(gameObject, iTween.Hash("position", targetPos1, 
                                          "time", timer,
                                          "onComplete", "Complete1",
                                          "easetype", "linear"));
    }
    void Complete1()
    {
        switch (GameManager.instance.flowSequence)
        {
            case FlowSequence.xyz:
                targetPos2 = new Vector3(transform.position.x, endPoint.position.y, transform.position.z);
                break;
            case FlowSequence.xzy:
                targetPos2 = new Vector3(transform.position.x, transform.position.y, endPoint.position.z);
                break;
            case FlowSequence.yxz:
                targetPos2 = new Vector3(endPoint.position.x, transform.position.y, transform.position.z);
                break;
            case FlowSequence.yzx:
                targetPos2 = new Vector3(transform.position.x, transform.position.y, endPoint.position.z);
                break;
            case FlowSequence.zxy:
                targetPos2 = new Vector3(endPoint.position.x, transform.position.y, transform.position.z);
                break;
            case FlowSequence.zyx:
                targetPos2 = new Vector3(transform.position.x, endPoint.position.y, transform.position.z);
                break;
        }
        distance = Vector3.Distance(targetPos2, transform.position);
        timer = distance / speed;
        iTween.MoveTo(gameObject, iTween.Hash("position", targetPos2,
                                          "time", timer,
                                          "onComplete", "Complete2",
                                          "easetype", "linear"));
    }
    void Complete2()
    {
        switch (GameManager.instance.flowSequence)
        {
            case FlowSequence.xyz:
                targetPos3 = new Vector3(transform.position.x, transform.position.y, endPoint.position.z);
                break;
            case FlowSequence.xzy:
                targetPos3 = new Vector3(transform.position.x, endPoint.position.y, transform.position.z);
                break;
            case FlowSequence.yxz:
                targetPos3 = new Vector3(transform.position.x, transform.position.y, endPoint.position.z);
                break;
            case FlowSequence.yzx:
                targetPos3 = new Vector3(endPoint.position.x, transform.position.y, transform.position.z);
                break;
            case FlowSequence.zxy:
                targetPos3 = new Vector3(transform.position.x, endPoint.position.y, transform.position.z);
                break;
            case FlowSequence.zyx:
                targetPos3 = new Vector3(endPoint.position.x, transform.position.y, transform.position.z);
                break;
        }
        distance = Vector3.Distance(targetPos3, transform.position);
        timer = distance / speed;
        iTween.MoveTo(gameObject, iTween.Hash("position", targetPos3,
                                          "time", timer,
                                          "onComplete", "Complete3",
                                          "easetype", "linear"));
    }
    void Complete3()
    {
        transform.GetComponent<TrailRenderer>().enabled = false;
        StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = startPos;
        distance = Vector3.Distance(targetPos1, transform.position);
        timer = distance / speed;
        iTween.MoveTo(gameObject, iTween.Hash("position", targetPos1,
                                          "time", timer,
                                          "onComplete", "Complete1",
                                          "easetype", "linear"));
        yield return new WaitForSeconds(0.2f);
        transform.GetComponent<TrailRenderer>().enabled = true;
    }
}
