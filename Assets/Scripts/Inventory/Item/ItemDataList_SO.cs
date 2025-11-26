using System.Collections.Generic;
using UnityEngine;


//游戏物品仓库
[CreateAssetMenu(fileName = "ItemDataList_SO", menuName = "Inventory/ItemDataList", order = 0)]
public class ItemDataList_SO : ScriptableObject
{
    public List<ItemDetials> itemDetialsList;
}
