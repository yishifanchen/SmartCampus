using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class WriteVehicleDataTest : MonoBehaviour {
    public string[] strLane = {"11", "12", "13", "14", "31", "32", "33", "34"};
	void Start () {
        WriteFile();
    }
    string Write()
    {
        string strResult = "";
        strResult += "[";
        for (int i = 0; i < 15; i++)
        {
            strResult += "{\"axlesNumber\":\"";
            strResult += Random.Range(2, 3).ToString();
            strResult += "\",\"laneNo\":\"";
            strResult += strLane[Random.Range(0,strLane.Length)];
            strResult += "\",\"axleOffset\":\"";
            strResult += Random.Range(1, 150).ToString();
            strResult += "\",\"speed\":\"";
            strResult += Random.Range(40,120).ToString();
            strResult += "\"},";
        }
        strResult = strResult.Remove(strResult.Length - 1, 1);
        strResult += "]";
        return strResult;
    }
    void WriteFile()
    {
        System.IO.File.WriteAllText(Application.dataPath + "\\Resources\\Vehicle.json", string.Empty);
        File.AppendAllText(Application.dataPath + "\\Resources\\Vehicle.json", Write(), Encoding.UTF8);
        print("写入车辆数据完成");
    }
}
