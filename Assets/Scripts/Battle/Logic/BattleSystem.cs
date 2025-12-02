using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//BattleSystem处理战斗的逻辑

public class BattleSystem : MonoBehaviour
{
    //BattleUI组件
    private BattleUI battleUI;

    //当前回合的战斗角色
    private BattleAttribute thisTurnCharacter;




    //玩家回合的触发事件
    //1.对应玩家角色HUD高亮
    //2.对应玩家显示图片闪烁
    //3.对应玩家操作Action激活
    public UnityEvent nowIsPlayerTurn;


    //敌人回合攻击的触发事件(更新玩家HUD)
    public UnityEvent nowIsEnemyTurn;

    //玩家攻击动画事件
    //1.玩家攻击动画
    //2.敌人受伤动画
    public UnityEvent playerAttackAnimations;

    //敌人攻击动画事件
    //1.敌人攻击动画
    //2.玩家受伤动画
    public UnityEvent EnemyAttackAnimations;


    private void Awake()
    {
        BattleManager.Instance.BattleTurn = Turn.None;
        battleUI = GetComponent<BattleUI>();
    }



    //前方一大波石山来袭()
    private void Update()
    {
        //是否战斗初始化完毕
        if(!BattleManager.Instance.Inited)
            return;
        if(BattleManager.Instance.BattleTurn == Turn.None)
        {
            //1.上个回合结束了才判断下个回合的行动
            if (BattleManager.Instance.thisTurnOver)
            {
                AxisOfAction();
            }
            
            //2.等待行动轴动画完成
            if(BattleManager.Instance.walking)
                return;
            
            //3.当前回合角色血量为0则跳过
            if(thisTurnCharacter.currentHP <= 0)
            {
                BattleManager.Instance.thisTurnOver = true;
                BattleManager.Instance.BattleTurn = Turn.None;
                return;
            }

            //以上条件均通过
            //再判断这个回合是玩家回合还是敌人回合
            if (thisTurnCharacter.isPlayer)
            {
                //玩家的战斗逻辑
                BattleManager.Instance.BattleTurn = Turn.Player;
                //UI提示事件
                nowIsPlayerTurn.Invoke();
                Debug.Log(thisTurnCharacter.roleName);
                //等待玩家操作......
                //当玩家按下Action按钮时，每个按钮对应不同事件
            }
            else
            {
                //敌人的战斗逻辑
                BattleManager.Instance.BattleTurn = Turn.Enemy;
                Debug.Log("敌人攻击");
                EnemyAttack();
            }
        }
        else if(BattleManager.Instance.BattleTurn == Turn.End)
        {
            Debug.Log("战斗结束");
            BattleEnd();
        }
        
    }


    //鼠标按下事件
    //3.攻击敌人(攻击动画，攻击数据处理)
    //切换回合
    public void PlayerAttack()
    {
        StartCoroutine(PlayerAttackAnimAndThenAttack());
    }

    //玩家攻击动画和数据处理
    private IEnumerator PlayerAttackAnimAndThenAttack()
    {
        //播放动画
        playerAttackAnimations?.Invoke();
        //等待动画播放
        yield return new WaitForSeconds(2f);
        //玩家攻击数据处理
        BattleManager.Instance.PlayerAttack();

        //判断战斗是否结束
        if(BattleManager.Instance.BattleEnd() == -1)
        {
            BattleManager.Instance.BattleTurn = Turn.None;
        }
        else
        {
            BattleManager.Instance.BattleTurn = Turn.End;
        }
        //该回合结束
        BattleManager.Instance.thisTurnOver = true;
    }

    public void EnemyAttack()
    {
        StartCoroutine(EnemyAttackAnimAndThenAttack());
    }

    private IEnumerator EnemyAttackAnimAndThenAttack()
    {
        //播放动画
        EnemyAttackAnimations?.Invoke();
        //等待动画播放
        yield return new WaitForSeconds(2f);
        //敌人攻击数据处理
        BattleManager.Instance.EnemyAttack();
        //玩家血量更新
        nowIsEnemyTurn?.Invoke();

        //战斗是否结束
        if(BattleManager.Instance.BattleEnd() == -1)
        {
            BattleManager.Instance.BattleTurn = Turn.None;
        }
        else
        {
            BattleManager.Instance.BattleTurn = Turn.End;
        }
        //该回合结束
        BattleManager.Instance.thisTurnOver = true;
    }


    /// <summary>
    /// 战斗结束的逻辑处理
    /// </summary>
    public void BattleEnd()
    {
        if(BattleManager.Instance.BattleEnd() == 1)
        {
            Debug.Log("玩家胜利");
            //返回
            EventHandler.CallBattleEndEvent();
            EventHandler.CallOpenPlayerMoveEvent();
        }
        else
        {
            Debug.Log("敌人胜利");
            //游戏失败
        }
        //战斗数据复原
        BattleManager.Instance.Recovery();
        //行动轴图片复原
        battleUI.RecoveryWalk();
    }



    /// <summary>
    /// 每个回合开始前判断该回合是谁的回合
    /// </summary>
    private void AxisOfAction()
    {
        //这个回合开始了
        BattleManager.Instance.thisTurnOver = false;
        //轮到谁了？
        thisTurnCharacter = BattleManager.Instance.isWhoTurn();
        //播放动画，动画结束BattleManager.Instance.walking变为false
        BattleManager.Instance.walking = true;
        battleUI.WalkAnimation();
    }

}