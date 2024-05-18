using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有建筑的基类
/// </summary>

public class Building : PVM
{
    //基础信息
    public int HP;
    public bool isConnectToMainBase;//是否连接到主基地
    public bool running;//是否在运行
}
