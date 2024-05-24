using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuCaiJiaGongChang : Factory
{
    protected override void Machining()
    {
        if (GameManager.Game.resourcesManager.log > 0)
        {
            float num = Time.deltaTime;
            float actual = Mathf.Min(num, GameManager.Game.resourcesManager.log);
            GameManager.Game.resourcesManager.log -= actual;
            GameManager.Game.resourcesManager.wood += actual * efficiency;
        }
    }
}
