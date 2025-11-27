using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    public GameObject Action;
    //用于存储当前回合的目标角色属性
    private BattleAttribute thisCharacterTurn;

    private void Update()
    {
        if (BattleManager.Instance.BattleTurn == Turn.None)
        {
            thisCharacterTurn = BattleManager.Instance.isWhoTurn();
            Debug.Log(thisCharacterTurn.roleName);
            if (thisCharacterTurn.isPlayer)
            {
                BattleManager.Instance.BattleTurn = Turn.Player;
                Action.SetActive(true);
                //高亮对应Player的HUD
            }
            else
            {
                BattleManager.Instance.BattleTurn = Turn.Enemy;
                Debug.Log("enemy攻击!");
                Action.SetActive(false);
                BattleManager.Instance.EnemyAttack();
                //HUD更新
                var allBattleHUD = GetComponentsInChildren<BattleHUD>();
                foreach (var battleHUD in allBattleHUD)
                {
                    if(battleHUD != null)
                        battleHUD.UpdateHUD();
                }
                BattleManager.Instance.BattleTurn = Turn.None;
            }
        }
        else if(BattleManager.Instance.BattleTurn == Turn.End)
        {
            Debug.Log("战斗结束");
            BattleManager.Instance.BattleEnd();
        }
    }

    /// <summary>
    /// 按钮按下事件调用，按下按钮关闭ActionUI
    /// </summary>
    public void CloseActionUI()
    {
        Action.SetActive(false);
    }

}
