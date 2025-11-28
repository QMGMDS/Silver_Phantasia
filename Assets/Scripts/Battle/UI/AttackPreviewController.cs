using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class AttackPreviewController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    //鼠标移入事件，显示敌人状态预览
    public UnityEvent mouseEnter;
    //鼠标移出事件，关闭敌人状态预览
    public UnityEvent mouseQuit;
    //鼠标按下事件
    //1.关闭敌人预览
    //2.关闭敌人图片的射线检测，不允许预览攻击
    //3.攻击敌人
    public UnityEvent mosueDown;

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseQuit?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        mosueDown?.Invoke();
    }
}
