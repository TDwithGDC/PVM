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
    public List<Transform> spawnPoints;//生成点
    public int totalWave;//总波次
    public int currentWave;//当前波次
    private float chance;//出怪几率(0.0%~100.0%)
    private float count;//出怪量
    private int currentLevel;//当前出怪强度

    private void Awake()
    {
        //计算一些初始值
        totalWave = hardLevel * 10;
        currentWave = 10;
        currentLevel = 1;
        chance = 1;//0.1%
    }

    /// <summary>
    /// 出怪方法
    /// </summary>

    public void NextWave()
    {
        if (currentWave>totalWave)
        {
            //怪出完了
            return;
        }
        Debug.Log("下一波");
        //判断是否能够出怪
        if (CanWave())
        {
            count = currentWave * hardLevel * 3;//数量
            currentEnemyType.Clear();//清空上一波的敌人种类
            //添加当前波次的敌人种类
            for (int i = 0; i < (int)(enemyType.Count * Mathf.Min(currentWave / 10, 1)); i++)
            {
                Enemy e = enemyType[Random.Range(0, enemyType.Count)];
                if (e.level<=currentLevel)
                {
                    if (!currentEnemyType.Exists(t => t == e))
                    {
                        currentEnemyType.Add(e);
                    }
                }
            }
            //生成
            if (currentEnemyType.Count>0)
            {
                for (int i = 0; i < currentEnemyType.Count; i++)
                {
                    for (int a = 0; a < currentEnemyType[i].level*count; a++)
                    {
                        Vector3 spawnPos = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
                        Instantiate(currentEnemyType[i].gameObject, spawnPos, Quaternion.identity);
                    }
                }
            }
        }
        currentWave++;
        //出怪强度
        currentLevel = currentWave / 5;
        //出怪几率
        if (chance<1000)
        {
            chance *= (float)System.Math.Pow(2, currentWave) * hardLevel;
        }
    }

    /// <summary>
    /// 是否能出怪
    /// </summary>

    private bool CanWave()
    {
        int n = Random.Range(1, 1001);
        if (n<=chance)
        {
            return true;
        }
        return false;
    }
}
