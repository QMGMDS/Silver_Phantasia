using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class BattleUI : MonoBehaviour
{
    //玩家操作UI显示
    public GameObject Action;
    //玩家Item/Skill背包
    public GameObject battleBag;
    //行动轴
    public BattleWalkController battleWalkController;

    //每次BattleUI被激活时调用，初始化HUD
    private void OnEnable()
    {
        StartCoroutine(InitBattle());
        EventHandler.PlayerUseItem += OnPlayerUseItem;
        EventHandler.PlayerUseSkill += OnPlayerUseSkill;
    }

    private void OnDisable()
    {
        EventHandler.PlayerUseItem -= OnPlayerUseItem;
        EventHandler.PlayerUseSkill -= OnPlayerUseSkill;
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
            {
                battleHUD.InitHUD();
            }
        }
        //等待初始化
        yield return new WaitForSeconds(2f);
        BattleManager.Instance.Inited = true;
        yield return new WaitForSeconds(1f);
        
    }


    //执行行动轴动画的前提是：
    //1.战斗初始化已经完成
    //2.行动轴判断当前行动玩家已经完成
    public void WalkAnimation()
    {
        StartCoroutine(battleWalkController.Move());
    }

    public void RecoveryWalk()
    {
        battleWalkController.Recovery();
    }
    

    //玩家回合的触发事件
    //3.对应玩家操作Action激活
    public void OpenAction()
    {
        Action.SetActive(true);
        BattleManager.Instance.currentChooseAction = ChooseAction.None;
    }

    //按钮按下了表示确认了对应的行动
    //1.关闭Action
    public void CloseAction()
    {
        Action.SetActive(false);
    }

    /// <summary>
    /// 打开战斗背包
    /// </summary>
    public void OpenBag()
    {
        Action.SetActive(false);
        battleBag.SetActive(true);
    }

    /// <summary>
    /// 使用物品关闭战斗背包
    /// </summary>
    private void OnPlayerUseItem(ItemDetials detials)
    {
        battleBag.SetActive(false);
    }

    /// <summary>
    /// 使用技能关闭战斗背包
    /// </summary>
    /// <param name="details"></param>
    private void OnPlayerUseSkill(SkillDetails details)
    {
        battleBag.SetActive(false);
    }

}
