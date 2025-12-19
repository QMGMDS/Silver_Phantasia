using System.Collections;
using UnityEngine;

//BattleSystem处理战斗的逻辑
public class BattleSystem : MonoBehaviour
{
    private BattleUI battleUI;
    public BattleWalkController battleWalkController;

    // 当前回合的角色站位ID，玩家为0
    [SerializeField]private int currentTurnCharacter_StandID;
    private BattleTurn currentBattleTurn;


    private void Awake()
    {
        battleUI = GetComponent<BattleUI>();
    }

    private void OnEnable()
    {
        StartCoroutine(Battle_LogicStart());
    }

    private void OnDisable()
    {
        
    }


    /// <summary>
    /// 一次战斗回合的开始
    /// </summary>
    private IEnumerator Battle_LogicStart()
    {
        currentBattleTurn = BattleTurn.None;
        // 判断
        JudgeAxisOfAction();
        // 播放行动轴动画
        //StartCoroutine(battleWalkController.Move());
        // 等待动画播放完毕
        //yield return new WaitUntil(() => battleWalkController.walkAnimIsOver);
        // 重置值
        //battleWalkController.walkAnimIsOver = false;
        // 走入对应回合
        switch (currentBattleTurn)
        {
            case BattleTurn.Player:
                StartCoroutine(NowIsPlayerTurn());
                break;
            case BattleTurn.Enemy:
                NowIsEnemyTurn();
                break;
        }
        yield return null;
    }

    

    /// <summary>
    /// 每个回合开始前判断该回合是谁的回合
    /// </summary>
    private void JudgeAxisOfAction()
    {
        // 死亡的跳过
        do
        {
            // 轮到谁了？
            currentTurnCharacter_StandID = BattleManager.Instance.isWhoTurn();
        }while(BattleManager.Instance.allBattleARB[currentTurnCharacter_StandID].currentHP < 0);


        if (currentTurnCharacter_StandID == 0) // 为0则此时为玩家回合
        {
            currentBattleTurn = BattleTurn.Player;
        }
        else // 否则为敌人回合
        {            
            currentBattleTurn = BattleTurn.Enemy;
        }
    }

    /// <summary>
    /// 玩家回合
    /// </summary>
    private IEnumerator NowIsPlayerTurn()
    {
        // 告诉UI显示玩家操作
        battleUI.Battle_ShowPlayerAction();


        // 等待玩家做出操作
        yield return new WaitUntil(() => BattleManager.Instance.playerMakeOperation);


        switch (BattleManager.Instance.playerChooseAction)
        {
            case PlayerChooseAction.Attack:
                BattleManager.Instance.PlayerAttack();
                break;
        }

        StartCoroutine(Battle_LogicEnd());
    }
    
    /// <summary>
    /// 敌人回合
    /// </summary>
    private void NowIsEnemyTurn()
    {
        BattleManager.Instance.attackEnemy_StandID = currentTurnCharacter_StandID;
        BattleManager.Instance.EnemyAttack();

        //更新玩家HUD
        EventHandler.CallBattle_PlayerHUDUpdate();

        StartCoroutine(Battle_LogicEnd());
    }


    /// <summary>
    /// 一次战斗回合的结束
    /// </summary>
    public IEnumerator Battle_LogicEnd()
    {
        int k = BattleManager.Instance.BattleEnd();
        if(k == 1)
        {
            currentBattleTurn = BattleTurn.End;
            Debug.Log("玩家胜利");
            BattleManager.Instance.Save_PlayerARB();
            yield return new WaitForSeconds(2f); //缓冲时间
            if(GamePlotManager.Instance.battle_BOSS == Battle_BOSS.Ing)
            {
                //结束游戏
                EventHandler.CallTransitionEvent("Game_Over",new Vector3(0,0,0));
                EventHandler.CallBattleEndEvent();
                EventHandler.CallClosePlayerMoveEvent();
                EventHandler.CallPlayerShowImageChange(false);
            }
            else
            {
                //返回
                EventHandler.CallBattleEndEvent();
                EventHandler.CallOpenPlayerMoveEvent();
            }
        }
        else if(k == 2)
        {
            Debug.Log("敌人胜利");
            yield return new WaitForSeconds(2f); //缓冲时间


            //游戏失败
            EventHandler.CallTransitionEvent("Dead",new Vector3(0,0,0));
            EventHandler.CallClosePlayerMoveEvent();
        }
        else
        {
            // 战斗继续，重置数据，回到回合开始
            BattleManager.Instance.InitAllData();
            StartCoroutine(Battle_LogicStart());
        }
    }



