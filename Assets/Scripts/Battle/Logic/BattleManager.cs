using System.Collections.Generic;
using UnityEngine;

//BattleManager处理战斗的数据
public class BattleManager : Singleton <BattleManager>
{
    // 所有参战角色的行动轴
    public List<AxisOfAction> allAxisOfAction = new List<AxisOfAction>();
    // 所有参战角色的战斗属性
    public List<BattleARB> allBattleARB = new List<BattleARB>();

    [Header("背包")]
    public ItemDataList_SO Bag;

    [Header("敌人队伍")]
    public EnemyTeam_SO enemyTeam_SO;
    // 敌人是否死亡
    private bool isDead_Enemy;
    // 当前受到攻击的敌人的站位编号 （为0说明目标为玩家）
    public int attackedEnemy_StandID;
    // 当前回合的敌人的站位编号
    public int attackEnemy_StandID;

    [Header("玩家")]
    public PlayerBattleARB_SO playerBattleARB_SO;
    // 玩家当前的操作
    public PlayerChooseAction playerChooseAction;
    // 玩家是否死亡
    private bool isDead_Player;
    // 玩家是否做出操作(选择目标)(达到目标操作线路的终点)
    public bool playerMakeOperation;

    // 玩家选择的物品的ID
    public int playerChooseItemID;

    private void OnEnable()
    {
        EventHandler.BattleStartEvent += OnBattleStartEvent;
    }

    private void OnDisable()
    {
        EventHandler.BattleStartEvent -= OnBattleStartEvent;
    }

    private void OnBattleStartEvent(string battleBack,EnemyTeam_SO enemyTeam)
    {
        // 获取敌人队伍信息
        enemyTeam_SO = enemyTeam;
        // 构建战斗属性表
        InitAllBattleARB();
        // 构建行动轴表
        InitAllAxisOfAction();
    }


    
    
    /// <summary>
    /// 玩家攻击
    /// </summary>
    public void PlayerAttack()
    {
        switch (playerChooseAction)
        {
            //普攻
            case PlayerChooseAction.Attack:
                int enemyRemainHP = allBattleARB[attackedEnemy_StandID].currentHP + allBattleARB[attackedEnemy_StandID].currentDefend - allBattleARB[0].currentAttack;
                if(enemyRemainHP > 0)
                {
                    allBattleARB[attackedEnemy_StandID].currentHP = enemyRemainHP;
                }
                else
                {
                    allBattleARB[attackedEnemy_StandID].currentHP = 0;
                }
                break;

            //技能攻击
            case PlayerChooseAction.Skill:

                break;
        }
    }

    public void PlayerUseItem()
    {
        switch (playerChooseItemID)
        {
            case 1001: //速度药剂
                UseSpeedPotion();
                break;
            case 1002: //恢复药剂
                UseHPPotion();
                Debug.Log("恢复！");
                break;
            case 1003: //未知药剂
                UseUnknownPotion();
                break;
            case 1004: //神秘小药水
                UseMysteriousPotion();
                break;
            
        }
    }
    /// <summary>
    /// 使用速度药剂
    /// </summary>
    private void UseSpeedPotion()
    {
        allBattleARB[attackedEnemy_StandID].currentSpeed += Bag.fromIDToFindItem(1001).baseAttribute;
        allAxisOfAction[attackedEnemy_StandID].walkSpeed += Bag.fromIDToFindItem(1001).baseAttribute;
        // 上Buff
        allBattleARB[attackedEnemy_StandID].buff = Bag.fromIDToFindItem(1001).buff;
    }
    /// <summary>
    /// 使用恢复药剂
    /// </summary>
    private void UseHPPotion()
    {
        if((allBattleARB[attackedEnemy_StandID].currentHP += Bag.fromIDToFindItem(1002).baseAttribute) <= allBattleARB[attackedEnemy_StandID].baseHP)
            allBattleARB[attackedEnemy_StandID].currentHP += Bag.fromIDToFindItem(1002).baseAttribute;
        else
            allBattleARB[attackedEnemy_StandID].currentHP = allBattleARB[attackedEnemy_StandID].baseHP;
        // 上Buff
        allBattleARB[attackedEnemy_StandID].buff = Bag.fromIDToFindItem(1002).buff;
    }
    /// <summary>
    /// 使用未知药剂
    /// </summary>
    private void UseUnknownPotion()
    {
        allBattleARB[attackedEnemy_StandID].currentHP -= Bag.fromIDToFindItem(1003).baseAttribute;
    }
    /// <summary>
    /// 使用神秘小药水
    /// </summary>
    private void UseMysteriousPotion()
    {
        if((allBattleARB[attackedEnemy_StandID].currentHP += Bag.fromIDToFindItem(1004).baseAttribute) <= allBattleARB[attackedEnemy_StandID].baseHP)
            allBattleARB[attackedEnemy_StandID].currentHP += Bag.fromIDToFindItem(1004).baseAttribute;
        else
            allBattleARB[attackedEnemy_StandID].currentHP = allBattleARB[attackedEnemy_StandID].baseHP;
    }

