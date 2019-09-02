using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public MeshRenderer[] constructionMemberMR;
    public bool isHideAll;
    public Material transparentMat;//透明材质
    public GameObject measurePointPrefab;

    public VehicleDataConfig[] dataConfig;
    public Transform vehiclePos;

    [HideInInspector] public GameObject measurePointParent;
    [HideInInspector] public GameObject parentGO;

    public GameObject ViewCubeCamera;

    public GameObject FlowingParticles;//流动粒子
    public Transform endPoint;
    public Transform endPoint1;

    /// <summary>
    /// 病害类型-构件列表数组
    /// </summary>
    List<GameObject>[] DiseaseComponent = new List<GameObject>[5];
    /// <summary>
    /// 全局虚化
    /// </summary>
    public bool isGlobalVirtual = false;
    /// <summary>
    /// 是否默认高闪
    /// </summary>
    public bool defaultHighFlash = false;
    /// <summary>
    /// 是否隐藏测点
    /// </summary>
    public bool isHidePoint = false;
    /// <summary>
    /// 是否显示流淌效果
    /// </summary>
    public bool isShowFlow = false;
    [Tooltip("流淌特效坐标顺序")]
    /// <summary>
    /// 流淌顺序
    /// </summary>
    public FlowSequence flowSequence=FlowSequence.xyz;
    /// <summary>
    /// 是否生成预警车辆
    /// </summary>
    public bool isVehicle = false;
    /// <summary>
    /// 是否模型渐变色
    /// </summary>
    public bool isGradient = false;
    /// <summary>
    /// 选择的测点
    /// </summary>
    public GameObject measurePointSelectTemp = null;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        for (int i = 0; i < DiseaseComponent.Length; i++)
        {
            DiseaseComponent[i] = new List<GameObject>();
        }
        measurePointParent = GameObject.Find("MeasurePoints");
        ConstructionMemberMROriginalMat();
        
#if UNITY_WEBGL
        DataHandler.CallWGL("OnSceneLoaded", "true");//加载场景完成
#endif
#if UNITY_EDITOR
        //LoadMeasurePointData(DataHandler.LoadLocalMeasurePoint());
        //LoadVehicleData(DataHandler.LoadLoaclVehicleDatas());
        //InvokeRepeating("LoadVeh", 0.1f,30);
        //LoadComponentColorData(DataHandler.LoadLocalComponentColor());
        //GameManager.instance.HideAllConstructionMemberMR(null);
        //if (isGradient)
            //parentGO.AddComponent<MergeMesh>().MeshMerge();
