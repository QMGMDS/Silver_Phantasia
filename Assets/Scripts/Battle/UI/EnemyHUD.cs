using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnemyHUD : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public int enemyStandID;
    private Image enemyImage;
    public GameObject enemyHUD;
    private RectTransform rectTransform;


    private void Awake()
    {
        enemyImage = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        EventHandler.Battle_ShowPrepareAttack += OnBattle_ShowPrepareAttack;
        EventHandler.Battle_ShowItemChoose += OnBattle_ShowItemChoose;
        EventHandler.Battle_AllQuitPrepare += OnBattle_AllQuitPrepare;
        InitEnemyShow();
        
    }

    private void OnDisable()
    {
        EventHandler.Battle_ShowPrepareAttack -= OnBattle_ShowPrepareAttack;
        EventHandler.Battle_ShowItemChoose -= OnBattle_ShowItemChoose;
        EventHandler.Battle_AllQuitPrepare -= OnBattle_AllQuitPrepare;
    }

    private void InitEnemyShow()
    {
        if(enemyStandID < BattleManager.Instance.allBattleARB.Count)
        {
            Debug.Log(BattleManager.Instance.allBattleARB.Count);
            enemyImage.enabled = true;
            rectTransform.sizeDelta = BattleManager.Instance.enemyTeam_SO.FromStandIDToFindEnemy(enemyStandID).spriteSize;
            enemyImage.sprite = BattleManager.Instance.enemyTeam_SO.FromStandIDToFindEnemy(enemyStandID).roleSprite;
            enemyHUD.SetActive(false);
            enemyImage.raycastTarget = false;
        }
        else
        {
            enemyImage.enabled = false;
            enemyHUD.SetActive(false);
            enemyImage.raycastTarget = false;
        }
    }

    private void OnBattle_AllQuitPrepare()
    {
        enemyImage.raycastTarget = false;
    }

    private void OnBattle_ShowPrepareAttack()
    {
        enemyImage.raycastTarget = true;
    }

    private void OnBattle_ShowItemChoose()
    {
        enemyImage.raycastTarget = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        enemyHUD.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        enemyHUD.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        enemyHUD.SetActive(false);
        // 玩家攻击(选择)该目标
        BattleManager.Instance.attackedEnemy_StandID = enemyStandID;
        BattleManager.Instance.playerMakeOperation = true;
        // 所有目标射线检测关闭
        EventHandler.CallBattle_AllQuitPrepare();
    }

}
