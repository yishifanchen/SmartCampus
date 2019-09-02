using UnityEngine;
/// <summary>
/// 合并网格
/// </summary>
public class MergeMesh : MonoBehaviour
{
    /// <summary>
    /// 合并网格
    /// </summary>
    public void MeshMerge()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();   //获取 所有子物体的网格
        CombineInstance[] combineInstances = new CombineInstance[meshFilters.Length]; //新建一个合并组，长度与 meshfilters一致
        for (int i = 0; i < meshFilters.Length; i++)                                  //遍历
        {
            combineInstances[i].mesh = meshFilters[i].sharedMesh;                   //将共享mesh，赋值
            combineInstances[i].transform = meshFilters[i].transform.localToWorldMatrix; //本地坐标转矩阵，赋值
        }
        Mesh newMesh = new Mesh();                                  //声明一个新网格对象
        newMesh.CombineMeshes(combineInstances);                    //将combineInstances数组传入函数

        GameObject newGO = new GameObject();
        newGO.name = "GradientBridge";
        newGO.AddComponent<MeshFilter>().sharedMesh = newMesh; //给当前空物体，添加网格组件；将合并后的网格，给到自身网格
                                                               //到这里，新模型的网格就已经生成了。运行模式下，可以点击物体的 MeshFilter 进行查看网格
        
        #region 以下是对新模型做的一些处理：添加材质，关闭所有子物体，添加自转脚本和控制相机的脚本

        newGO.AddComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Gradient"); //给当前空物体添加渲染组件，给新模型网格上色;
        //foreach (Transform t in transform)                                                              //禁用掉所有子物体
        //{
        //    t.gameObject.SetActive(false);
        //}
        //transform.gameObject.SetActive(false);

        MeshRenderer[] renderers = GameManager.instance.parentGO.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer meshRenderer in renderers)
        {
            meshRenderer.enabled = false;
        }

        newGO.AddComponent<ChangeColor>().GetObjectSize();

        #endregion
    }
}