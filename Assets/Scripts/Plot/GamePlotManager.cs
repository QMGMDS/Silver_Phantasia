using System.Collections;
using UnityEngine;

public class GamePlotManager : Singleton <GamePlotManager>
{
    public GameObject MPlot_1;
    public GameObject KPlot_1;
    public GameObject Plot1_Enemy_Dragon;


    [Header("剧情一判断布尔值")]
    // 视野是否打开
    public bool visionOpen;
    // 妹红的自言自语1是否结束
    public bool MTalkOneisOver;
    // 妹红的跳和走是否结束
    public bool MJumpAndWalkisOver;
    // 妹红的自言自语2是否结束
    public bool MTalkTwoisOver;
    // 运镜 + 移动是否结束
    public bool MCameraAndWalkisOver;
    // 发出感叹号 + 运镜是否结束
    public bool MAmazingAndCamera;
    // 小对话
    public bool dialogue1;
    // 视野是否展开
    public bool visionAllOpen;
    // 小对话 2
    public bool dialogue2;
    // 谁肚子叫？
    public bool strangeSound;
    // 小对话 3 
    public bool dialogue3;
    // 龙出现
    public bool dragonAppear;
    // 发现龙
    public bool findDragon;

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
        MPlot_1.SetActive(true);
        KPlot_1.SetActive(true);
        StartCoroutine(OpenVision());
    }

    /// <summary>
    /// 视野被打开时触发的方法：剧情一启动
    /// </summary>
    private IEnumerator OpenVision()
    {
        // 1.视野打开
        EventHandler.CallPlotOneVisionOpen();
        yield return new WaitUntil(() => visionOpen);

        // 2.妹红的自言自语1
        EventHandler.CallPlotDialogueEvent(1);
        // MTalkOneisOver为true表明第一段对话结束了，关闭对话框事时MTalkOneisOver为true
        yield return new WaitUntil(() => MTalkOneisOver);

        // 3.动画：人物跳起来，随后移动
        EventHandler.CallPlot1_MJumpAndWalk();
        yield return new WaitUntil(() => MJumpAndWalkisOver);

        // 4.妹红的自言自语2 + 环顾四周
        EventHandler.CallPlotDialogueEvent(2);
        yield return new WaitUntil(() => MTalkTwoisOver);

        // 5.摄像机先移动，妹红紧随其后
        EventHandler.CallMPlot1_CameraAndMove();
        yield return new WaitUntil(() => MCameraAndWalkisOver);

        // 6.妹红转向，发现了什么，感叹号出现，摄像机快速移动
        EventHandler.CallPlot1_MFindWhat();
        yield return new WaitUntil(() => MAmazingAndCamera);

        // 7.对话1
        EventHandler.CallPlotDialogueEvent(3);
        yield return new WaitUntil(() => dialogue1);

        // 8.视野展开!!!
        EventHandler.CallVisionAllOpen();
        yield return new WaitUntil(() => visionAllOpen);

        // 9.小对话2
        EventHandler.CallPlotDialogueEvent(4);
        yield return new WaitUntil(() => dialogue2);

        // 10.谁肚子叫？
        EventHandler.CallStrangeSound();
        yield return new WaitUntil(() => strangeSound);

        // 11.小对话3
        EventHandler.CallPlotDialogueEvent(5);
        yield return new WaitUntil(() => dialogue3);

        // 12.龙出现
        EventHandler.CallDragonAppear();
        yield return new WaitUntil(() => dragonAppear);

        // 13.两人惊讶
        EventHandler.CallFindDragon();
        yield return new WaitUntil(() => findDragon);

        // 14.小对话4

    }
}
