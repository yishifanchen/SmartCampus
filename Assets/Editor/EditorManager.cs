using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

public class EditorManager : Editor {

	[MenuItem("Custom/PrintChildCount")]
    static void PrintChildCount()
    {
        Transform transform = Selection.activeTransform;
        Debug.Log(transform.childCount);
    }
    [MenuItem("Custom/PrintInfo")]
    static void PrintInfo()
    {
        Transform transform = Selection.activeTransform;
        Debug.Log(transform.name+":"+transform.position);
        WriteFile("\n"+transform.name + "," + transform.position.x+","+ transform.position.y + "," + transform.position.z);
    }
    static void WriteFile(string str)
    {
        System.IO.File.WriteAllText(Application.dataPath + "\\Resources\\从模型导出的测点位置.txt", string.Empty);
        File.AppendAllText(Application.dataPath + "\\Resources\\从模型导出的测点位置.txt", str, Encoding.UTF8);
    }
}
