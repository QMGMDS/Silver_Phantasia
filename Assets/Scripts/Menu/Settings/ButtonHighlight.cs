using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHighlight : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("高亮图片")]
    public Sprite highlightSprite;

    private Image highlightImage;

    //确保只设置一次高亮图片
    private bool isInit;
    //鼠标按下是否保持高亮
    [SerializeField]private bool clickStay;
    //鼠标是否按下
    private bool clicked;
    //父级背包
    private BagControl parentBag;

    private void Awake()
    {
        highlightImage = transform.GetChild(0).GetComponent<Image>();
        highlightImage.enabled = false;
        parentBag = transform.GetComponentInParent<BagControl>();
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        // 该键不被按下时鼠标的移入才会显示高亮
        if(!clicked)
        {
            highlightImage.enabled = true;
        }
        if(isInit == false)
        {
            highlightImage.sprite = highlightSprite;
            isInit = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 该键不被按下时鼠标的移开会关闭高亮
        if (!clicked)
        {
            highlightImage.enabled = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickStay)
        {
            highlightImage.enabled = true;
            parentBag.IsChoose = true;
            clicked = true;
        }
        else
        {
            highlightImage.enabled = false;
        }
    }


}
