using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有加工厂的基类 2024.5.24 GDC
/// </summary>

public class Factory : Building
{
    public float efficiency;//效率（每秒多少千克）

    protected override void Update()
    {
        base.Update();
        if (running && isConnectToMainBase)
        {
            Machining();
        }
    }

    /// <summary>
    /// 加工
    /// </summary>

    protected virtual void Machining()
    {
        
    }
}
