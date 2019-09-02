using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ConvertCsvToJson : MonoBehaviour {
    
	void Start () {
        WriteFile();
    }
    public string LoadCsv()
    {
        string strResult = "";
        strResult += "[";
        TextAsset ta = Resources.Load("测点坐标") as TextAsset;
        string[] lines = ta.text.Split('\n');
        foreach(string line in lines)
        {
            if (line.Contains(","))
            {
                string[] str = line.Split(',');
                strResult += "{\"name\":\"";
                strResult += str[1];
                strResult += "\",\"type\":\"";
                strResult += str[0];
                strResult += "\",\"x\":\"";
                strResult += str[2];
                strResult += "\",\"y\":\"";
                strResult += str[3];
                strResult += "\",\"z\":\"";
                strResult += str[4].TrimEnd();
                strResult += "\",\"color\":\"";
                strResult += "#"+ RandomColor();
                strResult += "\",\"memberName\":\"";
                strResult += "索塔";
                strResult += "\"},";
            }
        }
        
        strResult=strResult.Remove(strResult.Length - 1, 1);
        strResult += "]";
        return strResult;
    }
    void WriteFile()
    {
        System.IO.File.WriteAllText(Application.dataPath + "\\Resources\\MeasurePoint.json", string.Empty);
        File.AppendAllText(Application.dataPath + "\\Resources\\MeasurePoint.json", LoadCsv(), Encoding.UTF8);
        print("写入测点数据完成");
    }
    public string RandomColor()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        Color color = new Color(r, g, b);
        return ColorUtility.ToHtmlStringRGB(color);
    }
}
