using UnityEngine;

public class BattleTrigger : MonoBehaviour
{
    [SerializeField]private string battleBack;
    public EnemyTeam_SO enemyTeam;

    private bool isTriggered;    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isTriggered == false)
        {
            isTriggered = true;
            EventHandler.CallBattleStartEvent(battleBack,enemyTeam);
            EventHandler.CallClosePlayerMoveEvent();
            transform.gameObject.SetActive(false);
        }
        
    }


}
