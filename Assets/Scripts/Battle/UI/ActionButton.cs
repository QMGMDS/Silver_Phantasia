using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ActionButton : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    //该按钮的类型
    public ButtonType buttonType;
    private Image highlight;

    //按钮按下时的触发事件
    public UnityEvent buttonStarted;


    //父物体Action被激活时（玩家可操作时）一开始就执行的方法
    private void Awake()
    {
        highlight = transform.GetChild(0).GetComponent<Image>();
    }

    private void Start()
    {
        highlight.enabled = false;
    }


    /// <summary>
    /// 鼠标进入
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        highlight.enabled = true;
    }

    /// <summary>
    /// 鼠标离开
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        highlight.enabled = false;
    }

    //鼠标按下
    public void OnPointerClick(PointerEventData eventData)
    {
        highlight.enabled = false;
        //告诉BattleManager现在是什么按钮模式
        BattleManager.Instance.currentButtonType = buttonType;

        //按钮按下了表示确认了对应的行动
        //1.关闭Action
        //攻击键被按下：2.开启敌人图片的射线检测，允许预览攻击
        buttonStarted?.Invoke();
    }

}