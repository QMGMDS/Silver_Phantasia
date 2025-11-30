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
        StartCoroutine(SwitchBattleEndUI());
    }

    /// <summary>
    /// 切换战斗模式UI
    /// </summary>
    private void SwitchBattleStartUI()
    {
        normalUI.SetActive(false);
        battleUI.SetActive(true);
    }

    
    private IEnumerator SwitchBattleEndUI()
    {
        yield return new WaitForSeconds(2f);
        normalUI.SetActive(true);
        battleUI.SetActive(false);
    }
}
