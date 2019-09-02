using HighlightingSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 测点
/// </summary>
public class MeasurePoint : MonoBehaviour {
    [HideInInspector]public string measurePointType;
    private Highlighter highlighter;
    private HighlighterFlashing highlighterFlashing;
    public string componentName;

    Transform sphereTrans;
    SphereCollider sphereCollider;
    private void Start()
    {
        sphereTrans = transform.GetChild(0);
        sphereCollider = GetComponent<SphereCollider>();
        highlighter = GetComponent<Highlighter>();
        highlighterFlashing = GetComponent<HighlighterFlashing>();
    }
    private void Update()
    {
        sphereTrans.localScale = Vector3.one*(ControllerOrbit.followDistance*0.015f);
        sphereTrans.localScale = new Vector3(Mathf.Clamp(sphereTrans.localScale.x,0.15f,0.7f), Mathf.Clamp(sphereTrans.localScale.y, 0.15f, 0.7f), Mathf.Clamp(sphereTrans.localScale.z, 0.15f, 0.7f));
        sphereCollider.radius = sphereTrans.localScale.x / 2;
    }
    /// <summary>
    /// 设置测点信息
    /// </summary>
    public void SetMeasurePoingInfo(string type)
    {
        measurePointType = type;
    }
    public void OnClick()
    {
        print(gameObject.name);
    }
    private void OnMouseEnter()
    {
        ShowHighlighter(true);
        UIManager.instance.ShowUIInfo(true, gameObject.name);
    }
    private void OnMouseExit()
    {
        UIManager.instance.ShowUIInfo(false, gameObject.name);
        if (GameManager.instance.measurePointSelectTemp==this.gameObject)return;
        ShowHighlighter(false);
    }
    public void ShowHighlighter(bool isShow)
    {
        highlighter.enabled = isShow;
        highlighterFlashing.enabled = isShow;
        highlighterFlashing.flashingEndColor = Color.yellow;
        highlighterFlashing.flashingFrequency = 0;
        StartCoroutine(highlighterFlashing.DelayFlashing());
    }
    public void ShowHighlighter(bool isShow,Color color)
    {
        highlighter.enabled = isShow;
        highlighterFlashing.enabled = isShow;
        highlighterFlashing.flashingEndColor = color;
        highlighterFlashing.flashingFrequency = 2;
        StartCoroutine(highlighterFlashing.DelayFlashing());
    }
    public IEnumerator DelayHighlighter(float timer)
    {
        yield return new WaitForSeconds(timer);
        ShowHighlighter(true,Color.red);
    }
}

