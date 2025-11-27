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
        EventHandler.BattleEndEvent += OnBattleEndEvent;
    }

    private void OnDisable()
    {
        EventHandler.BattleStartEvent -= OnBattleStartEvent;
        EventHandler.BattleEndEvent -= OnBattleEndEvent;
    }

    private void OnBattleStartEvent(string battleBack,BattleAttributeDataList_SO enemyTeam)
    {
        SwitchBattleStartUI();
    }

    private void OnBattleEndEvent()
    {
        SwitchBattleEndUI();
    }

    /// <summary>
    /// 切换战斗模式UI并将敌人队伍给到battleUI
    /// </summary>
    /// <param name="player"></param>
    private void SwitchBattleStartUI()
    {
        normalUI.SetActive(false);
        battleUI.SetActive(true);
        //拿到UI所有含有BattleHUD组件的子物体的BattleHUD并执行里面的初始化方法
        var allBattleHUD = GetComponentsInChildren<BattleHUD>();
        foreach (var battleHUD in allBattleHUD)
        {
            if(battleHUD != null)
            {
                battleHUD.InitHUD();
            }
        }
    }


    private void SwitchBattleEndUI()
    {
        normalUI.SetActive(true);
        battleUI.SetActive(false);
    }
}
