using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    //临时存储，表明当前HUD是thisCharacter的HUD
    public BattleAttribute thisCharacter;

    [Header("HUD辨别填写")]
    //是否为Player的HUD，确定阵营
    public bool isPlayerHUD;
    //站位编号
    public int ID;

    [Header("PlayerHUD要赋值")]
    //PlayerHUD的激活和关闭
    public GameObject playerHUD;
    public Image playerImage;

    [Header("EnemyHUD要赋值")]
    public GameObject enemyHUD;
    public Image enemyImage;

    /// <summary>
    /// 初始化HUD，由DisplayUI在战斗开始时调用一次，告诉当前的HUD是哪个角色的HUD
    /// </summary>
    public void InitHUD()
    {
        //先判断该HUD是PlayerHUD还是EnemyHUD（判断阵营）
        if (isPlayerHUD == true)
        {
            //判断站位编号
            foreach (var player in BattleManager.Instance.playerTeam.AttributesList)
            {
                if(player.roleID == ID)
                {
                    //存起来
                    thisCharacter = player;
                    //显示图片
                    playerImage.enabled = true;
                    playerImage.sprite = thisCharacter.roleSprite;
                    //激活HUD
                    playerHUD.SetActive(true);
                }
            }
        }
        else
        {
            //判断站位编号
            if(BattleManager.Instance.enemyTeam[0] == null)
            {
                Debug.Log("空");
            }
            // foreach (var enemy in BattleManager.Instance.enemyTeam)
            // {
            //     Debug.Log("aa");
            //     if(enemy.roleID == ID)
            //     {
            //         Debug.Log("aaa");
            //         //存起来
            //         thisCharacter = enemy;
            //         //显示图片
            //         enemyImage.enabled = true;
            //         enemyImage.sprite = thisCharacter.roleSprite;
            //     }
            // }
        }
    }

    //玩家回合的触发事件
    //2.开启对应玩家显示图片闪烁
    public void isYourTurnImage()
    {
        if(BattleManager.Instance.thisCharacterTurn != thisCharacter)
            return;
        if(BattleManager.Instance.BattleTurn == Turn.Player)
            playerImage.GetComponent<Animator>().enabled = true;
        else
            playerImage.GetComponent<Animator>().enabled = false;
    }

    //关闭对应玩家显示图片闪烁
    public void CloseisYourTurnImage()
    {
        if(BattleManager.Instance.thisCharacterTurn != thisCharacter)
            return;
        playerImage.GetComponent<Animator>().enabled = false;
        playerImage.color = new Color(255,255,255,255);
    }


    //按钮按下了表示确认了对应的行动
    //攻击键被按下：2.开启敌人图片的射线检测，允许预览攻击
    public void AllowEnemyPreview()
    {
        enemyImage.raycastTarget = true;
    }

    //鼠标按下事件
    //2.关闭敌人图片的射线检测，不允许预览攻击
    public void NotAllowEnemyPreview()
    {
        enemyImage.raycastTarget = false;
    }


    //鼠标射线检测到AttackPreviewController的图片时，开启敌人预览
    //鼠标移入事件，显示敌人状态预览
    public void OpenAttackPreview()
    {
        enemyHUD.SetActive(true);
    }

    //鼠标移出事件，关闭敌人状态预览
    //鼠标按下事件
    //1.关闭敌人状态预览
    public void CloseAttackPreview()
    {
        enemyHUD.SetActive(false);
    }

    //玩家血量UI更新
    public void UpdatePlayerHUD()
    {
        playerHUD.SetActive(false);
        playerHUD.SetActive(true);
    }


    //鼠标按下事件
    //3.攻击敌人（确认攻击对象）
    public void PlayerAttackToThis()
    {
        BattleManager.Instance.attackedCharacter = thisCharacter;
    }
}
