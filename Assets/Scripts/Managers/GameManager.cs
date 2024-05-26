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
    public GameObject selectedBuildingToBuild;//被选中的建筑
    public GameObject mainBase; // 主基地
    public GameObject buildingsSelector;//选卡UI
    public GameObject cardSlotsInGame;//第三阶段（游戏进行阶段）的卡槽

    public GameObject selectedBuilding;//已选择的建筑

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
                selectedBuildingToBuild = mainBase;
            }
        }
        else if (gameStage == 2)//选卡阶段
        {
            if (!buildingsSelector.activeSelf)
                buildingsSelector.SetActive(true);
        }
        else if (gameStage == 3)//开始游戏
        {
            BuildingTip();
        }
        PlaceBuilding();
    }

    /// <summary>
    /// 建造
    /// </summary>

    private void PlaceBuilding()
    {
        if (selectedBuildingToBuild)
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
                            GameObject mainBase = Instantiate(selectedBuildingToBuild, hit.collider.transform.GetChild(0).transform.position, hit.collider.transform.GetChild(0).transform.rotation);
                            mainBase.transform.SetParent(hit.collider.transform.GetChild(0).transform);
                            selectedBuildingToBuild = null;
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
                            GameObject go = Instantiate(selectedBuildingToBuild, hit.collider.transform.GetChild(0).transform.position, hit.collider.transform.GetChild(0).transform.rotation);
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

    /// <summary>
    /// 玩家可以点击建筑物查看属性，更改属性
    /// </summary>

    private void BuildingTip()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())//左键
        {
            RaycastHit hit = RaycastFromMousePosition();
            if (hit.collider)
            {
                if (hit.collider.gameObject.layer==9)//点击了建筑
                {
                    selectedBuildingToBuild = null;
                    selectedBuilding = hit.collider.gameObject;
                    //关掉所有的提示框，显示当前建筑物的提示框
                    uiManager.CloseAllTips();
                    Building building = hit.collider.GetComponent<Building>();
                    GameObject tipGo = uiManager.tipsDict[building.buildingType];
                    tipGo.SetActive(true);
                    tipGo.transform.position = Input.mousePosition;
                    uiManager.currentTip = tipGo;
                }
                else if (selectedBuildingToBuild == null)
                {
                    uiManager.CloseAllTips();
                    selectedBuilding = null;
                    for (int i = 0; i < uiManager.cardSlotsInGame.Count; i++)
                    {
                        uiManager.cardSlotsInGame[i].transform.GetChild(1).gameObject.SetActive(false);
                    }
                    uiManager.currentTip = null;
                }
            }
        }
    }
}
