using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float curSpeed=0;
    Ray ray;
    RaycastHit hit;
    public float length;
    Rigidbody rigid;
    int dir=1;
    bool once = false;
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }
    /// <summary>
    /// 设置车轮滚动速度
    /// </summary>
    /// <param name="speed"></param>
	public void SetWheelSpeed(float speed)
    {
        dir = (int)Mathf.Sign(speed);
        curSpeed = Mathf.Abs(speed);
        Wheel[] wheels = transform.GetComponentsInChildren<Wheel>();
        foreach (Wheel wheel in wheels)
        {
            wheel.speed = curSpeed;
        }
    }

    private void Update()
    {
        rigid.velocity = Vector3.right*dir* curSpeed;
        ray = new Ray(transform.position + Vector3.up, transform.right * 100);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Car")
            {
                float limit = (length + hit.collider.GetComponent<Car>().length)/2;
                if((Vector3.Distance(transform.position, hit.collider.transform.position)<limit+2)){
                    if (hit.collider.GetComponent<Car>().curSpeed < 20)
                        hit.collider.GetComponent<Car>().curSpeed += 0.3f;
                    if (curSpeed>4)
                        curSpeed = hit.collider.GetComponent<Car>().curSpeed-1;
                    else
                        curSpeed = hit.collider.GetComponent<Car>().curSpeed;
                }
            }
        }
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawRay(transform.position + Vector3.up, transform.right * 100);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AirWall")
        {
            Destroy(gameObject);
        }
    }
}
