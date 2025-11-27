using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton <BattleManager>
{
    //战斗列表（用于搜索出角色行动轴）
    public List<BattleAttribute> battleList = new List<BattleAttribute>();
    //玩家的战斗信息，调用时实时进行修改
    public BattleAttributeDataList_SO playerTeam;
    //只拿敌人的战斗信息，不对里面内容进行修改
    public List<BattleAttribute> enemyTeam = new List<BattleAttribute>();
    //当前按下的按钮类型
    public ButtonType currentButtonType;

    

    
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
    public void PlayerAttack()
    {
        switch (currentButtonType)
        {
            case ButtonType.Attack:

                break;
            case ButtonType.Skill:

                break;
            case ButtonType.Item:

                break;
        }
    }

    /// <summary>
    /// 行动轴判断下一次轮到谁的回合，返回角色的信息BattleAttribute
    /// </summary>
    /// <returns></returns>
    public BattleAttribute isWhoTurn()
    {
        //直到循环到有角色行动到路程的终点
        while (true)
        {
            //用速度乘以次数的方式，对战斗列表的每个元素进行判断是否抵达终点
            foreach (var character in battleList)
            {
                character.path += character.speed;
                if (character.path >= Settings.battleDistance)
                {
                    //走到终点路程清零
                    character.path = 0f;
                    //进入该角色的回合，返回该角色的信息
                    return character;
                }
            }
        }
    } 
}
