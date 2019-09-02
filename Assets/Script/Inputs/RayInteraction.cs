using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 射线检测交互
/// </summary>
public class RayInteraction : MonoBehaviour {
    public static RayInteraction instance;
    private Ray ray;
    private RaycastHit hit;
    private ControllerOrbit controllerOrbit;
    private InputController IC;
    private LayerMask bridgeLayer;
    private LayerMask measurePointLayer;

    int detectionLayer;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        controllerOrbit = GetComponent<ControllerOrbit>();
        IC = GetComponent<InputController>();
        bridgeLayer = LayerMask.NameToLayer("Bridge");
        measurePointLayer = LayerMask.NameToLayer("MeasurePoint");
        detectionLayer = (1<<measurePointLayer)| (1 << bridgeLayer);
    }
    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out hit, 1000,detectionLayer))
        {
            if (IC.inputDoubleClick)
            {
                if (hit.collider.gameObject.layer == measurePointLayer)
                {
                    LocationMeasurePoint(hit.collider.gameObject,true);
                    StartCoroutine(GameManager.instance.DelayShowComponent(hit.collider.gameObject.GetComponent<MeasurePoint>().componentName));
                }
                if (hit.collider.gameObject.layer == bridgeLayer)
                {
                    LocationConstructionMember(hit.collider.gameObject,true);
                }
            }
            if (IC.inputMouseKeyDown1)
            {
                if (hit.collider.gameObject.layer == measurePointLayer)
                {
                    UIManager.instance.ShowUIInfo(false, hit.collider.gameObject.name);
                    hit.collider.gameObject.SetActive(false);
                    //controllerOrbit.cameraTarget = hit.collider.gameObject.transform;
                }
                if (hit.collider.gameObject.layer == bridgeLayer)
                {
                    UIManager.instance.ShowUIInfo(false, hit.collider.gameObject.name);
                    hit.collider.gameObject.GetComponent<ConstructionMember>().ShowMeshRender(false);
                    //controllerOrbit.cameraTarget = hit.collider.gameObject.transform;
                }
            }
        }
        else
        {
            if (IC.inputDoubleClick)
            {
                //if (!GameManager.instance.isHideAll) return;
                controllerOrbit.cameraTarget = transform;
                GameManager.instance.ShowAllConstructionMemberMR();
                if (GameManager.instance.measurePointSelectTemp != null&& GameManager.instance.measurePointSelectTemp.transform.gameObject.activeSelf)
                {
                    GameManager.instance.measurePointSelectTemp.GetComponent<MeasurePoint>().ShowHighlighter(false);
                    GameManager.instance.measurePointSelectTemp = null;
                }
            }
        }
    }
    /// <summary>
    /// 定位测点
    /// </summary>
    /// <param name="go"></param>
    public void LocationMeasurePoint(GameObject go,bool callwgl)
    {
        if (GameManager.instance.measurePointSelectTemp != null && go != GameManager.instance.measurePointSelectTemp)
        {
            print(GameManager.instance.measurePointSelectTemp.name);
            if(GameManager.instance.measurePointSelectTemp.activeSelf)
                GameManager.instance.measurePointSelectTemp.GetComponent<MeasurePoint>().ShowHighlighter(false);
        }
        GameManager.instance.measurePointSelectTemp = go;
        controllerOrbit.cameraTarget = go.transform;
        if (callwgl)
        {
            DataHandler.CallWGL("SelectMonitorPoint", go.GetComponent<MeasurePoint>().measurePointType, go.name);
        }//调用网页函数
        print("点击测点" + go.name);
        StartCoroutine(go.GetComponent<MeasurePoint>().DelayHighlighter(0.05f));
        foreach (MeasurePoint measurePoint in GameManager.instance.measurePointsList)
        {
            measurePoint.ShowHighlighter(false);
        }
        GameManager.instance.HideAllConstructionMemberMR(null);
    }
    /// <summary>
    /// 定位构件
    /// </summary>
    /// <param name="go"></param>
    public void LocationConstructionMember(GameObject go,bool callwgl)
    {
        controllerOrbit.cameraTarget = go.transform;
        //if (GameManager.instance.isHideAll)
        //{
        //    GameManager.instance.ShowAllConstructionMemberMR();
        //    //controllerOrbit.cameraTarget = transform;
        //}
        //else
        GameManager.instance.HideAllConstructionMemberMR(go);
        if(callwgl)DataHandler.CallWGL("SelectComponent", go.name);//调用网页函数
    }
}
