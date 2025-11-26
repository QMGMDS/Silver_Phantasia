using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    //玩家的战斗信息
    public BattleAttributeDataList_SO playerTeam;
    //敌人的战斗信息
    private List<BattleAttribute> enemyTeam;


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

    }

    
    



}
