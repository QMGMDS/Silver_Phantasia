using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class BattleUI : MonoBehaviour
{
    public GameObject Action;
    //用于存储当前回合的目标角色属性
    private BattleAttribute thisCharacter;
    private bool isAction;


    private void Awake()
    {
        //战斗行动轴排序到玩家角色时触发玩家行动，开启玩家ActionUI
        thisCharacter = BattleManager.Instance.isWhoTurn();
        Debug.Log(thisCharacter.roleName);
        //是玩家的话就打开玩家操作面板
        if (thisCharacter.isPlayer)
        {
            isAction = true;
            Action.SetActive(true);
        }
        else
        {
            isAction = false;
            Action.SetActive(false);
        }
    }



}
