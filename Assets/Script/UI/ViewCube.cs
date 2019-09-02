using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 视角方块
/// </summary>

public class ViewCube : MonoBehaviour {
    private static Transform viewCubeTransX;
    private static Transform viewCubeTransY;
    private static GameObject NSEWPositive;//东南西北  正方向
    private static GameObject NSEWNegative;//东南西北  反方向
    private void Awake()
    {
        viewCubeTransX = this.transform;
        viewCubeTransY = viewCubeTransX.GetChild(0);
        NSEWPositive = viewCubeTransX.GetChild(0).GetChild(0).GetChild(0).gameObject;
        NSEWNegative = viewCubeTransX.GetChild(0).GetChild(0).GetChild(1).gameObject;
    }
    public static void SetAngleY(float angle)
    {
        viewCubeTransY.localEulerAngles = new Vector3(viewCubeTransY.localEulerAngles.x, -(angle-180), viewCubeTransY.localEulerAngles.z);
    }

    public static void SetAngleX(float angle)
    {
        viewCubeTransX.localEulerAngles = new Vector3(-angle,viewCubeTransX.localEulerAngles.y, viewCubeTransX.localEulerAngles.z);
        SetDir_NSEW(viewCubeTransX.localEulerAngles.x>180);
    }
    /// <summary>
    /// 设置东南西北旋转朝向、 位置
    /// </summary>
    /// <param name="isBool"></param>
    static void SetDir_NSEW(bool isBool)
    {
        NSEWPositive.SetActive(isBool);
        NSEWNegative.SetActive(!isBool);
    }
}
