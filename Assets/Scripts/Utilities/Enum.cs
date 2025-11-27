//场景类别
public enum SceneType
{
    Menu,Battle,Loaction,Dialogue
}

//战斗回合
public enum BattleState 
{
    Start, PlayerTurn, MonsterTurn, Won, Lost
}

//物品类型
public enum ItemType
{
    //攻击物品
    Attack,
    //治疗物品
    Treatment,
}

//攻击类型
// public enum AttackType
// {
//     //普攻
//     Normal,
//     //技能型攻击
//     Skill,
//     //物品型攻击
//     Item,
// }

//按键Button类型
public enum ButtonType
{
    Attack,
    Defend,
    Status,
    Skill,
    Item,
    Flee,
}