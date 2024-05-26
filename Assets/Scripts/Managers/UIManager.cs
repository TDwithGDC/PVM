using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// UI管理器
/// </summary>
public class UIManager : MonoBehaviour
{
    //选卡页面
    public List<GameObject> buildingsSelectorPages;
    private int currentPage;
    public List<GameObject> selectedBuildings;
    public List<Image> cardSlotsImages;//已选卡的图片
    public List<Image> allBuildings;//所有的建筑选卡
    public Sprite defaultSprite;//没有选择，默认图片
    public Button startButton;//开始游戏按钮
    public List<Button> cardSlotsInGame;//游戏开始后的卡槽

    //建筑提示框
    public List<GameObject> tips;//所有建筑的提示框
    public Dictionary<BuildingTypes, GameObject> tipsDict;//用于精确获取提示框
    public GameObject currentTip;//当前提示框

    private void Start()
    {
        currentPage = 1;
        //tipsDict初始化
        tipsDict = new Dictionary<BuildingTypes, GameObject>();
        for (int i = 0; i < tips.Count; i++)
        {
            tipsDict.Add((BuildingTypes)i, tips[i]);
        }
    }

    private void Update()
    {
        if (GameManager.Game.gameStage==2)
        {
            UpdateCardSlots();
            if (cardSlotsImages[cardSlotsImages.Count-1].sprite!=defaultSprite)
            {
                startButton.interactable = true;
            }
            else
            {
                startButton.interactable = false;
            }
        }
        if (GameManager.Game.gameStage==3)
        {
            if (Input.GetKeyDown(KeyCode.Escape))//取消选择
            {
                for (int i = 0; i < GameManager.Game.cardSlotsInGame.transform.childCount; i++)
                {
                    GameManager.Game.cardSlotsInGame.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
                }
                GameManager.Game.selectedBuildingToBuild = null;
            }
            if (currentTip != null)
            {
                UpdateTip();
            }
        }
    }

    #region 提示框

    /// <summary>
    /// 关闭所有提示框
    /// </summary>

    public void CloseAllTips()
    {
        for (int i = 0; i < tips.Count; i++)
        {
            tips[i].SetActive(false);
        }
    }

    /// <summary>
    /// 更新提示框信息
    /// </summary>

    public void UpdateTip()
    {
        Building building = GameManager.Game.selectedBuilding.GetComponent<Building>();
        //是否连接到主基地
        Text text = currentTip.transform.Find("IsConnectedToMainBase").GetComponent<Text>();
        text.text = building.isConnectToMainBase ? "连接到主基地" : "未连接到主基地";
        //HP
    }

    /// <summary>
    /// 运行的开关
    /// </summary>

    public void RunningButton()
    {
        Building building = GameManager.Game.selectedBuilding.GetComponent<Building>();
        if (building.running)
        {
            building.running = false;
            EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text = "未运行";
        }
        else
        {
            building.running = true;
            EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text = "正在运行";
        }
    }

    /// <summary>
    /// 切换开采矿物资源类型
    /// </summary>

    public void ChangeResourcesType()
    {
        KuangJing k = GameManager.Game.selectedBuilding.GetComponent<KuangJing>();
        Text text = currentTip.transform.Find("ChangeType").GetChild(1).GetComponent<Text>();
        if (k.type==ResourcesTypes.Iron)
        {
            text.text = "开采的资源：煤";
            k.type = ResourcesTypes.Coal;
        }
        else
        {
            text.text = "开采的资源：铁";
            k.type = ResourcesTypes.Iron;
        }
    }

    /// <summary>
    /// 销毁建筑物
    /// </summary>

    public void DestroyBuilding()
    {
        Destroy(GameManager.Game.selectedBuilding);
        CloseAllTips();
        currentTip = null;
    }

    #endregion

    #region 选卡界面

    //切换选卡页面（上下箭头，相当于鼠标滚轮滑动）

