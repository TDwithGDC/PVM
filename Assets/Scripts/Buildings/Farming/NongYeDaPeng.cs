using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NongYeDaPeng : Farming
{
    protected override void GrowUp()
    {
        speed = (((float)System.Math.Pow(GameManager.Game.weatherManager.temperature,2) + GameManager.Game.weatherManager.water) * distance) / (300 * distance);
        growProgress += speed;
        if (growProgress >= 1)
        {
            growProgress = 0;
            //成熟一次
            GameManager.Game.resourcesManager.food += efficiency;
        }
    }
}
