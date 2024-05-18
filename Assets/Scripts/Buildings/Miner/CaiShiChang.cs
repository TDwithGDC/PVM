using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaiShiChang : Miner
{
    protected override void Run()
    {
        if (running && isConnectToMainBase && (selectedPoint != null))
        {
            float resourcesPointReserves = selectedPoint.reserves;
            float res = efficiency * Time.deltaTime;
            float actual = Mathf.Min(res, resourcesPointReserves);
            selectedPoint.reserves -= actual;
            GameManager.Game.resourcesManager.stone += actual;
        }
    }
}