    // //玩家攻击动画和数据处理
    // private IEnumerator PlayerAttackAnimAndThenAttack()
    // {
    //     //播放动画
    //     playerAttackAnimations?.Invoke();
    //     //等待动画播放
    //     yield return new WaitForSeconds(2f);
    //     //玩家攻击数据处理
    //     BattleManager.Instance.PlayerAttack();

    //     //判断战斗是否结束
    //     if(BattleManager.Instance.BattleEnd() == -1)
    //     {
    //         BattleManager.Instance.BattleTurn = Turn.None;
    //     }
    //     else
    //     {
    //         BattleManager.Instance.BattleTurn = Turn.End;
    //     }
    //     //该回合结束
    //     BattleManager.Instance.thisTurnOver = true;
    // }

    // public void EnemyAttack()
    // {
    //     StartCoroutine(EnemyAttackAnimAndThenAttack());
    // }

    // private IEnumerator EnemyAttackAnimAndThenAttack()
    // {
    //     //播放动画
    //     EnemyAttackAnimations?.Invoke();
    //     //等待动画播放
    //     yield return new WaitForSeconds(2f);
    //     //敌人攻击数据处理
    //     BattleManager.Instance.EnemyAttack();
    //     //玩家血量更新
    //     nowIsEnemyTurn?.Invoke();

    //     //战斗是否结束
    //     if(BattleManager.Instance.BattleEnd() == -1)
    //     {
    //         BattleManager.Instance.BattleTurn = Turn.None;
    //     }
    //     else
    //     {
    //         BattleManager.Instance.BattleTurn = Turn.End;
    //     }
    //     //该回合结束
    //     BattleManager.Instance.thisTurnOver = true;
    // }


    



    
    
    // /// <summary>
    // /// 玩家按下攻击
    // /// </summary>
    // public void AttackButtonDown()
    // {
    //     BattleManager.Instance.currentChooseAction = ChooseAction.Attack;
    // }

    // /// <summary>
    // /// 玩家按下防御
    // /// </summary>
    // public void DefendButtonDown()
    // {
    //     //玩家防御数据处理
    //     BattleManager.Instance.PlayerDefend();

    //     //该回合结束
    //     BattleManager.Instance.thisTurnOver = true;
    //     BattleManager.Instance.BattleTurn = Turn.None;
    // }

    // /// <summary>
    // /// 玩家按下逃跑
    // /// </summary>
    // public void FleeButtonDown()
    // {
    //     //该回合结束
    //     BattleManager.Instance.thisTurnOver = true;
    //     BattleManager.Instance.BattleTurn = Turn.End;
    // }

    // /// <summary>
    // /// 玩家使用物品
    // /// </summary>
    // /// <param name="usedItem">被使用的物品</param>
    // private void OnPlayerUseItem(ItemDetials usedItem)
    // {
    //     // 使用物品
    //     BattleManager.Instance.PlayerUseItem(usedItem);
    //     //玩家血量更新
    //     nowIsEnemyTurn?.Invoke();

    //     // 该回合结束
    //     BattleManager.Instance.thisTurnOver = true;
    //     BattleManager.Instance.BattleTurn = Turn.None;
    // }


    // /// <summary>
    // /// buff状态更新
    // /// </summary>
    // private void UpdataBuff()
    // {
    //     //角色buff减少一回合
    //     if(thisTurnCharacter.buff.remaining != 0)
    //     {
    //         // 执行buff效果
    //         switch (thisTurnCharacter.buff.type)
    //         {
    //             case BuffType.Treatment:
    //                 BattleManager.Instance.Treatment(thisTurnCharacter.buff.buffAttribute);
    //                 nowIsEnemyTurn?.Invoke();
    //                 break;
    //             case BuffType.Speed:

    //                 break;
    //         }
    //         // buff剩余回合-1
    //         thisTurnCharacter.buff.remaining--;
    //     }
    //     else
    //     {
    //         // buff归零则重置玩家基础属性
    //         BattleManager.Instance.BuffReset();
    //     }
    // }
    

}