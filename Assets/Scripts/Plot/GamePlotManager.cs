using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class GamePlotManager : Singleton <GamePlotManager>
{
    // 勇者与国王的谈话中，玩家的选择
    public Kingdom_PlayerChoose kingdom_PlayerChoose;

    // BOSS战斗
    public Battle_BOSS battle_BOSS;

    // 勇者是否进入地牢
    public bool braveEntryDungeon;

    
    

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    /// <summary>
    /// 玩家选择完选项之后，对话结束之后
    /// </summary>
    public void Kingdom_ChooseAndDialogueOver()
    {
        switch (kingdom_PlayerChoose)
        {
            case Kingdom_PlayerChoose.Yes: //勇者离开王宫
                EventHandler.CallKingdom_BraveQuit();
                break;
            case Kingdom_PlayerChoose.No: //游戏结束
                EventHandler.CallTransitionEvent("Dead",new Vector3(0,0,0));
                EventHandler.CallClosePlayerMoveEvent();
                break;
        }
    }
    
}
