using System.Collections;
using UnityEngine;

public class Plot_Kingdom : MonoBehaviour
{

    public NPCMovement NPCMovement;
    public Animator braveAnim;



    private void OnEnable()
    {
        braveAnim.enabled = false;
        StartCoroutine(KingdomPlot());
        EventHandler.Kingdom_BraveQuit += OnKingdom_BraveQuit;
    }

    private void OnDisable()
    {
        EventHandler.Kingdom_BraveQuit -= OnKingdom_BraveQuit;
    }



    /// <summary>
    /// 王国场景激活时触发的剧情
    /// </summary>
    /// <returns></returns>
    private IEnumerator KingdomPlot()
    {
        // 关闭人物显示
        EventHandler.CallPlayerShowImageChange(false);
        // 初始化灯光
        EventHandler.CallKingdom_InitAllSpot();
        // 缓慢移动摄像机
        EventHandler.CallKingdom_CameraOverview();

        // 等待摄像机移动结束
        yield return new WaitUntil(() => CameraController.Instance.cameraMoveIsOver); 
        CameraController.Instance.cameraMoveIsOver = false;

        yield return new WaitForSeconds(1f);
        // 触发对话
        EventHandler.CallPlotDialogueEvent(1);
    }

    /// <summary>
    /// 勇者离开王宫
    /// </summary>
    private void OnKingdom_BraveQuit()
    {
        // 摄像机移动
        EventHandler.CallKingdom_CameraFollowBrave();

        // 勇者移动
        braveAnim.enabled = true;
        NPCMovement.enabled = true;
        NPCMovement.startGridPosition = new Vector3Int(1,5,0);
        NPCMovement.targetGridPosition = new Vector3Int(1,-14,0);
        NPCMovement.InitNPC();
        StartCoroutine(NPCMovement.Movement());

        
    }
}
