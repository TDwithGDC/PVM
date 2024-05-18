using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Game
    {
        private set;
        get;
    }

    public UIManager uiManager; // UI管理器
    public GameResourcesManager resourcesManager; // 游戏资源管理器

    public GameObject map;//游戏地图
    public int gameStage;//游戏阶段
    public bool mainBasePlaced;//主基地是否放置
    public GameObject selectedBuilding;//被选中的建筑
    public GameObject mainBase; // 主基地
    public GameObject buildingsSelector;//选卡UI
    public GameObject cardSlotsInGame;//第三阶段（游戏进行阶段）的卡槽

    private void Awake()
    {
        Game = this;
        gameStage = 1;
        Instantiate(map, Vector2.zero, Quaternion.Euler(new Vector3(-90,0,0)));//生成地图
    }

    private void Update()
    {
        if (gameStage==1)
        {
            if (!mainBasePlaced)
            {
                selectedBuilding = mainBase;
            }
        }
        else if (gameStage == 2)//选卡阶段
        {
            if (!buildingsSelector.activeSelf)
                buildingsSelector.SetActive(true);
        }
        // *预留*
        // else if (gameStage==3)//开始游戏
        // {
        //     
        // }
        PlaceBuilding();
    }

    /// <summary>
    /// 建造
    /// </summary>

    private void PlaceBuilding()
    {
        if (selectedBuilding)
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (gameStage == 1)//第一阶段
                {
                    RaycastHit hit = RaycastFromMousePosition();
                    if (hit.collider)
                    {
                        if (hit.collider.gameObject.layer == 8)
                        {
                            GameObject mainBase = Instantiate(selectedBuilding, hit.collider.transform.GetChild(0).transform.position, hit.collider.transform.GetChild(0).transform.rotation);
                            mainBase.transform.SetParent(hit.collider.transform.GetChild(0).transform);
                            selectedBuilding = null;
                            gameStage++;
                            mainBasePlaced = true;
                        }
                    }
                    return;
                }
                if (gameStage==3)//开始游戏
                {
                    RaycastHit hit = RaycastFromMousePosition();
                    if (hit.collider)
                    {
                        if (hit.collider.gameObject.layer == 8)
                        {
                            GameObject go = Instantiate(selectedBuilding, hit.collider.transform.GetChild(0).transform.position, hit.collider.transform.GetChild(0).transform.rotation);
                            go.transform.SetParent(hit.collider.transform.GetChild(0).transform);
                            Debug.Log("放置了" + go.name);
                        }
                    }
                    return;
                }
            }
        }
    }

    /// <summary>
    /// 从鼠标位置发射射线
    /// </summary>

    private RaycastHit RaycastFromMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity);
        return hit;
    }
}
