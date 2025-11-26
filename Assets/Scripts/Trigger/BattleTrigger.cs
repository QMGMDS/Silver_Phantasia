using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class BattleTrigger : MonoBehaviour
{
    [SerializeField]private string battleBack;
    public BattleAttributeDataList_SO enemyTeam;



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EventHandler.CallBattleStartEvent(battleBack,enemyTeam);
            EventHandler.CallClosePlayerMoveEvent();
        }
        
    }


}
