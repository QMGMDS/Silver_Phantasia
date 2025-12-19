using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class ChestControl : MonoBehaviour
{
    [Header("该宝箱的ID")]
    public int thisChestID;
    [Header("宝箱数据库")]
    public Chest_SO chest_SO;
    [Header("宝箱打开的图片")]
    public Sprite openSprite;

    private Animator chestAnim;
    private SpriteRenderer sprite;
    // 该宝箱是否可交互
    private bool canInteract;
    // 该宝箱是否被打开
    private bool thisChestOpened;

    private void Awake()
    {
        chestAnim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        EventHandler.InteractButtonStartEvent += OnChestOpenEvent;


        thisChestOpened = chest_SO.FromIDToFindChest(thisChestID).isOpen;
        if(thisChestOpened)
            ChestAlwaysOpen();
    }

    private void OnDisable()
    {
        EventHandler.InteractButtonStartEvent -= OnChestOpenEvent;
    }


    /// <summary>
    /// 空格键按下
    /// </summary>
    private void OnChestOpenEvent()
    {
        if (canInteract)
        {
            ChestOpen();
        }
    }

    /// <summary>
    /// 宝箱打开！
    /// </summary>
    private void ChestOpen()
    {
        // 宝箱内的物品（宝藏）传给玩家背包
        EventHandler.CallChestOpen(chest_SO.FromIDToFindChest(thisChestID).inChest_Truesure);


        canInteract = false;
        thisChestOpened = true;
        chestAnim.SetTrigger("Open");
        chestAnim.SetBool("IsOpen",true);
        EventHandler.CallPlayerSign(false);
        chest_SO.FromIDToFindChest(thisChestID).isOpen = true;
    }

    /// <summary>
    /// 宝箱恒打开
    /// </summary>
    private void ChestAlwaysOpen()
    {
        chestAnim.enabled = false;
        sprite.sprite = openSprite;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(thisChestOpened == false) 
        {
            canInteract = true;
            EventHandler.CallPlayerSign(true); // 宝箱没被打开才会显示提示Sign
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        canInteract = false;
        EventHandler.CallPlayerSign(false);
    }


    
    
}
