using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionTrigger : MonoBehaviour
{
    public string sceneToGo;
    public Vector3 positionToGo;

    private void OnTriggerEnter2D(Collider2D other)
    {
            if (other.CompareTag("Player"))
            {
                EventHandler.CallTransitionEvent(sceneToGo,positionToGo);
                // 第一次碰到传送点传送是传送到地牢
                if (!GamePlotManager.Instance.braveEntryDungeon)
                {
                    StartCoroutine(Dungeon_Start());
                }
            }
    }
    

    /// <summary>
    /// 勇者第一次进入地牢
    /// </summary>
    /// <returns></returns>
    private IEnumerator Dungeon_Start()
    {
        yield return new WaitForSeconds(1f);

        GamePlotManager.Instance.braveEntryDungeon = true;
        // 打开人物显示图片
        EventHandler.CallPlayerShowImageChange(true);
        // 摄像机跟随玩家
        EventHandler.CallDungeon_CameraFollowBrave();
        // 玩家朝向单次修改
        EventHandler.CallBraveFaceChange(2);
        // 初始化地牢灯光
        EventHandler.CallDungeon_InitAllSpot();
    }
}



