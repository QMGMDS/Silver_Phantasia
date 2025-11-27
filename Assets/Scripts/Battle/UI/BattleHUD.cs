using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    //临时存储
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
    //玩家队伍的信息，手动赋值
    public BattleAttributeDataList_SO playerTeam;
    
    [Header("EnemyHUD需要额外赋值的")]
    public Image back;


    /// <summary>
    /// 初始化HUD，在DisplayUI调用
    /// </summary>
    public void InitHUD(BattleAttributeDataList_SO enemyTeam)
    {
        //先判断该HUD是PlayerHUD还是EnemyHUD（判断阵营）
        if (isPlayerHUD == true)
        {
            //判断站位编号
            foreach (var player in playerTeam.AttributesList)
            {
                if(player.roleID == ID)
                {
                    //存起来
                    thisCharacter = player;
                    //UI初始化更新
                    characterName.text = thisCharacter.roleName;
                    characterHPNum.text = thisCharacter.currentHP + "/" + thisCharacter.maxHP;
                    characterHP.fillAmount = (float)thisCharacter.currentHP/thisCharacter.maxHP;
                    characterImage.sprite = thisCharacter.roleSprite;
                }
            }

        }
        else
        {
            //判断站位编号
            foreach (var enemy in enemyTeam.AttributesList)
            {
                if(enemy.roleID == ID)
                {
                    //存起来
                    thisCharacter = enemy;
                    //UI初始化更新
                    characterName.text = thisCharacter.roleName;
                    characterHP.fillAmount = (float)thisCharacter.currentHP/thisCharacter.maxHP;
                    characterImage.sprite = thisCharacter.roleSprite;
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
        }
    }

    
}
