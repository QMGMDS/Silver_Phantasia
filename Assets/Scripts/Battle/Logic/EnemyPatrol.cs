using System.Collections;
using System.Linq.Expressions;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private NPCMovement NPCMovement;

    [SerializeField]private Vector3Int patrolStartPos;
    [SerializeField]private Vector3Int patrolTargetPos;



    private void Awake()
    {
        NPCMovement = GetComponent<NPCMovement>();
    }

    private void OnEnable()
    {
        StartCoroutine(EnemyAllowsPatrol());
    }


    /// <summary>
    /// 敌人持续巡逻
    /// </summary>
    /// <returns></returns>
    private IEnumerator EnemyAllowsPatrol()
    {
        yield return new WaitForSeconds(2f);
        Vector3Int temp;
        while (true)
        {
            NPCMovement.startGridPosition = patrolStartPos;
            NPCMovement.targetGridPosition = patrolTargetPos;
            NPCMovement.InitNPC();
            StartCoroutine(NPCMovement.Movement());
            yield return new WaitUntil(() => NPCMovement.moveToTarget);

            temp = patrolStartPos;
            patrolStartPos = patrolTargetPos;
            patrolTargetPos = temp;
        }
    }

}
