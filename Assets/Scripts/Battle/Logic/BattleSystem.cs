using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//BattleSystem处理战斗的逻辑

public class BattleSystem : MonoBehaviour
{
    private BattleUI battleUI;


    //当前回合的战斗角色
    private BattleAttribute thisTurnCharacter;

    //玩家回合的触发事件
    //1.对应玩家角色HUD高亮
    //2.对应玩家显示图片闪烁
    //3.对应玩家操作Action激活
    public UnityEvent nowIsPlayerTurn;

    public UnityEvent nowIsEnemyTurn;

    //玩家攻击事件
    //1.玩家攻击动画
    //2.敌人受伤动画
    public UnityEvent playerAttackAnimations;

    private void Awake()
    {
        BattleManager.Instance.BattleTurn = Turn.None;
        battleUI = GetComponent<BattleUI>();
    }

    private void Update()
    {
        if(!BattleManager.Instance.Inited)
            return;
        if(BattleManager.Instance.BattleTurn == Turn.None)
        {
            //上个回合结束了才判断下个回合的行动
            if (BattleManager.Instance.thisTurnOver)
            {
                AxisOfAction();
            }
            
            //等待行动轴动画完成
            if(BattleManager.Instance.walking)
                return;
            
            if(thisTurnCharacter.currentHP <= 0)
                return;
            if (thisTurnCharacter.isPlayer)
            {
                //玩家的战斗逻辑
                BattleManager.Instance.BattleTurn = Turn.Player;
                //UI提示事件
                nowIsPlayerTurn.Invoke();
                Debug.Log(thisTurnCharacter.roleName);
                //等待玩家操作......
                //当玩家按下Action按钮时，每个按钮对应不同事件

            }
            //当前回合角色不是玩家就是敌人
            else
            {
                //敌人的战斗逻辑
                BattleManager.Instance.BattleTurn = Turn.Enemy;
                Debug.Log("敌人攻击");
                EnemyAttack();
            }
        }
        //不是空回合就是结束回合
        else if(BattleManager.Instance.BattleTurn == Turn.End)
        {
            Debug.Log("战斗结束");
            BattleEnd();
        }
        
    }


    //鼠标按下事件
    //3.攻击敌人(攻击动画，攻击数据处理)
    //切换回合
    public void PlayerAttack()
    {
        //玩家攻击动画
        //敌人受伤动画
        //等待动画播放结束......
        //敌人血量处理
        BattleManager.Instance.PlayerAttack();
        if(BattleManager.Instance.BattleEnd() == -1)
        {
            BattleManager.Instance.BattleTurn = Turn.None;
        }
        else
        {
            BattleManager.Instance.BattleTurn = Turn.End;
        }
        BattleManager.Instance.thisTurnOver = true;
    }


    public void EnemyAttack()
    {
        //敌人攻击动画
        //玩家受伤动画
        //等待动画播放结束......
        //玩家血量处理
        BattleManager.Instance.EnemyAttack();
        nowIsEnemyTurn?.Invoke();
        if(BattleManager.Instance.BattleEnd() == -1)
        {
            Debug.Log("继续");
            BattleManager.Instance.BattleTurn = Turn.None;
        }
        else
        {
            BattleManager.Instance.BattleTurn = Turn.End;
        }
        BattleManager.Instance.thisTurnOver = true;
    }

    public void BattleEnd()
    {
        if(BattleManager.Instance.BattleEnd() == 1)
        {
            Debug.Log("玩家胜利");
            //返回
            EventHandler.CallBattleEndEvent();
            EventHandler.CallOpenPlayerMoveEvent();
        }
        else
        {
            Debug.Log("敌人胜利");
            //游戏失败
        }

        //清空临时数据
        foreach (var character in BattleManager.Instance.battleList)
        {
            if(!character.isPlayer)
                character.currentHP = character.maxHP;
            character.path = 0;
            character.walkPath = 0;
            character.lastWalkPath = 0;
            character.walkSpeed = 0;
        }
        //战斗结束清空列表
        BattleManager.Instance.battleList = new List<BattleAttribute>();
    }

    private void AxisOfAction()
    {
        //这个回合开始了
        BattleManager.Instance.thisTurnOver = false;
        BattleManager.Instance.walking = true;
        Debug.Log("aaa");
        thisTurnCharacter = BattleManager.Instance.isWhoTurn();
        battleUI.WalkAnimation();
    }

}