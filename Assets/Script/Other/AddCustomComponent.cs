using HighlightingSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 添加自定义组件
/// </summary>
public class AddCustomComponent : MonoBehaviour {
    /// <summary>
    /// 桥父物体
    /// </summary>
    public GameObject parentGO;
    MeshRenderer[] renderers;
    private LayerMask bridgeLayer;
    private void Awake()
    {
        parentGO = GameObject.Find("RootNode");
        bridgeLayer = LayerMask.NameToLayer("Bridge");
    }
    void Start () {
        GameManager.instance.parentGO = parentGO;
        renderers = parentGO.GetComponentsInChildren<MeshRenderer>();
        GameManager.instance.constructionMemberMR = renderers;
        foreach (MeshRenderer meshRenderer in renderers)
        {
            meshRenderer.gameObject.AddComponent<MeshCollider>();
            meshRenderer.gameObject.AddComponent<Highlighter>().enabled = false;
            meshRenderer.gameObject.AddComponent<HighlighterFlashing>().enabled=false;
            meshRenderer.gameObject.AddComponent<ConstructionMember>();
            meshRenderer.gameObject.layer = bridgeLayer;
        }
        FileWrite.instance.namestr = new string[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            FileWrite.instance.namestr[i] = renderers[i].name;
        }
    }
	
}
