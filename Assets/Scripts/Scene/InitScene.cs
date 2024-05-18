using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitScene : MonoBehaviour
{
    public GameObject backGround;
    private Vector2 startPosition;//第一个（左上角方块坐标）
    public GameObject sprite;//背景方块
    public int weight;//长
    public int height;//宽

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        startPosition = backGround.transform.position;
        for (int i = 0; i < weight; i++)
        {
            for (int a = 0; a < height; a++)
            {
                Vector2 pos = new Vector2(startPosition.x + (i * 0.16f), startPosition.y + (a * 0.16f));
                GameObject go = Instantiate(sprite, pos, Quaternion.identity);
                go.transform.SetParent(backGround.transform);
            }
        }
    }
}
