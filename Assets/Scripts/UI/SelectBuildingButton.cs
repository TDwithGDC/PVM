using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 挂载在选择建筑的按钮上面
/// </summary>

public class SelectBuildingButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public BuildingDepletion buildingDepletion;

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.Game.uiManager.buildingDepletionTip.SetActive(true);
        GameManager.Game.uiManager.buildingDepletionTip.transform.position = Input.mousePosition;
        GameManager.Game.uiManager.buildingDepletionTip.transform.GetChild(1).GetComponent<Text>().text =
            buildingDepletion.depletion[0].ToString() + "钢\n" +
            buildingDepletion.depletion[1].ToString() + "木材\n" +
            buildingDepletion.depletion[2].ToString() + "石头\n" +
            buildingDepletion.depletion[3].ToString() + "元";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.Game.uiManager.buildingDepletionTip.SetActive(false);
    }
}
