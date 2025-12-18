using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleSkill_SO", menuName = "Inventory/BattleSkillDetialsList", order = 0)]
public class BattleSkill_SO : ScriptableObject
{
    public List<SkillDetails> battleSkillList;
}
