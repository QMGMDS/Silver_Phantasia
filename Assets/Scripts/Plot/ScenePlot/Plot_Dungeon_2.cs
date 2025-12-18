using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot_Dungeon_2 : MonoBehaviour
{
    public NPCMovement NPCMovement;
    public GameObject Stranger;

    [Header("机关数据库")]
    public Organ_SO organ_SO;

    // 勇者遇上陌生人的剧情是否触发过
    private bool smallPlotIsTriggered;

    private void OnEnable()
    {
        EventHandler.FindStranger += OnFindStranger;
        smallPlotIsTriggered = organ_SO.fromIDToFindOrgan(1).isOpen;
        StrangerShow();
    }

    private void OnDisable()
    {
        EventHandler.FindStranger -= OnFindStranger;
    }

    private void OnFindStranger()
    {
        StartCoroutine(StrangerQuit());
    }

    /// <summary>
    /// 断桥，陌生人离开
    /// </summary>
    /// <returns></returns>
    private IEnumerator StrangerQuit()
    {
        yield return new WaitForSeconds(1f);
        // 断桥
        EventHandler.CallBridgeBreak();

        yield return new WaitForSeconds(2f);

        NPCMovement.enabled = true;
        NPCMovement.startGridPosition = new Vector3Int(-7,42,0);
        NPCMovement.targetGridPosition = new Vector3Int(-16,43,0);
        NPCMovement.InitNPC();
        StartCoroutine(NPCMovement.Movement());

        yield return new WaitUntil(() => NPCMovement.moveToTarget);
        Stranger.SetActive(false);

        EventHandler.CallDungeon_CameraFindStrangerEnd();

    }

    /// <summary>
    /// 神秘魔理沙是否显示
    /// </summary>
    private void StrangerShow()
    {
        if (smallPlotIsTriggered)
        {
            Stranger.SetActive(false);
        }
        else
        {
            Stranger.SetActive(true);
        }
    }
}
