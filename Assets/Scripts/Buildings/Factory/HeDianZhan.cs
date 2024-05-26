using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 核电站 2024.5.26 GDC
/// </summary>

public class HeDianZhan : Factory
{
    protected override void Machining()
    {
        GameManager.Game.resourcesManager.power += Time.deltaTime * efficiency;
    }
}
