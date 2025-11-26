using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleAttributeDataList_SO", menuName = "Inventory/BattleAttributeDataList", order = 0)]
public class BattleAttributeDataList_SO : ScriptableObject
{
    public List<BattleAttribute> AttributesList;
}