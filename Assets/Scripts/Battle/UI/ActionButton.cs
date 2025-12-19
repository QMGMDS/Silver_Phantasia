using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ActionButton : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public PlayerChooseAction thisButtonType;
    [SerializeField]private Image highlight;

    //按钮按下时的触发事件
    public UnityEvent ButtonStarted;

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

        switch (thisButtonType)
        {
            case PlayerChooseAction.Attack:
                EventHandler.CallBattle_ShowPrepareAttack();
                BattleManager.Instance.playerChooseAction = PlayerChooseAction.Attack;
                break;
        }
    }

}