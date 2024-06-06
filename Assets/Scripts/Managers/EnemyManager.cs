using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人管理员,负责生成敌人和管理敌人
/// </summary>

public class EnemyManager : MonoBehaviour
{
    public int hardLevel;//出怪强度(1简单,2普通,3困难,4极难,5传奇)
    public List<Enemy> enemyType;//当前关卡的出怪类型

    public List<Enemy> currentEnemyType;//当前波次的出怪类型
    public int totalWave;//总波次
    public int currentWave;//波次
    private float chance;//出怪几率
    private float count;//出怪量
    private int currentLevel;//当前出怪强度

    private void Awake()
    {
        //计算一些初始值
        totalWave = hardLevel * 10;
    }
}
