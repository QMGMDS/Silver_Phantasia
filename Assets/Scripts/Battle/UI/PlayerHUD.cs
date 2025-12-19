using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public PlayerBattleARB_SO playerBattleARB_SO;


    public Image BraveImage;
    public TextMeshProUGUI BraveName;
    public Image BraveHPFill;
    public TextMeshProUGUI BraveHPText;



    private void OnEnable()
    {
        EventHandler.Battle_PlayerHUDUpdate += OnBattle_PlayerHUDUpdate;

        InitHUD();
    }

    private void OnDisable()
    {
        EventHandler.Battle_PlayerHUDUpdate += OnBattle_PlayerHUDUpdate;
    }

    /// <summary>
    /// 更新玩家HUD
    /// </summary>
    private void OnBattle_PlayerHUDUpdate()
    {
        UpdateHUD(BattleManager.Instance.allBattleARB[0]);
    }

    private void InitHUD()
    {
        BraveImage.sprite = playerBattleARB_SO.playerBattleARB.roleSprite;
        BraveName.text = playerBattleARB_SO.playerBattleARB.roleName;
        BraveHPFill.fillAmount = (float)playerBattleARB_SO.playerBattleARB.currentHP / playerBattleARB_SO.playerBattleARB.baseHP;
        BraveHPText.text = playerBattleARB_SO.playerBattleARB.currentHP + "/" + playerBattleARB_SO.playerBattleARB.baseHP;
    }

    private void UpdateHUD(BattleARB player)
    {
        BraveHPFill.fillAmount = (float)player.currentHP / player.baseHP;
        BraveHPText.text = player.currentHP + "/" + player.baseHP;
    }
}
