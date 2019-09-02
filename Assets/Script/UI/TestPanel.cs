using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 测试面板
/// </summary>
public class TestPanel : MonoBehaviour {
    public static TestPanel instance;
    public RectTransform parentRect;
    public Button btnPrefab;
    public InputField inputField;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        CreateBtn("显示全部");
        inputField.onEndEdit.AddListener(EndValue);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //DataHandler.CallBackWGL_StartScene("3");
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameManager.instance.CallBackWGL_ShowComponmentByType("1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameManager.instance.CallBackWGL_ShowComponmentByType("2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameManager.instance.CallBackWGL_ShowComponmentByType("3");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GameManager.instance.CallBackWGL_ShowComponmentByType("4");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            GameManager.instance.CallBackWGL_ShowComponmentByType("5");
        }
    }
    public void CreateBtn(string type)
    {
        Button btn = Instantiate(btnPrefab);
        btn.transform.GetChild(0).GetComponent<Text>().text = type;
        btn.onClick.AddListener(()=>OnBtnClick(type));
        btn.transform.SetParent(parentRect);
    }
    void OnBtnClick(string type)
    {
        GameManager.instance.CallBackWGL_ShowMeasurePointByType(type);
    }
    void EndValue(string name)
    {
        GameManager.instance.CallBackWGL_LocationMeasurePoint(inputField.text);
    }
}
