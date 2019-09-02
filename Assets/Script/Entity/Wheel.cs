using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 车轮
/// </summary>
public class Wheel : MonoBehaviour {
    [HideInInspector]
    public float speed=0;
    //private void Start()
    //{
    //    speed = -5;
    //}
    void Update () {
        transform.Rotate(Vector3.right*-speed*3);
	}
}
