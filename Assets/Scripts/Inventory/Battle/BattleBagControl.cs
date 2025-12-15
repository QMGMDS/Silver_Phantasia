using UnityEngine;

public class BattleBagControl : MonoBehaviour
{
    public ItemDataList_SO itemBag;
    public ItemDataList_SO skillBag;

    




    // 初始化物品背包
    public void OpenItemBag()
    {
        InitBag(itemBag,1);
    }




    /// <summary>
    /// 背包初始化
    /// </summary>
    /// <param name="bag">要显示的背包</param>
    /// <param name="model">0表示该背包是战斗技能背包，1表示该背包是战斗物品背包</param>
    private void InitBag(ItemDataList_SO bag,int model)
    {
        int i = 0;
        var allItem = transform.GetComponentsInChildren<Item>(true); //查找含有Item组件的子物体（包括未激活的子物体）
        foreach (var item in allItem)
        {
            item.InitItem(bag.itemDetialsList[i],model);
            i++;
        }
    }
}
