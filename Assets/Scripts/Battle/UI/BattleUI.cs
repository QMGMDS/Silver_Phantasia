using System.Collections;
using UnityEngine;
using DG.Tweening;

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

    

    //玩家回合的触发事件
    //3.对应玩家操作Action激活
    public void OpenAction()
    {
        Action.SetActive(true);
    }

    //按钮按下了表示确认了对应的行动
    //1.关闭Action
    public void CloseAction()
    {
        Action.SetActive(false);
    }

}
