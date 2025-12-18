using System.Collections.Generic;
using UnityEngine;


//游戏物品仓库
[CreateAssetMenu(fileName = "ItemDataList_SO", menuName = "Inventory/ItemDataList", order = 0)]
public class ItemDataList_SO : ScriptableObject
{
    public List<ItemDetials> itemDetialsList;



    /// <summary>
    /// 根据物品ID查找对应的物品信息，没找到返回空
    /// </summary>
    /// <param name="itemID"></param>
    /// <returns></returns>
    public ItemDetials fromIDToFindItem(int itemID)
    {
        foreach (var item in itemDetialsList)
        {
            if(itemID == item.itemID)
            {
                return item;
            }
        }
        return null;
    }
}
