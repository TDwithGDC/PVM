using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有建筑的基类
/// </summary>

public class Building : PVM
{
    //基础信息
    public BuildingTypes buildingType;
    public int HP;
    public bool isConnectToMainBase;//是否连接到主基地
    public bool running;//是否在运行
}

/// <summary>
/// 建筑类型
/// </summary>

public enum BuildingTypes
{
    YunShuZhongXin=0,
    KuangJing=1,
    FaMuChang = 2,
    MainBase = 3,
    CaiShiChang = 4,
    HeDianZhan = 5,
    ChouShuiJi = 6,
    LianTieChang = 7,
    MuCaiJiaGongChang = 8,
    ReDianZhan = 9,
}