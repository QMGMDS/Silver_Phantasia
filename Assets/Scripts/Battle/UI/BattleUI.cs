using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    //玩家操作UI显示
    public GameObject Action;

    //每次BattleUI被激活时调用，初始化HUD
    private void OnEnable()
    {
        StartCoroutine(InitBattle());
    }

    /// <summary>
    /// 确保战斗初始化
    /// </summary>
    /// <returns></returns>
    private IEnumerator InitBattle()
    {
        var allBattleHUD = GetComponentsInChildren<BattleHUD>();
        foreach (var battleHUD in allBattleHUD)
        {
            if(battleHUD != null)
                battleHUD.InitHUD();
        }
        //等待初始化（TODO：这里本来应该是等待行动轴上的玩家角色移动到终点）
        yield return new WaitForSeconds(2f);
        BattleManager.Instance.Inited = true;
    }

    


}
