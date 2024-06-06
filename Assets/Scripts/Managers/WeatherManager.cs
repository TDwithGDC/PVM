using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherManager : MonoBehaviour
{
    public WeatherType weatherType;
    public int year;
    public int month;
    public int day;
    public float temperature;//温度
    public float water;//降水

    public Dictionary<WeatherType, Dictionary<int, Dictionary<int, float>>> weatherDict = new Dictionary<WeatherType, Dictionary<int, Dictionary<int, float>>>();

    public Text timeText;

    private float timer;

    private void Awake()
    {
        //初始化所有气候类型的降水与温度
        weatherDict = new Dictionary<WeatherType, Dictionary<int, Dictionary<int, float>>>
        {
            //温带季风气候
            {WeatherType.WenDaiJiFeng,new Dictionary<int, Dictionary<int, float>>
            {
                //温度
                {1,new Dictionary<int, float>{{1,-2 },{2,0 },{3,5 },{4,14 },{5,17 },{6,26 },{7,28 },{8,26 },{9,16 },{10,14 },{11,5 },{12,-1 }} },
                //降水
                {2,new Dictionary<int, float>{{1,0 },{2,0.36f },{3,0.67f },{4,1.33f },{5,1.67f },{6,2.5f },{7,6.67f },{8,8 },{9,2.33f },{10,1.33f },{11,0.67f },{12,0.07f }} },
            }},
            //热带季风
            {WeatherType.ReDaiJiFeng,new Dictionary<int, Dictionary<int, float>>
            {
                //温度
                {1,new Dictionary<int, float>{{1,-2 },{2,0 },{3,5 },{4,14 },{5,17 },{6,26 },{7,28 },{8,26 },{9,16 },{10,14 },{11,5 },{12,-1 }} },
                //降水
                {2,new Dictionary<int, float>{{1,0 },{2,0.36f },{3,0.67f },{4,1.33f },{5,1.67f },{6,2.5f },{7,6.67f },{8,8 },{9,2.33f },{10,1.33f },{11,0.67f },{12,0.07f }} },
            }},
            //亚热带季风和亚热带湿润
            {WeatherType.YaReDaiJiFengHeYaReDaiShiRun,new Dictionary<int, Dictionary<int, float>>
            {
                //温度
                {1,new Dictionary<int, float>{{1,-2 },{2,0 },{3,5 },{4,14 },{5,17 },{6,26 },{7,28 },{8,26 },{9,16 },{10,14 },{11,5 },{12,-1 }} },
                //降水
                {2,new Dictionary<int, float>{{1,0 },{2,0.36f },{3,0.67f },{4,1.33f },{5,1.67f },{6,2.5f },{7,6.67f },{8,8 },{9,2.33f },{10,1.33f },{11,0.67f },{12,0.07f }} },
            }},
            //热带沙漠
            {WeatherType.ReDaiShaMo,new Dictionary<int, Dictionary<int, float>>
            {
                //温度
                {1,new Dictionary<int, float>{{1,-2 },{2,0 },{3,5 },{4,14 },{5,17 },{6,26 },{7,28 },{8,26 },{9,16 },{10,14 },{11,5 },{12,-1 }} },
                //降水
                {2,new Dictionary<int, float>{{1,0 },{2,0.36f },{3,0.67f },{4,1.33f },{5,1.67f },{6,2.5f },{7,6.67f },{8,8 },{9,2.33f },{10,1.33f },{11,0.67f },{12,0.07f }} },
            }},
            //热带草原
            {WeatherType.ReDaiCaoYuan,new Dictionary<int, Dictionary<int, float>>
            {
                //温度
                {1,new Dictionary<int, float>{{1,-2 },{2,0 },{3,5 },{4,14 },{5,17 },{6,26 },{7,28 },{8,26 },{9,16 },{10,14 },{11,5 },{12,-1 }} },
                //降水
                {2,new Dictionary<int, float>{{1,0 },{2,0.36f },{3,0.67f },{4,1.33f },{5,1.67f },{6,2.5f },{7,6.67f },{8,8 },{9,2.33f },{10,1.33f },{11,0.67f },{12,0.07f }} },
            }},
            //热带雨林
            {WeatherType.ReDaiYuLin,new Dictionary<int, Dictionary<int, float>>
            {
                //温度
                {1,new Dictionary<int, float>{{1,-2 },{2,0 },{3,5 },{4,14 },{5,17 },{6,26 },{7,28 },{8,26 },{9,16 },{10,14 },{11,5 },{12,-1 }} },
                //降水
                {2,new Dictionary<int, float>{{1,0 },{2,0.36f },{3,0.67f },{4,1.33f },{5,1.67f },{6,2.5f },{7,6.67f },{8,8 },{9,2.33f },{10,1.33f },{11,0.67f },{12,0.07f }} },
            }},
            //温带大陆
            {WeatherType.WenDaiDaLu,new Dictionary<int, Dictionary<int, float>>
            {
                //温度
                {1,new Dictionary<int, float>{{1,-2 },{2,0 },{3,5 },{4,14 },{5,17 },{6,26 },{7,28 },{8,26 },{9,16 },{10,14 },{11,5 },{12,-1 }} },
                //降水
                {2,new Dictionary<int, float>{{1,0 },{2,0.36f },{3,0.67f },{4,1.33f },{5,1.67f },{6,2.5f },{7,6.67f },{8,8 },{9,2.33f },{10,1.33f },{11,0.67f },{12,0.07f }} },
            }},
            //寒带
            {WeatherType.HanDai,new Dictionary<int, Dictionary<int, float>>
            {
                //温度
                {1,new Dictionary<int, float>{{1,-2 },{2,0 },{3,5 },{4,14 },{5,17 },{6,26 },{7,28 },{8,26 },{9,16 },{10,14 },{11,5 },{12,-1 }} },
                //降水
                {2,new Dictionary<int, float>{{1,0 },{2,0.36f },{3,0.67f },{4,1.33f },{5,1.67f },{6,2.5f },{7,6.67f },{8,8 },{9,2.33f },{10,1.33f },{11,0.67f },{12,0.07f }} },
            }},
            //地中海
            {WeatherType.DiZhongHai,new Dictionary<int, Dictionary<int, float>>
            {
                //温度
                {1,new Dictionary<int, float>{{1,-2 },{2,0 },{3,5 },{4,14 },{5,17 },{6,26 },{7,28 },{8,26 },{9,16 },{10,14 },{11,5 },{12,-1 }} },
                //降水
                {2,new Dictionary<int, float>{{1,0 },{2,0.36f },{3,0.67f },{4,1.33f },{5,1.67f },{6,2.5f },{7,6.67f },{8,8 },{9,2.33f },{10,1.33f },{11,0.67f },{12,0.07f }} },
            }},
            //温带海洋
            {WeatherType.WenDaiHaiYang,new Dictionary<int, Dictionary<int, float>>
            {
                //温度
                {1,new Dictionary<int, float>{{1,-2 },{2,0 },{3,5 },{4,14 },{5,17 },{6,26 },{7,28 },{8,26 },{9,16 },{10,14 },{11,5 },{12,-1 }} },
                //降水
                {2,new Dictionary<int, float>{{1,0 },{2,0.36f },{3,0.67f },{4,1.33f },{5,1.67f },{6,2.5f },{7,6.67f },{8,8 },{9,2.33f },{10,1.33f },{11,0.67f },{12,0.07f }} },
            }},
            //高原山地
            {WeatherType.GaoYuanShanDi,new Dictionary<int, Dictionary<int, float>>
            {
                //温度
                {1,new Dictionary<int, float>{{1,-2 },{2,0 },{3,5 },{4,14 },{5,17 },{6,26 },{7,28 },{8,26 },{9,16 },{10,14 },{11,5 },{12,-1 }} },
                //降水
                {2,new Dictionary<int, float>{{1,0 },{2,0.36f },{3,0.67f },{4,1.33f },{5,1.67f },{6,2.5f },{7,6.67f },{8,8 },{9,2.33f },{10,1.33f },{11,0.67f },{12,0.07f }} },
            }},
        };
        GameManager.Game.enemyManager.NextWave();
    }

    private void Update()
    {
        if (GameManager.Game.gameStage==3)
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                timer = 0;
                day++;
                //更改当天的降水与温度
                temperature = weatherDict[weatherType][1][month];
                water = weatherDict[weatherType][2][month];
                //判断
                if (day > 28)
                {
                    switch (month)
                    {
                        //最特殊
                        case 2:
                            //闰年
                            if (((year % 4) == 0) || ((year % 400) == 0))
                            {
                                if (day > 29)
                                {
                                    day = 1;
                                    month++;
                                    GameManager.Game.enemyManager.NextWave();
                                }
                            }
                            else
                            {
                                day = 1;
                                month++;
                                GameManager.Game.enemyManager.NextWave();
                            }
                            break;
                        //30天
                        case 4:
                        case 6:
                        case 9:
                        case 11:
                            if (day > 30)
                            {
                                day = 1;
                                month++;
                                GameManager.Game.enemyManager.NextWave();
                            }
                            break;
                        //31天
                        case 1:
                        case 3:
                        case 5:
                        case 7:
                        case 8:
                        case 10:
                            if (day > 31)
                            {
                                day = 1;
                                month++;
                                GameManager.Game.enemyManager.NextWave();
                            }
                            break;
                        //下一年
                        case 12:
                            if (day > 31)
                            {
                                day = 1;
                                month = 1;
                                year++;
                                GameManager.Game.enemyManager.NextWave();
                            }
                            break;
                        default:
                            break;
                    }
                }
                timeText.text = year.ToString() + "年" + month.ToString() + "月" + day + "日";
            }
        }
    }
}

/// <summary>
/// 天气类型
/// </summary>

public enum WeatherType
{
    WenDaiJiFeng,
    ReDaiJiFeng,
    YaReDaiJiFengHeYaReDaiShiRun,
    ReDaiShaMo,
    ReDaiCaoYuan,
    ReDaiYuLin,
    WenDaiDaLu,
    HanDai,
    DiZhongHai,
    WenDaiHaiYang,
    GaoYuanShanDi
}
