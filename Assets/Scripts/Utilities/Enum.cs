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
    // +攻击
    Attack,
    // +治疗
    Treatment,
    // +速度
    Speed,
}

//buff种类
public enum BuffType
{
    Treatment,
    Speed,
}

//技能种类
public enum SkillType
{
    Treatment,
    Attack,
    
}

// 存储容器的类别
public enum BagType
{
    Item,
    skill,
}

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

//现在是什么回合？
public enum Turn
{
    None,
    Player,
    Enemy,
    End,
}

// 玩家选择的操作
public enum ChooseAction
{
    None,
    Attack,
    Defend,
    Status,
    Skill,
    Item,
    Flee,
}

//Grid地图的类型
public enum GridType
{
    //NPC障碍物地图
    NPCObstacle,
    //不允许走的地方的地图
    NotAllowWalk,
    //传送点的地图
    Transition
}

// 勇者与国王的谈话中，玩家的选择
public enum Kingdom_PlayerChoose
{
    // 未选择
    None,
    // 能 
    Yes,
    // 不能
    No,
}