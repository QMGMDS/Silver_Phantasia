using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]private int thisItemID;
    [SerializeField]private Image highlight;







    private void OnEnable()
    {
        highlight.enabled = false;
    }



    /// <summary>
    /// 鼠标进入
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        highlight.enabled = true;
    }

    /// <summary>
    /// 鼠标离开
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        highlight.enabled = false;
    }

    /// <summary>
    /// 鼠标按下
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        highlight.enabled = false;
        // 玩家选择对应物品
        BattleManager.Instance.playerChooseItemID = thisItemID;
        // 物品选择了
        EventHandler.CallBattle_ShowItemChoose();
    }
}