#endif
    }


    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.L))
    //    {
    //        CallBackWGL_LocationConstructionMember("左幅_23#墩墩基础");
    //    }
    //}
    void LoadVeh()
    {
        LoadVehicleData(DataHandler.LoadLoaclVehicleDatas());
    }
    /// <summary>
    /// 设置所有构件颜色初始化
    /// </summary>
    public void SetConstructionMemberOriginalColor()
    {
        foreach (MeshRenderer meshRenderer in constructionMemberMR)
        {
            meshRenderer.materials = meshRenderer.GetComponent<ConstructionMember>().originalMat;
            meshRenderer.GetComponent<ConstructionMember>().ShowHighlighter(false);
            for (int j = 0; j < meshRenderer.materials.Length; j++)
            {
                meshRenderer.materials[j].color = ConstructionMember.originalColor;
            }
        }
    }
    /// <summary>
    /// 获取所有构件初始化材质、颜色
    /// </summary>
    public void ConstructionMemberMROriginalMat()
    {
        ConstructionMember.originalColor = constructionMemberMR[0].material.color;
        foreach (MeshRenderer meshRenderer in constructionMemberMR)
        {
            meshRenderer.GetComponent<ConstructionMember>().originalMat = meshRenderer.materials;
        }
    }
    /// <summary>
    /// 禁用碰撞器
    /// </summary>
    public void CloseCollider()
    {
        foreach (MeshRenderer meshRenderer in constructionMemberMR)
        {
            meshRenderer.GetComponent<Collider>().enabled = false;
        }
    }
    /// <summary>
    /// 隐藏构件渲染，透明化
    /// </summary>
    /// <param name="go"></param>
    public void HideAllConstructionMemberMR(GameObject go)
    {
        foreach (MeshRenderer meshRenderer in constructionMemberMR)
        {
            Material[] tranparentMats = new Material[meshRenderer.materials.Length];
            for (int i = 0; i < tranparentMats.Length; i++)
            {
                tranparentMats[i] = transparentMat;
            }
            meshRenderer.materials = tranparentMats;
            meshRenderer.GetComponent<Collider>().enabled = false;
        }
        if (go != null)
        {
            ShowComponent(go);
        }
        isHideAll = true;
        DataHandler.CallWGL("SetShowState", true.ToString());//网页日志
    }
    public void HideAllConstructionMemberMR(GameObject go, bool isWgl)
    {
        foreach (MeshRenderer meshRenderer in constructionMemberMR)
        {
            Material[] tranparentMats = new Material[meshRenderer.materials.Length];
            for (int i = 0; i < tranparentMats.Length; i++)
            {
                tranparentMats[i] = transparentMat;
            }
            meshRenderer.materials = tranparentMats;
            meshRenderer.GetComponent<Collider>().enabled = false;
        }
        if (go != null)
        {
            ShowComponent(go);
        }
        isHideAll = true;
    }
    /// <summary>
    /// 显示单独构件
    /// </summary>
    /// <param name="go"></param>
    void ShowComponent(GameObject go)
    {
        go.GetComponent<Collider>().enabled = true;
        go.GetComponent<MeshRenderer>().materials = go.GetComponent<ConstructionMember>().originalMat;
    }
    /// <summary>
    /// 显示构件渲染
    /// </summary>
    public void ShowAllConstructionMemberMR()
    {
        if (isGlobalVirtual) return;
        foreach (MeshRenderer meshRenderer in constructionMemberMR)
        {
            meshRenderer.materials = meshRenderer.GetComponent<ConstructionMember>().originalMat;
            meshRenderer.GetComponent<Collider>().enabled = true;
        }
        isHideAll = false;
        DataHandler.CallWGL("SetShowState", false.ToString());//网页日志
    }
    public void ShowAllConstructionMemberMR(bool isWgl)
    {
        if (isGlobalVirtual) return;
        foreach (MeshRenderer meshRenderer in constructionMemberMR)
        {
            meshRenderer.materials = meshRenderer.GetComponent<ConstructionMember>().originalMat;
            meshRenderer.GetComponent<Collider>().enabled = true;
        }
        isHideAll = false;

    }
    /// <summary>
    /// 根据数据加载测点
    /// </summary>
    public void LoadMeasurePointData(MeasurePointData[] measurePointDatas)
    {
        bool isExist = false;
        List<string> childList = new List<string>();
        GameObject child = new GameObject("go")
        {
            name = measurePointDatas[0].type
        };
        child.transform.parent = measurePointParent.transform;
        childList.Add(child.name);
        for (int i = 0; i < measurePointDatas.Length; i++)
        {
            isExist = false;
            foreach (string na in childList)
            {
                if (na == measurePointDatas[i].type)
                {
                    isExist = true;
                }
            }
            if (!isExist)
            {
                GameObject child1 = new GameObject("go")
                {
                    name = measurePointDatas[i].type
                };
                child1.transform.parent = measurePointParent.transform;
                childList.Add(child1.name);
                if (TestPanel.instance != null)
                    TestPanel.instance.CreateBtn(measurePointDatas[i].type);
            }
            string name = measurePointDatas[i].name;
            string type = measurePointDatas[i].type;
            Vector3 position = new Vector3(
                measurePointDatas[i].x,
                measurePointDatas[i].y,
                measurePointDatas[i].z
                );
            Color color = measurePointDatas[i].color;
            string cName = measurePointDatas[i].memberName;
            GenerateMeasurePoint(name, type, position, color, cName);
        }
        if (defaultHighFlash)//如果测点默认高闪
        {
            StartCoroutine(Delay());
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        DataHandler.CallWGL("DefaultHighFlash");//网页日志
    }
    /// <summary>
    /// 加载构件颜色
    /// </summary>
    public void LoadComponentColorData(ComponentColorData[] componentColorDatas)
    {
        ClearDiseaseComponentList();
        Color color;
        for (int i = 0; i < componentColorDatas.Length; i++)
        {
            GameObject go = GameObject.Find(componentColorDatas[i].name);
            if (go != null)
            {
                for (int j = 0; j < go.GetComponent<MeshRenderer>().materials.Length; j++)
                {
                    if (DataHandler.ColorDict.TryGetValue(componentColorDatas[i].diseaseDegree, out color))
                    {
                        try
                        {
                            go.GetComponent<MeshRenderer>().materials[j].color = color;
                            DiseaseComponent[int.Parse(componentColorDatas[i].diseaseDegree) - 1].Add(go);
                        }
                        catch (Exception e)
                        {
                            DataHandler.CallWGL("WGLDebugLog", e.ToString());//网页日志
                        }
                    }
                }
            }
        }
    }
    //加载预警数据
    public void LoadWarningData(WarningData[] warningDatas)
    {
        Color color;
        for (int i = 0; i < warningDatas.Length; i++)
        {
            if (warningDatas[i].componentName != "")
            {
                if (warningDatas[i].componentName.Contains(","))//如果是多个构件
                {
                    string[] names = warningDatas[i].componentName.Split(',');
                    if (DataHandler.WarningColorDict.TryGetValue(warningDatas[i].warningDegree, out color))
                    {
                        try
                        {
                            foreach (string name in names)
                            {
                                GameObject go = GameObject.Find(name);
                                {
                                    if (go != null)
                                    {
                                        ShowComponent(go);
                                        for (int j = 0; j < go.GetComponent<MeshRenderer>().materials.Length; j++)
                                        {
                                            go.GetComponent<MeshRenderer>().materials[j].color = color;
                                            go.GetComponent<ConstructionMember>().ShowHighlighter(true, color);
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            DataHandler.CallWGL("WGLDebugLog", e.ToString());//网页日志
                        }
                    }
                }
                else//只有一个构件
                {
                    if (DataHandler.WarningColorDict.TryGetValue(warningDatas[i].warningDegree, out color))
                    {
                        try
                        {
                            GameObject go = GameObject.Find(warningDatas[i].componentName);
                            {
                                if (go != null)
                                {
                                    ShowComponent(go);
                                    for (int j = 0; j < go.GetComponent<MeshRenderer>().materials.Length; j++)
                                    {
                                        go.GetComponent<MeshRenderer>().materials[j].color = color;
                                        go.GetComponent<ConstructionMember>().ShowHighlighter(true, color);
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            DataHandler.CallWGL("WGLDebugLog", e.ToString());//网页日志
                        }
                    }
                }
            }
        }
    }
    public void LoadVehicleData(VehicleData[] vehicleDatas)
    {
        for (int i = 0; i < vehicleDatas.Length; i++)
        {
            try
            {
                //车辆模型
                GameObject go = null;
                go = dataConfig[vehicleDatas[i].axlesNumber - 2].prefab[UnityEngine.Random.Range(0, dataConfig[vehicleDatas[i].axlesNumber - 2].prefab.Length)];
                //生成位置
                bool positive = vehicleDatas[i].laneNo > 20;//true：右幅  +offest         false ：左幅   -offest       左幅：11,12，13,14 右幅  31,32,33,34
                Vector3 pos = vehiclePos.transform.Find(vehicleDatas[i].laneNo.ToString()).position;
                float offest = vehicleDatas[i].axleOffset;
                pos = new Vector3(pos.x + (positive ? offest : -offest), pos.y, pos.z);
                //生成朝向
                Quaternion quat = Quaternion.Euler(0, positive ? 0 : 180, 0);
                //初始速度
                float speed = positive ? vehicleDatas[i].speed : -vehicleDatas[i].speed;
                speed *= 0.1f;
                GenerateVehicle(go, pos, quat, speed);
            }
            catch (Exception e)
            {
                DataHandler.CallWGL("WGLDebugLog", e.ToString());//网页日志
            }
        }
    }
    /// <summary>
    /// 生成测点
    /// </summary>
    /// <param name="name"></param>
    /// <param name="type"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public void GenerateMeasurePoint(string name, string type, Vector3 position, Color color, string cName)
    {
        GameObject go = Instantiate(measurePointPrefab, position, Quaternion.identity);
        go.name = name;
        go.GetComponent<MeasurePoint>().measurePointType = type;
        go.GetComponent<MeasurePoint>().componentName = cName;
        go.transform.parent = measurePointParent.transform.Find(type);
        go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = color;
        if (isHidePoint)//隐藏测点
        {
            go.GetComponent<SphereCollider>().enabled = false;
            go.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        }
        if (isShowFlow) StartCoroutine(CyclicGeneration(go));//流淌效果
    }
    /// <summary>
    /// 生成车辆
    /// </summary>
    /// <param name="goPrefab"></param>
    /// <param name="pos"></param>
    /// <param name="quat"></param>
    /// <param name="speed"></param>
    /// <returns></returns>
    public GameObject GenerateVehicle(GameObject goPrefab, Vector3 pos, Quaternion quat, float speed)
    {
        GameObject go = Instantiate(goPrefab, pos, quat);
        go.GetComponent<Car>().SetWheelSpeed(speed);
        return go;
    }
    IEnumerator CyclicGeneration(GameObject go)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(1.5f);
        int i = 5;
        while (i > 0)
        {
            GameObject flow = Instantiate(GameManager.instance.FlowingParticles, go.transform.position, Quaternion.identity);//生成粒子动画
            flow.AddComponent<Flow>().Move(go.transform, go.transform.position.z >= 0 ? endPoint : endPoint1);
            i--;
            yield return waitForSeconds;
        }
    }
    public List<MeasurePoint> measurePointsList = new List<MeasurePoint>();
    /// <summary>
    /// 按测点类型批量显隐藏测点
    /// </summary>
    /// <param name="type"></param>
    public void CallBackWGL_ShowMeasurePointByType(string type)
    {
        measurePointsList.Clear();
        try
        {
            for (int i = 0; i < measurePointParent.transform.childCount; i++)
            {
                for (int j = 0; j < measurePointParent.transform.GetChild(i).childCount; j++)
                {
                    measurePointParent.transform.GetChild(i).GetChild(j).gameObject.SetActive(false);
                }
                //measurePointParent.transform.GetChild(i).gameObject.SetActive(false);
            }
            if (type.Contains(","))
            {
                string[] names = type.Split(',');
                for (int i = 0; i < names.Length; i++)
                {
                    try
                    {
                        GameObject go = measurePointParent.transform.Find(names[i]).gameObject;
                        if (go != null)
                        {
                            //go.gameObject.SetActive(true);//显示测点父物体
                            for (int j = 0; j < go.transform.childCount; j++)
                            {
                                GameObject goChild = go.transform.GetChild(j).gameObject;
                                goChild.SetActive(true);
                                goChild.GetComponent<MeasurePoint>().ShowHighlighter(true, Color.red);
                                measurePointsList.Add(goChild.GetComponent<MeasurePoint>());
                            }
                        }
                        else
                        {
                            DataHandler.CallWGL("WGLDebugLog", "无法找到测点类型：" + name[i]);//网页日志
                        }
                    }
                    catch (Exception e)
                    {
                        DataHandler.CallWGL("WGLDebugLog", "全选测点导致问题" + e.ToString());//网页日志
                    }
                }
            }
            else if (type != "")
            {
                GameObject go = measurePointParent.transform.Find(type).gameObject;
                if (go != null)
                {
                    //go.gameObject.SetActive(true);//显示测点父物体
                    for (int j = 0; j < go.transform.childCount; j++)
                    {
                        GameObject goChild = go.transform.GetChild(j).gameObject;
                        goChild.SetActive(true);
                        goChild.GetComponent<MeasurePoint>().ShowHighlighter(true, Color.red);
                        measurePointsList.Add(goChild.GetComponent<MeasurePoint>());
                    }
                }
                else
                {
                    DataHandler.CallWGL("WGLDebugLog", "无法找到测点类型：" + type);//网页日志
                }
            }
        }
        catch (Exception e)
        {
            DataHandler.CallWGL("WGLDebugLog", e.ToString() + type);//网页日志
        }
    }
    /// <summary>
    /// 显示隐藏测点
    /// </summary>
    /// <param name="name">测点名称</param>
    /// <param name="value">是否显示</param>
    public void CallBackWGL_ShowMeasurePoint(string str)
    {
        try
        {
            string name = str.Split(',')[0];
            string value = str.Split(',')[1];
            GameObject go = DeepFindChild(measurePointParent.transform, name).gameObject;
            go.SetActive(bool.Parse(value));
        }
        catch (Exception e)
        {
            Debug.Log("无法找到测点：" + name + "&" + e);
            DataHandler.CallWGL("WGLDebugLog", "无法找到测点：" + name + "&" + e);//网页日志
        }
    }
    /// <summary>
    /// 定位单个测点
    /// </summary>
    /// <param name="name">测点名称</param>
    public void CallBackWGL_LocationMeasurePoint(string name)
    {
        try
        {
            if (name == "") return;
            GameObject go = DeepFindChild(measurePointParent.transform, name).gameObject;
            go.SetActive(true);
            ControllerOrbit.isFoucs = true;
            RayInteraction.instance.LocationMeasurePoint(go, false);
            StartCoroutine(DelayShowComponent(go.GetComponent<MeasurePoint>().componentName));
        }
        catch (Exception e)
        {
            Debug.Log("无法找到测点：" + name + "&" + e);
            DataHandler.CallWGL("WGLDebugLog", "无法找到测点：" + name + "&" + e);//网页日志
        }
    }
    /// <summary>
    /// 定位测点延时显示测点关联的构件模型
    /// </summary>
    /// <param name="cName"></param>
    /// <returns></returns>
    public IEnumerator DelayShowComponent(string cName)
    {
        yield return new WaitForSeconds(2);
        if (!string.IsNullOrEmpty(cName))
        {
            if (!cName.Contains(","))
            {
                if (cName != "")
                {
                    GameObject go = DeepFindChild(parentGO.transform, name).gameObject;
                    if (go != null)
                    {
                        go.GetComponent<ConstructionMember>().ShowMeshRender(true);
                    }
                }
            }
            else
            {
                string[] cnames = cName.Split(',');
                for (int i = 0; i < cnames.Length; i++)
                {
                    GameObject go = DeepFindChild(parentGO.transform, cnames[i]).gameObject;
                    if (go != null)
                    {
                        go.GetComponent<ConstructionMember>().ShowMeshRender(true);
                    }
                }
            }
        }
    }
    /// <summary>
    /// 定位构件
    /// </summary>
    /// <param name="name"></param>
    public void CallBackWGL_LocationConstructionMember(string name)
    {
        try
        {
            GameObject go = DeepFindChild(parentGO.transform,name).gameObject;
            RayInteraction.instance.LocationConstructionMember(go, false);
        }
        catch (Exception e)
        {
            Debug.Log("无法找到构件：" + name + "&" + e);
            DataHandler.CallWGL("WGLDebugLog", "无法找到构件：" + name + "&" + e);//网页日志
        }
    }
    /// <summary>
    /// 通过病害类型显示隐藏构件
    /// </summary>
    /// <param name="type">病害类型</param>
    public void CallBackWGL_ShowComponmentByType(string type)//type:1,2,3,4,5,
    {
        try
        {
            int index = int.Parse(type);
            HideAllConstructionMemberMR(null);
            //for (int i = 0; i < DiseaseComponent.Length; i++)
            //{
            //    for (int j = 0; j < DiseaseComponent[i].Count; j++)
            //    {
            //        DiseaseComponent[i][j].gameObject.GetComponent<ConstructionMember>().ShowMeshRender(false);
            //    }
            //}
            for (int j = 0; j < DiseaseComponent[index - 1].Count; j++)
            {
                DiseaseComponent[index - 1][j].gameObject.GetComponent<ConstructionMember>().ShowMeshRender(true);
            }
        }
        catch (Exception e)
        {
            DataHandler.CallWGL("WGLDebugLog", e.ToString());//网页日志
        }
    }
    /// <summary>
    /// 通过按钮点击显示隐藏全部构件，网页调用
    /// </summary>
    /// <param name="isBool"></param>
    public void CallBackWGL_ShowAllComponment(string isBool)
    {
        try
        {
            bool isShow = bool.Parse(isBool);
            if (isShow)
                ShowAllConstructionMemberMR(true);
            else
                HideAllConstructionMemberMR(null, true);
        }
        catch (Exception e)
        {
            DataHandler.CallWGL("WGLDebugLog", e.ToString());//网页日志
        }
    }
    /// <summary>
    /// 清除病害构件列表中的数据
    /// </summary>
    public void ClearDiseaseComponentList()
    {
        for (int i=0;i<DiseaseComponent.Length;i++)
        {
            DiseaseComponent[i].Clear();
        }
    }
    /// <summary>
    /// 深度查找子物体
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="targetName"></param>
    /// <returns></returns>
    public Transform DeepFindChild(Transform parent, string targetName)
    {
        Transform _result = null;
        _result = parent.Find(targetName);
        if (_result == null)
        {
            foreach (Transform child in parent)
            {
                _result = DeepFindChild(child, targetName);
                if (_result != null)
                {
                    return _result;
                }
            }
        }
        return _result;
    }
    /// <summary>
    /// 是否启用ViewCube
    /// </summary>
    public void EnableViewCube(bool isEnable)
    {
        ViewCubeCamera.SetActive(isEnable);
    }
}
