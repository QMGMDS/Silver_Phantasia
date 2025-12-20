using System.Collections;
using Unity.VisualScripting;
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


        // 等待玩家做出操作(抵达操作终点)
        yield return new WaitUntil(() => BattleManager.Instance.playerMakeOperation);


        switch (BattleManager.Instance.playerChooseAction)
        {
            case PlayerChooseAction.Attack:
                BattleManager.Instance.PlayerAttack();
                break;
            case PlayerChooseAction.Item:
                BattleManager.Instance.PlayerUseItem();
                break;
            case PlayerChooseAction.Skill:
                EventHandler.CallPlotDialogueEvent(7);
                break;
            case PlayerChooseAction.Flee:
                EventHandler.CallPlotDialogueEvent(6);
                break;
        }

        //更新Buff
        BattleManager.Instance.UpdataAllBuff();

        //更新玩家HUD
        EventHandler.CallBattle_PlayerHUDUpdate();


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

    

}