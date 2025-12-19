using UnityEngine;

public class BattleUI : MonoBehaviour
{
    // 玩家操作UI
    public GameObject AllButton;
    // 玩家操作提示
    public GameObject Tip;

    
    private void OnEnable()
    {

        EventHandler.Battle_ShowPrepareAttack += OnBattle_ShowPrepareAttack;
    }

    private void OnDisable()
    {

        EventHandler.Battle_ShowPrepareAttack -= OnBattle_ShowPrepareAttack;
    }


    /// <summary>
    /// 关闭玩家操作UI————Action，打开提示面板
    /// </summary>
    private void OnBattle_ShowPrepareAttack()
    {
        Tip.SetActive(true);
        AllButton.SetActive(false);
    }

    /// <summary>
    /// 显示玩家操作UI————Action
    /// </summary>
    public void Battle_ShowPlayerAction()
    {
        Tip.SetActive(false);
        AllButton.SetActive(true);
    }

    

}
