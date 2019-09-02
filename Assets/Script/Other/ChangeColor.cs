using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct ColorData
{
    public float pos;
    public float r;
    public float g;
    public float b;
}
public class ChangeColor : MonoBehaviour {
    public Vector4[] colors;
    public float[] points;
    public Dictionary<float, Vector4> colorDict = new Dictionary<float, Vector4>();
    public static ColorData[] colorDatas;
    float[] nums;
    void Start () {
        
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetObjectSize();
        }
	}
    /// <summary>
    /// 获取模型尺寸
    /// </summary>
    public void GetObjectSize()
    {
        Vector3 realSize = Vector3.zero;
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3 meshSize = mesh.bounds.size;
        Vector3 scale = transform.lossyScale;
        realSize = new Vector3(meshSize.x * scale.x, meshSize.y * scale.y, meshSize.z * scale.z);
        SetColor(realSize.x);
    }
    public void SetColor(float length)
    {
        colorDict.Clear();
        ProcessColorData(LoadLocalColor());
        points = new float[colorDict.Count+2];
        colors= new Vector4[colorDict.Count + 2];
        for (int i=0;i< colorDict.Count;i++)
        {
            points[i + 1] = nums[i];
            colors[i + 1] = colorDict[nums[i]];
        }
        points[0]=-(length / 2);
        points[colorDict.Count + 1] = length / 2;

        colors[0] = new Vector4(1f, 1f, 1f, 1f);
        colors[colorDict.Count + 1] = new Vector4(1f, 1f, 1f, 1f);

        Material material = GetComponent<MeshRenderer>().material;
        material.SetInt("_Num", points.Length);
        material.SetVectorArray("_Colors", colors);
        material.SetFloatArray("_Points", points);
    }
    public static ColorData[] LoadLocalColor()
    {
        TextAsset ta = Resources.Load("Color") as TextAsset;
        JsonData jd = JsonMapper.ToObject(ta.ToString());
        colorDatas = LoadJsonColor(jd);
        return colorDatas;
    }
    public static ColorData[] LoadJsonColor(JsonData jd)
    {
        colorDatas = new ColorData[jd.Count];
        for (int i = 0; i < jd.Count; i++)
        {
            colorDatas[i].pos = float.Parse(jd[i]["Pos"].ToString());
            colorDatas[i].r = float.Parse(jd[i]["r"].ToString());
            colorDatas[i].g = float.Parse(jd[i]["g"].ToString());
            colorDatas[i].b = float.Parse(jd[i]["b"].ToString());
        }
        return colorDatas;
    }
    public void ProcessColorData(ColorData[] colorData)
    {
        List<Vector4> vec4 = new List<Vector4>(colorData.Length);
        nums = new float[colorData.Length];
        for (int i=0;i< colorDatas.Length; i++)
        {
            colorDict.Add(colorDatas[i].pos, new Vector4(colorDatas[i].r, colorDatas[i].g, colorDatas[i].b,1));
            nums[i] = colorDatas[i].pos;
        }
        Array.Sort(nums);
        foreach (var val in nums)
        {
            vec4.Add(colorDict[val]);
        }
        colorDict.Clear();
        for(int i = 0; i < nums.Length; i++)
        {
            colorDict.Add(nums[i], vec4[i]);
        }
    }
}
