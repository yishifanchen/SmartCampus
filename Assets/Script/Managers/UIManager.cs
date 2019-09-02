using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static UIManager instance;
    /// <summary>
    /// 构件信息UI位置
    /// </summary>
    public RectTransform uiInfo;
    /// <summary>
    /// 构件信息文本
    /// </summary>
    public Text txtConstructionMemberInfo;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (uiInfo.gameObject.activeSelf)
            uiInfo.position = Input.mousePosition + new Vector3(0,70,0);
    }
    /// <summary>
    /// 显示构件详情
    /// </summary>
    /// <param name="isShow"></param>
    /// <param name="info"></param>
    public void ShowUIInfo(bool isShow, string info)
    {
        uiInfo.gameObject.SetActive(isShow);
        txtConstructionMemberInfo.text = info;
    }

}
