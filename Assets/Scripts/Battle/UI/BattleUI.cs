using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class BattleUI : MonoBehaviour
{
    public GameObject Action;


    private void OnEnable()
    {
        //TODO:战斗行动轴排序到玩家角色时触发玩家行动，开启玩家ActionUI
    }

    private void OnDisable()
    {
        
    }


    /// <summary>
    /// 关闭玩家操作UI
    /// </summary>
    public void CloseActionUI()
    {
        Action.SetActive(true);
    }


}
