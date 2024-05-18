using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 开采资源的基类
/// </summary>

public class Miner : Building
{
    public ResourcesTypes type;//该矿井需要开采的类型
    public ResourcesPoint selectedPoint;//选择好的资源点（开采时将要开采这个）
    public float efficiency;//效率
    public float findRadius;//寻找半径

    public float findTime;//查找时间
    protected float findTimer;

    protected virtual void Update()
    {
        findTimer += Time.deltaTime;
        if (findTimer >= findTime)
        {
            findTimer = 0;
            FindResourcesPoint();
        }
        Run();
    }

    /// <summary>
    /// 寻找附近的资源点
    /// </summary>

    protected virtual void FindResourcesPoint()
    {
        Collider[] points = Physics.OverlapSphere(transform.position, findRadius, 1 << 10);
        if (points.Length > 0)//有资源
        {
            for (int i = 0; i < points.Length; i++)
            {
                ResourcesPoint t = points[i].GetComponent<ResourcesPoint>();
                if (t.resourcesType == type)
                {
                    selectedPoint = t;
                    break;
                }
                else
                {
                    selectedPoint = null;
                }
            }
        }
        else
        {
            selectedPoint = null;
        }
    }

    /// <summary>
    /// 运行
    /// </summary>

    protected virtual void Run()
    {
        
    }
}
