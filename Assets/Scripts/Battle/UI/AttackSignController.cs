using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackSignController : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private GameObject attackSign;
    //Enemy身上的显示图片（因为图片射线检测范围大）
    private Image signImageParent;

    //Enemy的HUD，用来显示关闭HUD
    private BattleHUD EnemyHUD;



    private void Awake()
    {
        attackSign = transform.GetChild(0).gameObject;
        signImageParent = transform.GetComponent<Image>();
        EnemyHUD = transform.GetComponent<BattleHUD>();
    }

    /// <summary>
    /// 允许显示预攻击，由攻击键Button按下或者技能选择后调用
    /// </summary>
    public void entryPrepareAttack()
    {
        signImageParent.raycastTarget = true;
    }
    
    /// <summary>
    /// 不允许显示预攻击，由按下攻击对象调用
    /// </summary>
    private void quitPrepareAttack()
    {
        signImageParent.raycastTarget = false;
    }



    
    //鼠标进入，打开预攻击显示提示
    public void OnPointerEnter(PointerEventData eventData)
    {
        attackSign.SetActive(true);
        EnemyHUD.OpenEnemyHUD();
    }

    //鼠标离开，关闭预攻击显示提示
    public void OnPointerExit(PointerEventData eventData)
    {
       attackSign.SetActive(false);
       EnemyHUD.CloseEnemyHUD();
    }

    //鼠标按下，进行攻击
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("打它");
        quitPrepareAttack();

        //调用BattleManager的玩家攻击攻击
        BattleManager.Instance.PlayerAttack();
    }

}
