using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum SixDir
{
    FORWARD,
    BACK,
    LEFT,
    RIGHT,
    UP,
    DOWN
}

public class SixFace : MonoBehaviour
{
    public SixDir curDir=SixDir.FORWARD;
    private Text text;
    public delegate void FaceClickDelegate(string message);//六个方向点击事件委托
    public static event FaceClickDelegate faceClick;
    private void Start()
    {
        text = GetComponent<Text>();
    }
    void OnMouseOver()
    {
        text.color = Color.white;
    }
    void OnMouseExit()
    {
        if(timer==0)
            text.color = Color.black;
    }
    float timer = 0;
    void OnMouseUp()
    {
        if (timer < 0.2f)
        {
            if (faceClick != null)
            {
                faceClick(curDir.ToString());
            }
        }
        timer = 0;
        text.color = Color.black;
    }
    private void OnMouseDrag()
    {
        timer += Time.deltaTime;
        text.color = Color.white;
    }
}
