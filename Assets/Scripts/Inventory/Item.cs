using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]private Image itemIcon;
    [SerializeField]private TextMeshProUGUI itemName;
    [SerializeField]private TextMeshProUGUI itemNum;

    [Header("该格子存储的物品ID")]
    [SerializeField]private int thisPlot_ItemID;



    
    /// <summary>
    /// 同步物品信息
    /// </summary>
    /// <param name="item"></param>
    public void InitItem(ItemDetials item)
    {
        thisPlot_ItemID = item.itemID;
        // 显示
        itemIcon.sprite = item.itemIcon;
        itemName.text = item.itemName;
        itemNum.text = "X" + item.itemNum.ToString();
    }




    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

}
