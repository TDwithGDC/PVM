using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 只能开采煤铁
/// </summary>

public class KuangJing : Miner
{
    public float ironEfficiency;
    public float coalEfficiency;

    protected override void Run()
    {
        if (running && isConnectToMainBase && (selectedPoint != null))
        {
            if (type == ResourcesTypes.Iron)
            {
                float resourcesPointReserves = selectedPoint.reserves;
                float res = ironEfficiency * Time.deltaTime;
                float actual = Mathf.Min(res, resourcesPointReserves);
                selectedPoint.reserves -= actual;
                GameManager.Game.resourcesManager.iron += actual;
            }
            else if (type == ResourcesTypes.Coal)
            {
                float resourcesPointReserves = selectedPoint.reserves;
                float res = coalEfficiency * Time.deltaTime;
                float actual = Mathf.Min(res, resourcesPointReserves);
                selectedPoint.reserves -= actual;
                GameManager.Game.resourcesManager.coal += actual;
            }
        }
    }
}
