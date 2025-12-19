using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyTeam_SO", menuName = "Battle/EnemyTeam_SO")]
public class EnemyTeam_SO : ScriptableObject
{
    public List<EnemyBattleARB> enemyTeam;



    public EnemyBattleARB FromStandIDToFindEnemy(int standID)
    {
        foreach (var enemy in enemyTeam)
        {
            if(standID == enemy.enemyStandID)
            {
                return enemy;
            }
        }
        return null;
    }

}
