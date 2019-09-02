using HighlightingSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 建筑构件
/// </summary>
public class ConstructionMember : MonoBehaviour
{
    private Highlighter highlighter;
    private HighlighterFlashing highlighterFlashing;
    [HideInInspector] public Material[] originalMat;//构件原始材质
    [HideInInspector] public static Color originalColor;//原始颜色
    MeshRenderer meshRenderer;
    bool warning = false;
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        highlighter = GetComponent<Highlighter>();
        highlighterFlashing = GetComponent<HighlighterFlashing>();
        highlighterFlashing.flashingStartColor = Color.white;
    }
    private void OnMouseEnter()
    {
        UIManager.instance.ShowUIInfo(true, gameObject.name);
        if(!warning)
            ShowHighlighter(true);
    }
    private void OnMouseExit()
    {
        UIManager.instance.ShowUIInfo(false, gameObject.name);
        if (!warning)
            ShowHighlighter(false);
    }
    public void ShowHighlighter(bool isShow)
    {
        warning = false;
        highlighter.enabled = isShow;
        highlighterFlashing.enabled = isShow;
        highlighterFlashing.flashingEndColor = Color.yellow;
        highlighterFlashing.flashingFrequency = 0;
        StartCoroutine(highlighterFlashing.DelayFlashing());
    }
    public void ShowHighlighter(bool isShow, Color color)
    {
        warning = true;
        highlighter.enabled = isShow;
        highlighterFlashing.enabled = isShow;
        highlighterFlashing.flashingEndColor = color;
        highlighterFlashing.flashingFrequency = 2;
        StartCoroutine(highlighterFlashing.DelayFlashing());
    }
    /// <summary>
    /// 显示隐藏渲染
    /// </summary>
    /// <param name="isShow"></param>
    public void ShowMeshRender(bool isShow)
    {
        Material[] tranparentMats = new Material[meshRenderer.materials.Length];
        for (int i = 0; i < tranparentMats.Length; i++)
        {
            tranparentMats[i] = GameManager.instance.transparentMat;
        }
        meshRenderer.materials = isShow ? originalMat : tranparentMats;
        GetComponent<Collider>().enabled = isShow;
    }
}
