using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BattleBackControl : MonoBehaviour
{
    //战斗背景仓库
    public BattleBack_SO battleBack_SO;
    //用来显示战斗场景的子物体
    private Image battleBackImage;


    private void Awake()
    {
        battleBackImage = transform.GetChild(0).gameObject.GetComponent<Image>();
        //默认关闭战斗背景图片
        battleBackImage.enabled = false;
    }

    private void OnEnable()
    {
        EventHandler.BattleStartEvent += OnBattleStartEvent;
        EventHandler.BattleEndEvent += OnBattleEndEvent;
    }

    private void OnDisable()
    {
        EventHandler.BattleStartEvent -= OnBattleStartEvent;
        EventHandler.BattleEndEvent += OnBattleEndEvent;
    }

    

    /// <summary>
    /// 战斗开始————显示战斗背景画布
    /// </summary>
    /// <param name="battleBack"></param>
    private void OnBattleStartEvent(string battleBack,BattleAttributeDataList_SO enemyTeam)
    {
        if(battleBackImage != null)
            battleBackImage.sprite = GetBattleBack(battleBack).backImage;
        battleBackImage.enabled = true;
    }

    private BattleBack GetBattleBack(string battleBack)
    {
        //根据名字找到对应的BattleBack
        //如果找到就返回对应的BattleBack，如果没找到就返回null
        return battleBack_SO.battleBacksList.Find(i => i.backName == battleBack);
    }

    private void OnBattleEndEvent()
    {
        StartCoroutine(BattleEnd());
    }

    private IEnumerator BattleEnd()
    {
        yield return new WaitForSeconds(2f);
        battleBackImage.enabled = false;
    }
}
