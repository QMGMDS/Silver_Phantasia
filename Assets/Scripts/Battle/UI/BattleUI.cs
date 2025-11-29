using System.Collections;
using UnityEngine;
using DG.Tweening;

public class BattleUI : MonoBehaviour
{
    //玩家操作UI显示
    public GameObject Action;

    //行动轴
    public BattleWalkController battleWalkController;

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
        //等待初始化
        yield return new WaitForSeconds(2f);
        BattleManager.Instance.Inited = true;
        yield return new WaitForSeconds(1f);
        //等待行动轴动画播放完毕
        //执行行动轴动画的前提是：
        //1.战斗初始化已经完成
        //2.行动轴判断当前行动玩家已经完成
        StartCoroutine(battleWalkController.Move());
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
