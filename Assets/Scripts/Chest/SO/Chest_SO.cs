using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chest_SO", menuName = "Plot/Chest_SO")]
public class Chest_SO : ScriptableObject
{
    public List<Chest> allChestData;


    /// <summary>
    /// 根据宝箱ID查找对应的宝箱信息，没找到返回空
    /// </summary>
    /// <param name="chestID"></param>
    /// <returns></returns>
    public Chest FromIDToFindChest(int chestID)
    {
        foreach (var chest in allChestData)
        {
            if(chestID == chest.ID)
            {
                return chest;
            }
        }
        return null;
    }
}
