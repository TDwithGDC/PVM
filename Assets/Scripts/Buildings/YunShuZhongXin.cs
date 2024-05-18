using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YunShuZhongXin : Building
{
    public Collider[] connectedBuildings;//连接到的建筑
    public List<LineRenderer> lines;

    public float range;//范围

    public float checkTime;//每隔多少时间更新一次连接
    private float checkTimer;

    private void Update()
    {
        checkTimer += Time.deltaTime;
        if (checkTimer>=checkTime)
        {
            checkTimer = 0;
            UpdateConnection();
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    /// <summary>
    /// 更新连接
    /// </summary>

    private void UpdateConnection()
    {
        Debug.Log("更新一次");
        for (int i = 0; i < lines.Count; i++)
        {
            lines[i].gameObject.SetActive(false);
        }
        Collider[] colliders = Physics.OverlapSphere(transform.position, range,1<<9);
        if (colliders.Length>0)
        {
            Debug.Log("有建筑");
            connectedBuildings = colliders;
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.name== "MainBase(Clone)")
                {
                    isConnectToMainBase = true;
                }
                lines[i].gameObject.SetActive(true);
                lines[i].positionCount = 2;
                lines[i].SetPositions(new Vector3[2] { transform.position, colliders[i].transform.position });
            }
        }
        for (int i = 0; i < connectedBuildings.Length; i++)
        {
            if ((connectedBuildings[i]) && (connectedBuildings[i].gameObject.name!= "MainBase(Clone)"))
            {
                connectedBuildings[i].gameObject.GetComponent<Building>().isConnectToMainBase = isConnectToMainBase;
            }
        }
    }

    private void OnDestroy()
    {
        isConnectToMainBase = false;
        for (int i = 0; i < connectedBuildings.Length; i++)
        {
            if ((connectedBuildings[i]) && (connectedBuildings[i].gameObject.name != "MainBase(Clone)"))
            {
                connectedBuildings[i].gameObject.GetComponent<Building>().isConnectToMainBase = false;
            }
        }
    }
}
