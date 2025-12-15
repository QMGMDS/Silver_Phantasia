using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Image highLightImage;
    [SerializeField]private Image itemIcon;
    [SerializeField]private TextMeshProUGUI itemName;
    [SerializeField]private TextMeshProUGUI itemNum;
    [SerializeField]private TextMeshProUGUI itemDecoration;

    [Header("该物品所在的背包格子序号")]
    public int ID;

    //存储该格子的物品信息
    private ItemDetials itemDetials;
    //该格子的用途类别
    private int itemType;


    private void Awake()
    {
        highLightImage = GetComponent<Image>();
        highLightImage.enabled = false;
        if(itemDecoration != null)
        {
            itemDecoration.enabled = false;
        }
    }

    
    /// <summary>
    /// 同步物品信息
    /// </summary>
    /// <param name="item"></param>
    /// <param name="model">该物品所属类别，0为战斗技能，1为战斗物品，2为F键背包物品</param>
    public void InitItem(ItemDetials item,int model)
    {
        itemType = model;
        itemDetials = item;
        itemIcon.sprite = item.itemIcon;
        itemName.text = item.itemName;
        itemNum.text = "X" + item.itemNum.ToString();
        if(itemDecoration != null)
        {
            itemDecoration.text = item.itemDecorations;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highLightImage.enabled = true;
        if(itemDecoration != null)
        {
            itemDecoration.enabled = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highLightImage.enabled = false;
        if(itemDecoration != null)
        {
            itemDecoration.enabled = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        highLightImage.enabled = false;
        switch (itemType)
        {
            case 1: //玩家进行物品操作
                EventHandler.CallPlayerUseItem(itemDetials);
                break;
            
        }
    }
}
