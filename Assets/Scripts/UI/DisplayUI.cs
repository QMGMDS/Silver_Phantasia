using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayUI : MonoBehaviour
{
    public GameObject baseUI;
    public GameObject normalUI;
    public GameObject battleUI;

    private void OnEnable()
    {
        EventHandler.BattleStartEvent += OnBattleStartEvent;
    }

    private void OnDisable()
    {
        EventHandler.BattleStartEvent -= OnBattleStartEvent;
    }

    private void OnBattleStartEvent(string battleBack,BattleAttributeDataList_SO enemyTeam)
    {
        SwitchBattleUI(enemyTeam);
    }

    /// <summary>
    /// 切换战斗模式UI并将敌人队伍给到battleUI
    /// </summary>
    /// <param name="player"></param>
    private void SwitchBattleUI(BattleAttributeDataList_SO enemyTeam)
    {
        normalUI.SetActive(false);
        battleUI.SetActive(true);
        //拿到UI所有含有BattleHUD组件的子物体的BattleHUD并执行里面的初始化方法
        var allBattleHUD = GetComponentsInChildren<BattleHUD>();
        foreach (var battleHUD in allBattleHUD)
        {
            if(battleHUD != null)
            {
                battleHUD.InitHUD(enemyTeam);
            }
        }
    }
}
