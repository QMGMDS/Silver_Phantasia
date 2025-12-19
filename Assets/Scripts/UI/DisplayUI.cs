using System.Collections;
using UnityEngine;

public class DisplayUI : MonoBehaviour
{
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

    private void OnBattleStartEvent(string battleBack,EnemyTeam_SO enemyTeam)
    {
        StartCoroutine(SwitchBattleStartUI());
    }

    private void OnBattleEndEvent()
    {
        StartCoroutine(SwitchBattleEndUI());
    }

    /// <summary>
    /// 切换战斗模式UI
    /// </summary>
    private IEnumerator SwitchBattleStartUI()
    {
        yield return new WaitForSeconds(1.2f);
        normalUI.SetActive(false);
        battleUI.SetActive(true);
    }

    
    private IEnumerator SwitchBattleEndUI()
    {
        yield return new WaitForSeconds(1.2f);
        battleUI.SetActive(false);
        normalUI.SetActive(true);
    }
}
