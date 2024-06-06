using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人基类
/// </summary>

public class Enemy : PVM
{
    //基础信息
    public float HP;
    public int level;//强度（五个等级）
    public float speed;
    public float damage;
}
