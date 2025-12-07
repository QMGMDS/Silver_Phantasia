using System.Collections;
using UnityEngine;

public class GamePlotManager : Singleton <GamePlotManager>
{
    public GameObject player;

    [Header("剧情一判断布尔值")]
    // 视野是否打开
    public bool visionOpen;
    // 视野是否打开属性
    public bool visionIs
    {
        get => visionOpen;
        set
        {
            // 视野被打开时执行的方法
            if(value == true)
            {
                visionOpen = true;
                OpenVision();
            }
            // 视野被关闭时执行的方法
            else
            {
                visionOpen = false;
            }
        }
    }

    private void OnEnable()
    {
        EventHandler.PlotOneEvent += OnPlotOneEvent;
    }

    private void OnDisable()
    {
        EventHandler.PlotOneEvent -= OnPlotOneEvent;
    }


    /// <summary>
    /// 进入剧情一
    /// </summary>
    private void OnPlotOneEvent()
    {
        StartCoroutine(PlotOne());
    }

    private IEnumerator PlotOne()
    {
        EventHandler.CallLoadSceneEvent("Forest");
        yield return new WaitForSeconds(0.5f);
        player.SetActive(true);
        //关闭人物操作系统
        EventHandler.CallClosePlayerMoveEvent();
    }

    /// <summary>
    /// 视野被打开时触发的方法：剧情对话一启动
    /// </summary>
    private void OpenVision()
    {
        EventHandler.CallPlotDialogueEvent(1);
    }
}
