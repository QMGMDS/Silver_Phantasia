using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyAdditionalHUD : MonoBehaviour
{
    // 敌人站位
    public int enemyStandID;
    //角色名称
    public TextMeshProUGUI enemyName;
    //角色血量填充图片
    public Image enemyHP;


    //每次EnemyHUD被激活时更新里面的数据
    private void OnEnable()
    {
        UpdateHUD();
    }

    private void UpdateHUD()
    {
        enemyName.text = BattleManager.Instance.enemyTeam_SO.FromStandIDToFindEnemy(enemyStandID).roleName;
        enemyHP.fillAmount = (float)BattleManager.Instance.allBattleARB[enemyStandID].currentHP / BattleManager.Instance.allBattleARB[enemyStandID].baseHP;
    }
    
}
