using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 农业建筑的基类
/// </summary>

public class Farming : Building
{
    protected float distance;//离水源的距离
    public float speed;//当天生长速度
    public float waterRadius;//灌溉半径（distance最大值）
    public float growProgress;//生长进度
    public float efficiency;

    protected float updateTimer;
    protected float growTimer;

    protected void Start()
    {
        growProgress = 0;
        updateTimer = 100;
        distance = 0.5f;
    }

    protected override void Update()
    {
        base.Update();
        updateTimer += Time.deltaTime;
        growTimer += Time.deltaTime;
        if (updateTimer >=10)
        {
            updateTimer = 0;
            //更新一次距离
            UpdateDistance();
        }
        if (growTimer>=1)
        {
            growTimer = 0;
            //生长一次
            GrowUp();
        }
    }

    /// <summary>
    /// 更新离水源的距离
    /// </summary>

    protected void UpdateDistance()
    {
        Debug.Log(1);
        Collider[] points = Physics.OverlapSphere(transform.position, waterRadius, 1 << 10);
        if (points.Length>0)
        {
            float dis = 0.5f;
            for (int i = 0; i < points.Length; i++)
            {
                if (points[i].GetComponent<ResourcesPoint>().resourcesType == ResourcesTypes.Water)
                {
                    distance = Mathf.Min(dis, Vector3.Distance(transform.position, points[i].transform.position));
                    dis = distance;
                }
            }
        }
    }

    /// <summary>
    /// 作物生长
    /// </summary>

    protected virtual void GrowUp()
    {
        if (GameManager.Game.weatherManager.temperature+GameManager.Game.weatherManager.water<0)
        {
            speed = 0;
        }
        else
        {
            speed = Mathf.Sqrt((GameManager.Game.weatherManager.temperature + GameManager.Game.weatherManager.water) * distance) / (300 * distance);
        }
        growProgress += speed;
        if (growProgress>=1)
        {
            growProgress = 0;
            //成熟一次
            GameManager.Game.resourcesManager.food += efficiency;
        }
    }
}
