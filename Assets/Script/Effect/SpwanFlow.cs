using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanFlow : MonoBehaviour {
    
	void Start () {
        for(int i = 0; i < transform.GetChild(0).childCount; i++) { 
            StartCoroutine(CyclicGeneration(this.gameObject, transform.GetChild(0).GetChild(i).GetComponentsInChildren<Transform>()));//流淌效果
        }
    }
    IEnumerator CyclicGeneration(GameObject go,Transform[] endPoint)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(1.5f);
        int i = 6;
        while (i > 0)
        {
            GameObject flow = Instantiate(GameManager.instance.FlowingParticles, go.transform.position, Quaternion.identity);//生成粒子动画
            flow.AddComponent<FlowDian>().Move(go.transform, endPoint);
            i--;
            yield return waitForSeconds;
        }
    }
}
