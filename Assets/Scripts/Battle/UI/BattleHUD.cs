using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    //临时存储，表明当前HUD是thisCharacter的HUD
    private BattleAttribute thisCharacter;

    //是否为Player的HUD，确定阵营
    public bool isPlayerHUD;
    //编号，确定站位
    public int ID;

    //角色名称
    public TextMeshProUGUI characterName;
    //角色血量填充图片
    public Image characterHP;
    //角色显示图片
    public Image characterImage;

    [Header("EnemyHUD不用赋值")]
    //角色血量数字显示
    public TextMeshProUGUI characterHPNum;
    //PlayerHUD的激活和关闭
    public GameObject playerHUD;
    
    [Header("EnemyHUD需要额外赋值的")]
    public Image back;
    public Image frame;


    /// <summary>
    /// 初始化HUD，在DisplayUI调用，告诉当前的HUD是哪个角色的HUD
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
                    playerHUD.SetActive(true);
                    //存起来
                    thisCharacter = player;
                    //UI初始化更新
                    characterName.text = thisCharacter.roleName;
                    characterHPNum.text = thisCharacter.currentHP + "/" + thisCharacter.maxHP;
                    characterHP.fillAmount = (float)thisCharacter.currentHP/thisCharacter.maxHP;
                    characterImage.enabled = true;
                    characterImage.sprite = thisCharacter.roleSprite;
                }
            }

        }
        else
        {
            //判断站位编号
            foreach (var enemy in BattleManager.Instance.enemyTeam)
            {
                if(enemy.roleID == ID)
                {
                    //存起来
                    thisCharacter = enemy;
                    //UI初始化更新
                    characterName.text = thisCharacter.roleName;
                    characterHP.fillAmount = (float)thisCharacter.currentHP/thisCharacter.maxHP;
                    characterImage.enabled = true;
                    characterImage.sprite = thisCharacter.roleSprite;
                    //顺手为AttackSignController绑定对应Character
                    transform.GetComponent<AttackSignController>().thisEnemy = thisCharacter;
                }
            }
        }
    }

    /// <summary>
    /// 开启EnemyHUD显示（除去角色的显示图片）
    /// </summary>
    public void OpenEnemyHUD()
    {
        if (isPlayerHUD == false)
        {
            back.color = new Color(255,255,255,255);
            characterName.color = new Color(255,255,255,255);
            characterHP.color = new Color(255,255,255,255);
            frame.color = new Color(255,255,255,255);
        }
    }

    /// <summary>
    /// 关闭EnemyHUD显示（除去角色的显示图片）
    /// </summary>
    public void CloseEnemyHUD()
    {
        if (isPlayerHUD == false)
        {
            back.color = new Color(255,255,255,0);
            characterName.color = new Color(255,255,255,0);
            characterHP.color = new Color(255,255,255,0);
            frame.color = new Color(255,255,255,0);
        }
    }

    /// <summary>
    /// 更新HUD
    /// </summary>
    public void UpdateHUD()
    {
        //先判断thisCharacter有没有被成功赋值
        if(thisCharacter == null)
            return;

        //先从BattleManager中拿取最新的角色数据
        foreach (var item in BattleManager.Instance.battleList)
        {
            if (thisCharacter == item)
                thisCharacter = item;
        }

        if (thisCharacter.isPlayer)
        {
            characterHPNum.text = thisCharacter.currentHP + "/" + thisCharacter.maxHP;
            characterHP.fillAmount = (float)thisCharacter.currentHP/thisCharacter.maxHP;
        }
        else
        {
            characterHP.fillAmount = (float)thisCharacter.currentHP/thisCharacter.maxHP;
        }
    }
}
