using System.Drawing.Printing;
using UnityEngine;

public class BagControl : MonoBehaviour
{
    // 该背包所链接的物品仓库
    public ItemDataList_SO bagInventory;



    private void OnEnable()
    {
        EventHandler.ChestOpen += OnChestOpen;

        UpdateBag();
    }

    private void OnDisable()
    {
        EventHandler.ChestOpen -= OnChestOpen;
    }

    /// <summary>
    /// 宝箱打开，获得宝藏
    /// </summary>
    /// <param name="inChest_Truesure"></param>
    private void OnChestOpen(Treasure inChest_Truesure)
    {
        // 玩家背包内对应物品增加
        bagInventory.fromIDToFindItem(inChest_Truesure.treasureID).itemNum += inChest_Truesure.num;
        // 更新物品信息
        UpdateBag();
    }


    /// <summary>
    /// 更新背包内的所有物品信息
    /// </summary>
    private void UpdateBag()
    {
        int i = 0;
        var allItem = transform.GetComponentsInChildren<Item>(true); //查找含有Item组件的子物体（包括未激活的子物体）
        foreach (var item in allItem)
        {
            item.InitItem(bagInventory.itemDetialsList[i]);
            i++;
        }
    }
}
