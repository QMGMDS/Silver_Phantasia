using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler 
{
    //对话选项提示光标
    private Image optionTip;

    private void Awake()
    {
        optionTip = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        optionTip.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        optionTip.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        optionTip.enabled = false;
    }
}
