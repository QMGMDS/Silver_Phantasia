using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyAdditionalHUD : MonoBehaviour
{
    //谁的HUD
    public BattleAttribute enemy;


    //角色名称
    public TextMeshProUGUI enemyName;
    //角色血量填充图片
    public Image enemyHP;


    //每次EnemyHUD被激活时更新里面的数据
    private void OnEnable()
    {
        enemy = transform.GetComponentInParent<BattleHUD>().thisCharacter;
        UpdateHUD();
    }

    private void UpdateHUD()
    {
        enemyName.text = enemy.roleName;
        enemyHP.fillAmount = (float)enemy.currentHP / enemy.maxHP;
    }
    
}
