using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 实现钢的获取 2024.5.24 GDC
/// </summary>

public class LianTieChang : Factory
{
    protected override void Machining()
    {
        if (GameManager.Game.resourcesManager.iron > 0)
        {
            float num = Time.deltaTime;
            float actual = Mathf.Min(num, GameManager.Game.resourcesManager.iron);
            GameManager.Game.resourcesManager.iron -= actual;
            GameManager.Game.resourcesManager.steel += actual * efficiency;
        }
    }
}