    public void PageUp()
    {
        for (int i = 0; i < buildingsSelectorPages.Count; i++)
        {
            buildingsSelectorPages[i].SetActive(false);
        }
        if (!(currentPage <= 1))
        {
            currentPage--;
        }
        buildingsSelectorPages[currentPage - 1].SetActive(true);
    }

    public void PageDown()
    {
        for (int i = 0; i < buildingsSelectorPages.Count; i++)
        {
            buildingsSelectorPages[i].SetActive(false);
        }
        if (!(currentPage >= buildingsSelectorPages.Count))
        {
            currentPage++;
        }
        buildingsSelectorPages[currentPage - 1].SetActive(true);
    }

    public void SelectBuilding()
    {
        if (cardSlotsImages[cardSlotsImages.Count-1].sprite!=defaultSprite)
        {
            //卡槽满了
            return;
        }
        GameObject currentSelectedGo = EventSystem.current.currentSelectedGameObject;
        //取消选择过的按钮交互
        currentSelectedGo.GetComponent<Button>().interactable = false;
        for (int i = 0; i < cardSlotsImages.Count; i++)
        {
            if (cardSlotsImages[i].sprite == defaultSprite)
            {
                cardSlotsImages[i].sprite = Resources.Load<Sprite>("Art/Pictures/BuildingsSelector/" + currentSelectedGo.name);
                break;
            }
        }
    }

    public void DeselectBuilding()
    {
        GameObject currentGo = EventSystem.current.currentSelectedGameObject;
        Image img = currentGo.GetComponent<Image>();
        if (img.sprite == defaultSprite)
        {
            return;
        }
        //恢复选卡按钮
        for (int i = 0; i < allBuildings.Count; i++)
        {
            if (allBuildings[i].sprite==img.sprite)
            {
                allBuildings[i].GetComponent<Button>().interactable = true;
            }
        }
        //将当前格子图片设置为默认
        img.sprite = defaultSprite;
    }
    
    /// <summary>
    /// 更新卡槽
    /// </summary>

    private void UpdateCardSlots()
    {
        for (int i = 0; i < cardSlotsImages.Count; i++)
        {
            if (i<(cardSlotsImages.Count-1))
            {
                if ((cardSlotsImages[i].sprite == defaultSprite) && (cardSlotsImages[i + 1].sprite != defaultSprite))
                {
                    cardSlotsImages[i].sprite = cardSlotsImages[i + 1].sprite;
                    cardSlotsImages[i + 1].sprite = defaultSprite;
                }
            }
        }
    }

    public void StartGame()
    {
        GameManager.Game.gameStage++;
        for (int i = 0; i < selectedBuildings.Count; i++)
        {
            GameObject building = Resources.Load<GameObject>("Prefabs/Buildings/" + cardSlotsImages[i].sprite.name);
            selectedBuildings[i] = building;
        }
        GameManager.Game.buildingsSelector.SetActive(false);
        for (int i = 0; i < GameManager.Game.cardSlotsInGame.transform.childCount; i++)
        {
            GameManager.Game.cardSlotsInGame.transform.GetChild(i).GetComponent<Image>().sprite = cardSlotsImages[i].sprite;
        }
        GameManager.Game.cardSlotsInGame.SetActive(true);
    }

    #endregion

    #region 第三阶段卡槽

    /// <summary>
    /// 按下卡槽按钮选择建筑
    /// </summary>

    public void CardSlotsButton()
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        for (int i = 0; i < GameManager.Game.cardSlotsInGame.transform.childCount; i++)
        {
            if (GameManager.Game.cardSlotsInGame.transform.GetChild(i).gameObject != go)
            {
                GameManager.Game.cardSlotsInGame.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
            }
        }
        GameManager.Game.selectedBuildingToBuild = selectedBuildings[int.Parse(EventSystem.current.currentSelectedGameObject.name) - 1];
        go.transform.GetChild(1).gameObject.SetActive(true);
    }

    #endregion
}
