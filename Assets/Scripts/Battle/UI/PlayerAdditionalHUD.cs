using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PlayerAdditionalHUD : MonoBehaviour
{
    public BattleAttribute player;

    //角色名称
    public TextMeshProUGUI playerName;
    //角色血量数字显示
    public TextMeshProUGUI playerHPNum;
    //角色血量填充图片
    public Image playerHP;

    //背景图
    public Image HUDBack;
    public RectTransform HUDBackRec;
    public RectTransform HUDNameRec;



    private void OnEnable()
    {
        player = transform.GetComponentInParent<BattleHUD>().thisCharacter;
        UpdateHUD();
    }


    private void UpdateHUD()
    {
        playerName.text = player.roleName;
        playerHPNum.text = player.currentHP + "/" + player.maxHP;
        playerHP.fillAmount = (float)player.currentHP/player.maxHP;
    }


    //nowIsplayerTurn玩家回合的触发事件
    //1.对应玩家角色HUD高亮
    public void isYourTurnHUD()
    {
        if(BattleManager.Instance.thisCharacterTurn == player)
        {
            HUDBack.DOColor(new Color(255,255,255,255),3f);
            Debug.Log("高亮显示力");

            HUDBackRec.sizeDelta = new Vector2(HUDBackRec.sizeDelta.x,210);
            HUDNameRec.position = new Vector2(HUDNameRec.position.x,160);
            Debug.Log("被拉伸力");
        }
    }
}
