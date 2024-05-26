using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 热电站 2024.5.25 GDC
/// </summary>

public class ReDianZhan : Factory
{
    public ResourcesTypes fuelType;//燃料类型(log,coal)
    public float coalEfficiency;//烧煤效率
    public float logEfficiency;//烧木头效率

    protected override void Machining()
    {
        if (fuelType==ResourcesTypes.Coal)
        {
            if (GameManager.Game.resourcesManager.coal > 0)
            {
                float num = Time.deltaTime;
                float actual = Mathf.Min(num, GameManager.Game.resourcesManager.coal);
                GameManager.Game.resourcesManager.coal -= actual;
                GameManager.Game.resourcesManager.power += actual * coalEfficiency;
            }
        }
        if (fuelType==ResourcesTypes.Log)
        {
            if (GameManager.Game.resourcesManager.log > 0)
            {
                float num = Time.deltaTime;
                float actual = Mathf.Min(num, GameManager.Game.resourcesManager.log);
                GameManager.Game.resourcesManager.log -= actual;
                GameManager.Game.resourcesManager.power += actual * logEfficiency;
            }
        }
    }
}
