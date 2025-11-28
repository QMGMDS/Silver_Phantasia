using System;
using UnityEngine;
using UnityEngine.Events;

public class BattleSystem : MonoBehaviour
{
    //当前回合的战斗角色
    private BattleAttribute thisTurnCharacter;

    //玩家回合的触发事件
    //1.对应玩家角色HUD高亮
    //2.对应玩家显示图片闪烁
    //3.对应玩家操作Action激活
    public UnityEvent nowIsPlayerTurn;

    private void Awake()
    {
        BattleManager.Instance.BattleTurn = Turn.None;
        BattleManager.Instance.Inited = false;
    }

    private void Update()
    {
        if(!BattleManager.Instance.Inited)
            return;
        if(BattleManager.Instance.BattleTurn == Turn.None)
        {
            thisTurnCharacter = BattleManager.Instance.isWhoTurn();
            if (thisTurnCharacter.isPlayer)
            {
                //玩家的战斗逻辑
                BattleManager.Instance.BattleTurn = Turn.Player;
                //UI提示事件
                nowIsPlayerTurn.Invoke();
                //等待玩家操作......
                //当玩家按下Action按钮时，每个按钮对应不同事件

            }
            //当前回合角色不是玩家就是敌人
            else
            {
                //敌人的战斗逻辑
            }
        }
        //不是空回合就是结束回合
        else
        {
            
        }
        
    }


}
