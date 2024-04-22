using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class daohang : MonoBehaviour
{
    /// <summary>
    /// 用一个枚举来 设置物体需要修改的offset值
    /// </summary>
    public enum Axis
    {
        X,
        y,
        None
    }
    public Axis OffsetAxis = Axis.None;//修改的偏移坐标轴
    private MeshRenderer mesh;//渲染器组件
    public float Speed = 0.5f;//轮播速度
    public float OffMax = 2.0f;//设定的offset最大值
    public float OffMin = 1.0f;//设定的offset最小值
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();//获取物体身上的MeshRenderer组件
    }
    void Update()
    {
        ChangeOffset();
    }
    Vector2 offset;//用来缓存offset变化值
    /// <summary>
    /// 通过三目运算控制offset值在设定的最大最小值之间不断变化，以实现贴图滚动效果
    /// </summary>
    private void ChangeOffset()
    {
        switch (OffsetAxis)
        {
            case Axis.X:
                offset = new Vector2(mesh.material.mainTextureOffset.x > OffMax ? OffMin : mesh.material.mainTextureOffset.x + Time.deltaTime * Speed, 0);
                break;
            case Axis.y:
                offset = new Vector2(0, mesh.material.mainTextureOffset.y > OffMax ? OffMin : mesh.material.mainTextureOffset.y + Time.deltaTime * Speed);
                break;

        }
        mesh.material.mainTextureOffset = offset;//实时修改渲染器offset偏移值，实现材质球贴图滚动
    }

}
