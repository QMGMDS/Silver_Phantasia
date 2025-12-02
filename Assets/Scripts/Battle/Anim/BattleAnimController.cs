using System.Collections;
using UnityEngine;

public class BattleAnimController : MonoBehaviour
{
    public BattleAnim playerBattleAnimOne;
    public BattleAnim playerBattleAnimTwo;
    public BattleAnim enemyBattleAnimOne;


    private void Awake()
    {
        playerBattleAnimOne.image.enabled = false;
        enemyBattleAnimOne.image.enabled =false;
    }

    public void PlayerAttackAnim()
    {
        //玩家动画进入攻击状态
        playerBattleAnimOne.anim.SetFloat("Normal",0f);
        playerBattleAnimTwo.anim.SetFloat("Normal",0f);
        //敌人动画进入受伤状态
        enemyBattleAnimOne.anim.SetFloat("Normal",1f);
        switch (BattleManager.Instance.thisCharacterTurn.roleID)
        {
            case 0:
                StartCoroutine(PlayerAttackOne());
                break;
            case 1:
                StartCoroutine(PlayerAttackTwo());
                break;
        }
    }


    public void EnemyAttackAnim()
    {
        //玩家动画进入受伤状态
        playerBattleAnimOne.anim.SetFloat("Normal",1f);
        playerBattleAnimTwo.anim.SetFloat("Normal",1f);
        //敌人动画进入攻击状态
        enemyBattleAnimOne.anim.SetFloat("Normal",0f);
        
        StartCoroutine(EnemyAttackOne());
    }


    private IEnumerator PlayerAttackOne()
    {
        playerBattleAnimOne.image.enabled = true;
        yield return new WaitForSeconds(1f);
        playerBattleAnimOne.image.enabled = false;

        enemyBattleAnimOne.anim.SetFloat("Hurt",0f);
        enemyBattleAnimOne.image.enabled = true;
        yield return new WaitForSeconds(1f);
        enemyBattleAnimOne.image.enabled = false;
    }

    private IEnumerator PlayerAttackTwo()
    {
        playerBattleAnimTwo.image.enabled = true;
        yield return new WaitForSeconds(1f);
        playerBattleAnimTwo.image.enabled = false;

        enemyBattleAnimOne.anim.SetFloat("Hurt",1f);
        enemyBattleAnimOne.image.enabled = true;
        yield return new WaitForSeconds(1f);
        enemyBattleAnimOne.image.enabled = false;
    }

    private IEnumerator EnemyAttackOne()
    {
        enemyBattleAnimOne.anim.SetFloat("Attack",0f);
        enemyBattleAnimOne.image.enabled = true;
        yield return new WaitForSeconds(1f);
        enemyBattleAnimOne.image.enabled = false;

        playerBattleAnimOne.image.enabled = true;
        yield return new WaitForSeconds(1f);
        playerBattleAnimOne.image.enabled = false;
    }

    private IEnumerator EnemyAttackTwo()
    {
        enemyBattleAnimOne.anim.SetFloat("Attack",0f);
        enemyBattleAnimOne.image.enabled = true;
        yield return new WaitForSeconds(1f);
        enemyBattleAnimOne.image.enabled = false;

        playerBattleAnimTwo.image.enabled = true;
        yield return new WaitForSeconds(1f);
        playerBattleAnimTwo.image.enabled = false;
    }



}
