using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleManager : Singleton <BattleManager>
{
    //战斗列表（用于搜索出角色行动轴和判断在场存活人数）
    public List<BattleAttribute> battleList = new List<BattleAttribute>();


    //玩家的战斗信息，调用时实时进行修改SO
    public BattleAttributeDataList_SO playerTeam;
    //敌人的战斗信息
    public List<BattleAttribute> enemyTeam;

    //当前按下的按钮类型
    public ButtonType currentButtonType;
    //当前行动回合的角色
    public BattleAttribute thisCharacterTurn;
    //当前的回合
    public Turn BattleTurn;
    

    
    //角色在战斗中已走过的回合制路程
    public int walkPath;


    private void OnEnable()
    {
        EventHandler.BattleStartEvent += OnBattleStartEvent;
    }

    private void OnDisable()
    {
        EventHandler.BattleStartEvent += OnBattleStartEvent;
    }

    private void OnBattleStartEvent(string battleBack,BattleAttributeDataList_SO enemyTeam)
    {
        //战斗开始从Team_SO中获取敌人队伍信息
        this.enemyTeam = enemyTeam.AttributesList;
        //创建战斗列表
        foreach (var player in playerTeam.AttributesList)
        {
            battleList.Add(player);
        }
        foreach (var enemy in this.enemyTeam)
        {
            battleList.Add(enemy);
        }
    }

    
    /// <summary>
    /// 玩家攻击
    /// </summary>
    /// <param name="enemy">被攻击敌人的信息</param>
    public void PlayerAttack(BattleAttribute toSearchEnemy)
    {
        if (toSearchEnemy == null)
            return;
        //搜索对应的enemy
        foreach (var enemy in enemyTeam)
        {
            if(enemy.roleID == toSearchEnemy.roleID)
            {
                switch (currentButtonType)
                {
                    case ButtonType.Attack:
                        enemy.currentHP = enemy.currentHP - thisCharacterTurn.baseAttack;
                        break;
                    case ButtonType.Skill:

                        break;
                    case ButtonType.Item:

                        break;
                }
                BattleTurn = Turn.None;
            }
        }
    }

    public void EnemyAttack()
    {
        //搜索活着的站位靠前的Player进行攻击
        foreach (var player in playerTeam.AttributesList)
        {
            if (player.currentHP > 0)
            {
                player.currentHP = player.currentHP - thisCharacterTurn.baseAttack;
                return;
            }
        }
    }


    /// <summary>
    /// 检测战斗是否结束
    /// </summary>
    public void BattleEnd()
    {
        foreach (var character in battleList)
        {
            if(character.currentHP > 0 && character.isPlayer)
            {
                
            }
        }


        BattleTurn = Turn.None;
        EventHandler.CallBattleEndEvent();
        EventHandler.CallOpenPlayerMoveEvent();
    }




    /// <summary>
    /// 行动轴判断下一次轮到谁的回合，返回角色的信息BattleAttribute，BattleUI调用
    /// </summary>
    /// <returns>角色信息BattleAttribute，为null则角色速度没赋值</returns>
    public BattleAttribute isWhoTurn()
    {
        //直到循环到有角色行动到路程的终点
        while (true)
        {
            //用速度乘以次数的方式，对战斗列表的每个元素进行判断是否抵达终点
            foreach (var character in battleList)
            {
                //先判断遍历到的角色速度是否为0，为0就陷入无限循环（
                if (character.speed == 0)
                    return null;
                character.path += character.speed;
                if (character.path >= Settings.battleDistance)
                {
                    //走到终点路程清零
                    character.path = 0f;
                    //临时存储
                    thisCharacterTurn = character;
                    //进入该角色的回合，返回该角色的信息
                    return character;
                }
            }
        }
    }
}
