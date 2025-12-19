using System.Drawing.Printing;
using UnityEngine;

public class OrganControl : MonoBehaviour
{
    [Header("该机关的ID")]
    public int thisOrganID;
    [Header("机关数据库")]
    public Organ_SO organ_SO;
    [Header("机关打开的图片")]
    public Sprite openSprite;

    [Header("该机关触发条件")]
    public int organ1ID;
    private bool organ1ID_OK;
    

    private Animator organAnim;
    private SpriteRenderer sprite;

    //该机关是否可交互
    private bool canInteract;
    //该机关是否被触发
    private bool thisOrganOpened;

    private void Awake()
    {
        organAnim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        EventHandler.InteractButtonStartEvent += OnOrganOpenEvent;
        organ1ID_OK = organ_SO.FromIDToFindOrgan(organ1ID).isOpen;

        thisOrganOpened = organ_SO.FromIDToFindOrgan(thisOrganID).isOpen;
        if(thisOrganOpened)
            OrganAlwaysOpen();
    }

    private void OnDisable()
    {
        EventHandler.InteractButtonStartEvent -= OnOrganOpenEvent;
    }

    /// <summary>
    /// 空格键按下
    /// </summary>
    private void OnOrganOpenEvent()
    {
        if (canInteract && organ1ID_OK)
        {
            OrganOpen();
        }
    }



    /// <summary>
    /// 机关启动！
    /// </summary>
    private void OrganOpen()
    {
        canInteract = false;
        thisOrganOpened = true;
        organAnim.SetTrigger("Open");
        organAnim.SetBool("IsOpen",true);
        organ_SO.FromIDToFindOrgan(thisOrganID).isOpen = true;
        EventHandler.CallPlayerSign(false);
    }


    /// <summary>
    /// 机关恒启动
    /// </summary>
    private void OrganAlwaysOpen()
    {
        organAnim.enabled = false;
        sprite.sprite = openSprite;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(thisOrganOpened == false && organ1ID_OK) // 机关没被触发并且对应剧情完成才会显示提示Sign
        {
            canInteract = true;
            EventHandler.CallPlayerSign(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        canInteract = false;
        EventHandler.CallPlayerSign(false);
    }

}
