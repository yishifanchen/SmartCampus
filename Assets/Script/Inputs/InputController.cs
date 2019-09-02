using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [HideInInspector] public bool inputMouseKey0 = false;
    [HideInInspector] public bool inputMouseKeyDown0 = false;
    [HideInInspector] public bool inputMouseKey1 = false;
    [HideInInspector] public bool inputMouseKeyDown1 = false;
    [HideInInspector] public bool inputMouseKey2 = false;
    [HideInInspector] public bool inputMouseKeyDown2 = false;
    [HideInInspector] public bool inputDoubleClick = false;
    [HideInInspector] public bool inputKeyShift = false;
    [HideInInspector] public bool inputKeySpace = false;
    [HideInInspector] public bool inputKeyW = false;
    [HideInInspector] public bool inputKeyA = false;
    [HideInInspector] public bool inputKeyS = false;
    [HideInInspector] public bool inputKeyD = false;
    [HideInInspector] public bool inputKeyF = false;
    [HideInInspector] public bool inputKeyQ = false;
    [HideInInspector] public bool inputKeyE = false;
    [HideInInspector] public bool inputKeyESC = false;
    [HideInInspector] public float inputMouseX = 0;
    [HideInInspector] public float inputMouseY = 0;
    [HideInInspector] public float inputMouseWheel = 0;

    private void Update()
    {
        inputDoubleClick = false;
        inputKeyW = Input.GetKey("w");
        inputKeyA = Input.GetKey("a");
        inputKeyS = Input.GetKey("s");
        inputKeyD = Input.GetKey("d");
        inputKeyF = Input.GetKey("f");
        inputKeyQ = Input.GetKey("q");
        inputKeyE = Input.GetKey("e");

        inputMouseKey0 = Input.GetKey("mouse 0");
        inputMouseKey1 = Input.GetKey("mouse 1");
        inputMouseKey2 = Input.GetKey("mouse 2");
        inputMouseKeyDown0 = Input.GetKeyDown("mouse 0");
        inputMouseKeyDown1 = Input.GetKeyDown("mouse 1");
        inputMouseKeyDown2 = Input.GetKeyDown("mouse 2");

        inputMouseX = Input.GetAxisRaw("Mouse X");
        inputMouseY = Input.GetAxisRaw("Mouse Y");
        inputMouseWheel = Input.GetAxisRaw("Mouse ScrollWheel");

        inputKeyShift = Input.GetKey("left shift");
        inputKeySpace = Input.GetKey("space");
        inputKeyESC = Input.GetKey("escape");

        if (HaveClickTwice(0.3f, ref twiceTime))
        {
            inputDoubleClick = true;
        }
    }
    float twiceTime;
    /// <summary>
    /// 鼠标双击判断
    /// </summary>
    /// <param name="offsetTime"></param>
    /// <param name="timer"></param>
    /// <returns></returns>
    bool HaveClickTwice(float offsetTime, ref float timer)
    {
        if (inputMouseKeyDown0)
            return HaveExecuteTwiceAtTime(offsetTime, ref timer);
        else
            return false;
    }
    static bool HaveExecuteTwiceAtTime(float offsetTime, ref float timer)
    {
        if (Time.time - timer < offsetTime)
            return true;
        else
        {
            timer = Time.time;
            return false;
        }
    }
}
