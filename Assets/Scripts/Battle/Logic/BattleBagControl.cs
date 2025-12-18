using UnityEngine;

public class BattleBagControl : MonoBehaviour
{
    // 物品背包
    public ItemDataList_SO itemBag;
    




    // 初始化物品背包
    public void OpenItemBag()
    {
        BattleManager.Instance.currentChooseAction = ChooseAction.Item;
        InitItemBag(itemBag);
    }

    /// <summary>
    /// 物品背包初始化
    /// </summary>
    /// <param name="bag">要显示的背包</param>
    /// <param name="model">1表示该背包是战斗物品背包</param>
    private void InitItemBag(ItemDataList_SO bag)
    {
        int i = 0;
        var allItem = transform.GetComponentsInChildren<Item>(true); //查找含有Item组件的子物体（包括未激活的子物体）
        foreach (var item in allItem)
        {
            item.InitItem(bag.itemDetialsList[i]);
            i++;
        }
    }

}
