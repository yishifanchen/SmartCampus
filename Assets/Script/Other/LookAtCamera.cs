using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {
    
	void Update () {
        this.transform.LookAt(Camera.main.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(Camera.main.transform.position - this.transform.position), 0);
	}
}
