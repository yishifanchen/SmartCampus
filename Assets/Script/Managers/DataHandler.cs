using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;
/// <summary>
/// 测点数据
/// </summary>
public struct MeasurePointData
{
    public string name;
    public string type;
    public float x;
    public float y;
    public float z;
    public Color color;
    public string memberName;
}
/// <summary>
/// 构件颜色
/// </summary>
public struct ComponentColorData
{
    public string name;//构件名称
    public string diseaseDegree;//病害程度
}
/// <summary>
/// 预警数据
/// </summary>
public struct WarningData
{
    public string measurePointName;//测点名称
    public string componentName;//构件名称       “，”隔开
    public string warningDegree;//预警类型  
}
/// <summary>
/// 车辆信息
/// </summary>
public struct VehicleData
{
    public int axlesNumber;//车轴，哪种车
    public int laneNo;//车道编号
    public float axleOffset;//车道偏移量
    public float speed;//初始速度
}
/// <summary>
/// 数据交互   webgl与unity
/// </summary>
public class DataHandler : MonoBehaviour
{
    public static MeasurePointData[] measurePointDatas;
    public static ComponentColorData[] componentColorDatas;
    public static WarningData[] warningData;
    public static VehicleData[] vehicleDatas;
    public static Dictionary<string, Color> ColorDict = new Dictionary<string, Color>();
    public static Dictionary<string, Color> WarningColorDict = new Dictionary<string, Color>();
    Color clr;
    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#969696", out clr);//969696    007AD9
        ColorDict.Add("1", clr);
        ColorUtility.TryParseHtmlString("#13C2C2", out clr);
        ColorDict.Add("2", clr);
        ColorUtility.TryParseHtmlString("#FEFA52", out clr);
        ColorDict.Add("3", clr);
        ColorUtility.TryParseHtmlString("#FFB951", out clr);
        ColorDict.Add("4", clr);
        ColorUtility.TryParseHtmlString("#FF6058", out clr);
        ColorDict.Add("5", clr);

