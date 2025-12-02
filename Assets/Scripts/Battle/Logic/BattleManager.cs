using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using Unity.VisualScripting;
using UnityEngine;

//战斗流程：
//战斗开始，初始化准备工作(战斗场景UI显示，角色移动操作关闭，)
//while循环（直到判断游戏结束）
//{
//行动轴判断谁的回合（行动轴判断谁的回合，行动轴移动判断动画，对应玩家角色HUD实化提示和人物选中半透明浮现提示，玩家操作面板显示提示）
//对应回合的角色攻击（攻击动画提示，攻击后数据更新，状态栏更新）
//判断游戏是否结束
//}




//BattleManager处理战斗的数据

public class BattleManager : Singleton <BattleManager>
{
    //战斗是否初始化完毕
    public bool Inited;
    //当前回合是否结束
    public bool thisTurnOver;


    //战斗列表（用于搜索出角色行动轴和判断在场存活人数）
    public List<BattleAttribute> battleList = new List<BattleAttribute>();
    //行动轴动画是否在播放
    public bool walking;


    //玩家的战斗信息，调用时实时进行修改SO
    public BattleAttributeDataList_SO playerTeam;
    //敌人的战斗信息
    public List<BattleAttribute> enemyTeam;

    //当前按下的按钮类型
    public ButtonType currentButtonType;
    //当前行动回合的角色
    public BattleAttribute thisCharacterTurn;
    //被攻击的角色
    public BattleAttribute attackedCharacter;
    //当前的回合阶段
    public Turn BattleTurn;
    



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
        if (attackedCharacter == null)
            return;
        //搜索对应的enemy
       
        switch (currentButtonType)
        {
            //普攻
            case ButtonType.Attack:
                attackedCharacter.currentHP = attackedCharacter.currentHP - thisCharacterTurn.baseAttack;
                break;
            //技能攻击
            case ButtonType.Skill:

                break;
            //物品攻击
            case ButtonType.Item:

                break;
        }
        BattleTurn = Turn.None;
    }

    public void EnemyAttack()
    {
        //搜索活着的站位靠前的Player进行攻击
        foreach (var player in playerTeam.AttributesList)
        {
            if (player.currentHP > 0)
            {
                attackedCharacter = player;
                player.currentHP = player.currentHP - thisCharacterTurn.baseAttack;
                return;
            }
        }
    }


    /// <summary>
    /// 检测战斗是否结束
    /// </summary>
    /// <returns>玩家胜利返回1，敌人胜利返回2，无人胜利返回-1</returns>
    public int BattleEnd()
    {
        //玩家是否存活
        bool playerSurvive = false;
        //敌人是否存活
        bool enemySurvive = false;
        foreach (var enemy in enemyTeam)
        {
            if (enemy.currentHP > 0)
            {
                enemySurvive = true;
                break;
            }
        }
        foreach (var player in playerTeam.AttributesList)
        {
            if (player.currentHP > 0)
            {
                playerSurvive = true;
                break;
            }
        }
        if(enemySurvive == false)
        {
            return 1;
        }
            
        if(playerSurvive == false)
        {
            return 2;
        }
        return -1;
    }


    /// <summary>
    /// 战斗数据复原
    /// </summary>
    public void Recovery()
    {
        //清空临时数据
        foreach (var character in battleList)
        {
            if(!character.isPlayer)
                character.currentHP = character.maxHP;
            character.path = 0;
            character.walkPath = 0;
            character.lastWalkPath = 0;
            character.walkSpeed = 0;
        }
        //战斗结束清空列表
        battleList = new List<BattleAttribute>();
        BattleTurn = Turn.None;
        Inited = false;
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
