using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 资源点(煤铁水木石头......)
/// </summary>

public class ResourcesPoint : MonoBehaviour
{
    public ResourcesTypes resourcesType;//资源类型
    public float reserves;//储量(kg)

    private void Update()
    {
        if (reserves <= 0)
        {
            Destroy(gameObject);
        }
    }
}

public enum ResourcesTypes
{
    Iron,
    Stone,
    Coal,
    Water,
    Log
}