using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class WriteColor : MonoBehaviour {
    int length;
	void Start () {
        length = Random.Range(3, 8);
#if UNITY_EDITOR
        WriteFile();
#endif

    }
    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            WriteFile();
        }
#endif

    }
    string Write()
    {
        string strResult = "";
        strResult += "[";
        for (int i = 0; i < length; i++)
        {
            strResult += "{\"Pos\":\"";
            strResult += Random.Range(-75.0f, 75.0f).ToString();
            strResult += "\",\"r\":\"";
            strResult += Random.Range(0, 1f).ToString();
            strResult += "\",\"g\":\"";
            strResult += Random.Range(0, 1f).ToString();
            strResult += "\",\"b\":\"";
            strResult += Random.Range(0, 1f).ToString();
            strResult += "\"},";
        }
        strResult = strResult.Remove(strResult.Length - 1, 1);
        strResult += "]";
        return strResult;
    }
    void WriteFile()
    {
        System.IO.File.WriteAllText(Application.dataPath + "\\Resources\\Color.json", string.Empty);
        File.AppendAllText(Application.dataPath + "\\Resources\\Color.json", Write(), Encoding.UTF8);
        print("写入颜色数据完成");

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
}