    /// <summary>
    /// 更新所有Buff效果
    /// </summary>
    public void UpdataAllBuff()
    {
        foreach (var battleARB in allBattleARB)
        {
            battleARB.UpdataBuff();
        }
    }



    /// <summary>
    /// 敌人攻击后玩家血量处理
    /// </summary>
    public void EnemyAttack()
    {
        // 玩家剩余血量
        int playerRemainHP = allBattleARB[0].currentHP + allBattleARB[0].currentDefend - allBattleARB[attackEnemy_StandID].currentAttack;
        if (playerRemainHP > 0)
        {
            allBattleARB[0].currentHP = playerRemainHP;
        }
        else
        {
            allBattleARB[0].currentHP = 0;
        }
    }


    /// <summary>
    /// 检测战斗是否结束
    /// </summary>
    /// <returns>玩家胜利返回1，敌人胜利返回2，无人胜利返回-1</returns>
    public int BattleEnd()
    {
        // 检测敌人是否死光
        int surviveEnemyNum = 0;
        foreach (var enemy in allBattleARB)
        {
            if (enemy.currentHP > 0 && enemy.standID != 0)
                surviveEnemyNum++;
        }
        if(surviveEnemyNum == 0)
            isDead_Enemy = true;
        else
            isDead_Enemy = false;

        // 检测玩家是否死亡
        if (allBattleARB[0].currentHP > 0)
            isDead_Player = false;
        else 
            isDead_Player = true;


        if (isDead_Enemy) // 如果敌人死亡，玩家胜利
        {
            return 1;
        }
        else if (isDead_Player) // 如果玩家死亡，敌人胜利
        {
            return 2;
        }
        else
        {
            return -1;
        }
    }

    /// <summary>
    /// 保存玩家战斗属性
    /// </summary>
    public void Save_PlayerARB()
    {
        playerBattleARB_SO.playerBattleARB.currentHP = allBattleARB[0].currentHP;
    }

    /// <summary>
    /// 重置杂七杂八的值
    /// </summary>
    public void InitAllData()
    {
        playerMakeOperation = false;
        playerChooseAction = PlayerChooseAction.None;
        isDead_Enemy = false;
        isDead_Player = false;
        attackedEnemy_StandID = 0;
        attackEnemy_StandID = 100;
    }

    /// <summary>
    /// 构建新的战斗属性表
    /// </summary>
    private void InitAllBattleARB()
    {
        allBattleARB.Clear();

        allBattleARB.Add(new BattleARB(playerBattleARB_SO.playerBattleARB));
        foreach (var enemy in enemyTeam_SO.enemyTeam)
        {
            allBattleARB.Add(new BattleARB(enemy));
        }
    }

    /// <summary>
    /// 构建新的行动轴表
    /// </summary>
    private void InitAllAxisOfAction()
    {
        allAxisOfAction.Clear();

        allAxisOfAction.Add(new AxisOfAction(playerBattleARB_SO.playerBattleARB));
        foreach (var enemy in enemyTeam_SO.enemyTeam)
        {
            allAxisOfAction.Add(new AxisOfAction(enemy));
        }
    }

    /// <summary>
    /// 行动轴：判断当前为谁的回合，返回对应回合角色的行动轴ID，0为玩家，1、2、3为敌人站位
    /// </summary>
    /// <returns></returns>
    public int isWhoTurn()
    {
        int i = 0;
        //直到循环到有角色行动到路程的终点
        while (i < 100)
        {
            //用速度乘以次数的方式，对战斗列表的每个元素进行判断是否抵达终点
            foreach (var axisOfAction in allAxisOfAction)
            {
                axisOfAction.path += axisOfAction.walkSpeed;
                if (axisOfAction.path >= Settings.battleDistance)
                {
                    // 走到终点路程清零
                    axisOfAction.path = 0f;
                    Debug.Log("轮到" + axisOfAction.AxisID);
                    // 返回该角色行动轴ID
                    return axisOfAction.AxisID;
                }
            }
            i++;
        }
        return 100;
    }
}