        ColorUtility.TryParseHtmlString("#FFFF00", out clr);//黄
        WarningColorDict.Add("1", clr);
        ColorUtility.TryParseHtmlString("#FFA500", out clr);//橙
        WarningColorDict.Add("2", clr);
        ColorUtility.TryParseHtmlString("#FF0000", out clr);//红
        WarningColorDict.Add("3", clr);
    }
    //string ttt="[{"+"\"measurePointName\""+":"+"\"QS19\""+","+"\"componentName\""+":"+"\"12#墩1#支座\""+","+"\"warningDegree\":"+"\"1\""+"}]";
    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.D))
    //        CallBackWGL_LoadWarning(ttt);
    //}
    /// <summary>
    /// 根据json数据返回测点信息数组
    /// </summary>
    public static MeasurePointData[] LoadDataMP(JsonData jd)
    {
        measurePointDatas = new MeasurePointData[jd.Count];
        for (int i = 0; i < jd.Count; i++)
        {
            measurePointDatas[i].name = jd[i]["name"].ToString();
            measurePointDatas[i].type = jd[i]["type"].ToString();
            measurePointDatas[i].x = float.Parse(jd[i]["x"].ToString());
            measurePointDatas[i].y = float.Parse(jd[i]["z"].ToString());
            measurePointDatas[i].z = float.Parse(jd[i]["y"].ToString());
            ColorUtility.TryParseHtmlString(jd[i]["color"].ToString(), out measurePointDatas[i].color);
            measurePointDatas[i].memberName = jd[i]["memberName"].ToString();
        }
        return measurePointDatas;
    }
    /// <summary>
    /// 根据json数据返回构件颜色信息数组
    /// </summary>
    /// <returns></returns>
    public static ComponentColorData[] LoadDataCC(JsonData jd)
    {
        componentColorDatas = new ComponentColorData[jd.Count];
        for (int i = 0; i < jd.Count; i++)
        {
            componentColorDatas[i].name = jd[i]["name"].ToString();
            componentColorDatas[i].diseaseDegree = jd[i]["color"].ToString();
        }
        return componentColorDatas;
    }
    /// <summary>
    /// 根据json数组返回测点关联的构件预警  改变构件颜色
    /// </summary>
    /// <param name="jd"></param>
    /// <returns></returns>
    public static WarningData[] LoadDataWarning(JsonData jd)
    {
        warningData = new WarningData[jd.Count];
        for (int i = 0; i < jd.Count; i++)
        {
            warningData[i].measurePointName = jd[i]["measurePointName"].ToString();
            warningData[i].componentName = jd[i]["componentName"].ToString();
            warningData[i].warningDegree = jd[i]["warningDegree"].ToString();
        }
        return warningData;
    }
    /// <summary>
    /// 根据json数组返回车辆信息生成车辆
    /// </summary>
    /// <param name="jd"></param>
    /// <returns></returns>
    public static VehicleData[] LoadVehicle(JsonData jd)
    {
        vehicleDatas = new VehicleData[jd["carList"].Count];
        for (int i = 0; i < jd["carList"].Count; i++)
        {
            vehicleDatas[i].axlesNumber = int.Parse(jd["carList"][i]["axlesNumber"].ToString());
            vehicleDatas[i].laneNo = int.Parse(jd["carList"][i]["laneNo"].ToString());
            vehicleDatas[i].axleOffset = float.Parse(jd["carList"][i]["axleOffset"].ToString());
            vehicleDatas[i].speed = float.Parse(jd["carList"][i]["speed"].ToString());
        }
        return vehicleDatas;
    }
    /// <summary>
    /// 调用网页javascript函数
    /// </summary>
    /// <param name="method"></param>
    /// <param name="value"></param>
    public static void CallWGL(string method)
    {
        Application.ExternalCall(method);
    }
    /// <summary>
    /// 调用网页javascript函数
    /// </summary>
    /// <param name="method"></param>
    /// <param name="value"></param>
    public static void CallWGL(string method, string value)
    {
        Application.ExternalCall(method, value);
    }
    /// <summary>
    /// 调用网页javascript函数
    /// </summary>
    /// <param name="method"></param>
    /// <param name="type"></param>
    /// <param name="value"></param>
    public static void CallWGL(string method, string type, string value)
    {
        Application.ExternalCall(method, type, value);
    }
    /// <summary>
    /// 回调webgl函数,场景刚加载完毕，需要传入参数，根据字符串判断加载哪些功能，0：无功能  1：生成测点， 2：加载构件病害标度颜色
    /// </summary>
    public void CallBackWGL_StartScene(string index)
    {
        switch (index)
        {
            case "0"://首页
                GameManager.instance.EnableViewCube(true);
                break;
            case "1"://测点时程
                GameManager.instance.defaultHighFlash = true;
                GameManager.instance.isShowFlow = true;
                CallWGL("LoadMeasurePoint");//加载测点信息
                GameManager.instance.HideAllConstructionMemberMR(null);
                GameManager.instance.EnableViewCube(true);
                break;
            case "2"://病害记录
                CallWGL("LoadComponentColor");//加载构件病害颜色
                GameManager.instance.EnableViewCube(false);
                break;
            case "3"://项目概况
                GameManager.instance.defaultHighFlash = true;
                CallWGL("LoadMeasurePoint");//加载测点信息
                GameManager.instance.isShowFlow = false;
                GameManager.instance.isGlobalVirtual = true;//默认全局虚化
                GameManager.instance.HideAllConstructionMemberMR(null);
                GameManager.instance.EnableViewCube(true);
                break;
            case "4"://三维预警
                GameManager.instance.isHidePoint = true;
                GameManager.instance.isShowFlow = false;
                GameManager.instance.CloseCollider();
                GameManager.instance.EnableViewCube(true);
                CallWGL("LoadMeasurePoint");//加载测点信息
                CallWGL("LoadWarning");//加载预警数据
                CallWGL("LoadVehicle");//加载车辆
                StartCoroutine(DelayLoadWarning());
                StartCoroutine(DelayLoadVehicle());
                break;
            case "5"://云图
                GameManager.instance.isGradient = true;
                GameManager.instance.isShowFlow = false;
                GameManager.instance.EnableViewCube(true);
                break;
        }
    }
    /// <summary>
    /// 每隔5分钟加载一次数据
    /// </summary>
    /// <returns></returns>
    IEnumerator DelayLoadWarning()
    {
        while (true)
        {
            yield return new WaitForSeconds(300);//五分钟
            GameManager.instance.SetConstructionMemberOriginalColor();//所有构件颜色初始化
            GameManager.instance.CloseCollider();
            CallWGL("LoadWarning");//加载预警数据
        }
    }
    /// <summary>
    /// 每隔1分钟加载一次车辆数据
    /// </summary>
    /// <returns></returns>
    IEnumerator DelayLoadVehicle()
    {
        while (true)
        {
            yield return new WaitForSeconds(60);//一分钟
            CallWGL("LoadVehicle");//加载车辆
        }
    }
    /// <summary>
    /// 网页调用（生成测点）
    /// </summary>
    /// <param name="str"></param>
    public void CallBackWGL_LoadMeasurePoint(string str)
    {
        JsonData jd = JsonMapper.ToObject(str);
        measurePointDatas = LoadDataMP(jd);
        GameManager.instance.LoadMeasurePointData(measurePointDatas);
    }
    /// <summary>
    /// 网页调用（构件上色）
    /// </summary>
    /// <param name="str"></param>
    public void CallBackWGL_LoadComponentColor(string str)
    {
        JsonData jd = JsonMapper.ToObject(str);
        componentColorDatas = LoadDataCC(jd);
        GameManager.instance.LoadComponentColorData(componentColorDatas);
    }
    /// <summary>
    /// 网页调用（预警信息，测点关联构件变色）
    /// </summary>
    /// <param name="str"></param>
    public void CallBackWGL_LoadWarning(string str)
    {
        JsonData jd = JsonMapper.ToObject(str);
        warningData = LoadDataWarning(jd);
        GameManager.instance.LoadWarningData(warningData);
    }
    /// <summary>
    /// 网页调用（车辆信息，生成车辆）
    /// </summary>
    /// <param name="str"></param>
    public void CallBackWGL_LoadVehicle(string str)
    {
        JsonData jd = JsonMapper.ToObject(str);
        vehicleDatas = LoadVehicle(jd);
        GameManager.instance.LoadVehicleData(vehicleDatas);
    }
    /// <summary>
    /// 加载本地json数据生成测点信息数组
    /// </summary>
    /// <returns></returns>
    public static MeasurePointData[] LoadLocalMeasurePoint()
    {
        TextAsset ta = Resources.Load("MeasurePoint") as TextAsset;
        JsonData jd = JsonMapper.ToObject(ta.ToString());
        measurePointDatas = LoadDataMP(jd);
        return measurePointDatas;
    }
    public static VehicleData[] LoadLoaclVehicleDatas()
    {
        TextAsset ta = Resources.Load("Vehicle") as TextAsset;
        JsonData jd = JsonMapper.ToObject(ta.ToString());
        vehicleDatas = LoadVehicle(jd);
        return vehicleDatas;
    }
    public static IEnumerator WWWLoadLocalMeasurePoint()
    {
        WWW www = new WWW(@"file:\F:\AWorkSpace\UnityPorject2018.2.6\CableStayedBridge\Assets\Resources\MeasurePoint.json");
        yield return www;
        JsonData jd = JsonMapper.ToObject(www.text);
        measurePointDatas = LoadDataMP(jd);
        GameManager.instance.LoadMeasurePointData(measurePointDatas);
    }
    public static IEnumerator WWWLoadLocalComponentColor()
    {
        WWW www = new WWW(@"file:\F:\AWorkSpace\UnityPorject2018.2.6\CableStayedBridge\Assets\Resources\ComponentColor.json");
        yield return www;
        JsonData jd = JsonMapper.ToObject(www.text);
        componentColorDatas = LoadDataCC(jd);
        GameManager.instance.LoadComponentColorData(componentColorDatas);
    }
    /// <summary>
    /// 加载本地json数据生成构件颜色信息数组
    /// </summary>
    /// <returns></returns>
    public static ComponentColorData[] LoadLocalComponentColor()
    {
        TextAsset ta = Resources.Load("ComponentColor2") as TextAsset;
        JsonData jd = JsonMapper.ToObject(ta.ToString());
        componentColorDatas = LoadDataCC(jd);
        return componentColorDatas;
    }
}
