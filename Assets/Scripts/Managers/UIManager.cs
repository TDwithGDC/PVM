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

    private void Start()
    {
        currentPage = 1;
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
                GameManager.Game.selectedBuilding = null;
            }
        }
    }

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
        GameManager.Game.selectedBuilding = selectedBuildings[int.Parse(EventSystem.current.currentSelectedGameObject.name) - 1];
        go.transform.GetChild(1).gameObject.SetActive(true);
    }

    #endregion
}
