using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class FileWrite : MonoBehaviour {
    public static FileWrite instance;
    public string[] namestr;
    private void Awake()
    {
        instance = this;
    }
    void Start () {
        Invoke("WriteFile", 4);
    }
    string ccolor= "#F8F333";
    string getColor()
    {
        var str = "#";
        //一个十六进制的值的数组 
        string[] arr = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };
        for (var i = 0; i < 6; i++)
        {
            str += arr[UnityEngine.Random.Range(0,15)];    //产生的每个随机数都是一个索引,根据索引找到数组中对应的值,拼接到一起 
        }
        return str;
    }

    string WriteString(string[] namestr)
    {
        string str="";
        str += "[";
        for (int i=0;i<namestr.Length-1;i++)
        {
            str += "{\"name\":\"";
            str += namestr[i];
            str += "\",\"color\":\"";
            str += UnityEngine.Random.Range(1,5);
            str += "\"},";
        }
        str += "{\"name\":\"";
        str += namestr[namestr.Length-1];
        str += "\",\"color\":\"";
        str += UnityEngine.Random.Range(1, 5);
        str += "\"}";
        str += "]";
        return str;
    }
    void WriteFile()
    {
        System.IO.File.WriteAllText(Application.dataPath+"\\Resources\\ComponentColor2.json", string.Empty);
        File.AppendAllText(Application.dataPath + "\\Resources\\ComponentColor2.json", WriteString(namestr), Encoding.UTF8);
        print("写入完成");

        //F:\AWorkSpace\UnityPorject2018.2.6\CableStayedBridge\Assets\Resources
    }
}
