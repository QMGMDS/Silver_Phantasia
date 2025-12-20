using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ActionButton : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public PlayerChooseAction thisButtonType;
    [SerializeField]private Image highlight;

    private void OnEnable()
    {
        highlight.enabled = false;
    }



    /// <summary>
    /// 鼠标进入
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        highlight.enabled = true;
    }

    /// <summary>
    /// 鼠标离开
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        highlight.enabled = false;
    }

    /// <summary>
    /// 鼠标按下
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        highlight.enabled = false;
        // 玩家选择对应操作

        switch (thisButtonType)
        {
            case PlayerChooseAction.Attack:
                EventHandler.CallBattle_ShowPrepareAttack();
                BattleManager.Instance.playerChooseAction = PlayerChooseAction.Attack;
                break;
            case PlayerChooseAction.Item:
                EventHandler.CallBattle_ShowPrepareItemUse();
                BattleManager.Instance.playerChooseAction = PlayerChooseAction.Item;
                break;
            case PlayerChooseAction.Skill:
                BattleManager.Instance.playerMakeOperation = true;
                BattleManager.Instance.playerChooseAction = PlayerChooseAction.Skill;
                break;
            case PlayerChooseAction.Flee:
                BattleManager.Instance.playerMakeOperation = true;
                BattleManager.Instance.playerChooseAction = PlayerChooseAction.Flee;
                break;
        }
    }

}