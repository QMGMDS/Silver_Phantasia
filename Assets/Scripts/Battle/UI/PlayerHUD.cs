using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public PlayerBattleARB_SO playerBattleARB_SO;


    public Image BraveImage;
    public TextMeshProUGUI BraveName;
    public Image BraveHPFill;
    public TextMeshProUGUI BraveHPText;

    public GameObject chooseSign;

    public Image buffImage;



    private void OnEnable()
    {
        EventHandler.Battle_PlayerHUDUpdate += OnBattle_PlayerHUDUpdate;
        EventHandler.Battle_ShowItemChoose += OnBattle_ShowItemChoose;
        EventHandler.Battle_AllQuitPrepare += OnBattle_AllQuitPrepare;

        InitHUD();
    }

    private void OnDisable()
    {
        EventHandler.Battle_PlayerHUDUpdate -= OnBattle_PlayerHUDUpdate;
        EventHandler.Battle_ShowItemChoose -= OnBattle_ShowItemChoose;
        EventHandler.Battle_AllQuitPrepare -= OnBattle_AllQuitPrepare;
    }

    private void OnBattle_AllQuitPrepare()
    {
        BraveImage.raycastTarget = false;
    }


    private void OnBattle_ShowItemChoose()
    {
        BraveImage.raycastTarget = true;
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
        if(BattleManager.Instance.allBattleARB[0].buff.remaining != 0)
        {
            buffImage.enabled = true;
            buffImage.sprite = BattleManager.Instance.allBattleARB[0].buff.sprite;
        }
        else
        {
            buffImage.enabled = false;
        }
        
        BraveHPFill.fillAmount = (float)player.currentHP / player.baseHP;
        BraveHPText.text = player.currentHP + "/" + player.baseHP;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        chooseSign.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        chooseSign.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        chooseSign.SetActive(false);
        // 玩家选择该目标
        BattleManager.Instance.playerMakeOperation = true;
        // 所有目标射线检测关闭
        EventHandler.CallBattle_AllQuitPrepare();
    }
}
