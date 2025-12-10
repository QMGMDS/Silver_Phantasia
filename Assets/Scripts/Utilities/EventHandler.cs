using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

//EventHandler专门定义跨场景的+控制人物输入系统的  事件
public static class EventHandler
{

#region 场景加载事件
    /// <summary>
    /// 场景卸载之前的事件
    /// </summary>
    public static event Action BeforeSceneUnloadEvent;
    public static void CallBeforeSceneUnloadEvent()
    {
        BeforeSceneUnloadEvent?.Invoke();
    }

    /// <summary>
    /// 场景卸载之后的事件
    /// </summary>
    public static event Action AfterSceneloadedEvent;

    public static void CallAfterSceneloadedEvent()
    {
        AfterSceneloadedEvent?.Invoke();
    }

    /// <summary>
    /// 场景切换事件
    /// </summary>
    public static event Action<string,Vector3> TransitionEvent;

    public static void CallTransitionEvent(string sceneToGo,Vector3 positionToGo)
    {
        TransitionEvent?.Invoke(sceneToGo,positionToGo);
    }

    public static event Action<string> LoadSceneEvent;
    /// <summary>
    /// 加载对应的场景
    /// </summary>
    /// <param name="loadScene">要加载的场景</param>
    public static void CallLoadSceneEvent(string loadScene)
    {
        LoadSceneEvent?.Invoke(loadScene);
    }

    /// <summary>
    /// 场景切换移动人物目的坐标事件
    /// </summary>
    public static event Action<Vector3> MoveToPositionEvent;

    public static void CallMoveToPosition(Vector3 targetPosition)
    {
        MoveToPositionEvent?.Invoke(targetPosition);
    }

#endregion

    /// <summary>
    /// 关闭人物移动控制的事件
    /// </summary>
    public static event Action ClosePlayerMoveEvent;

    public static void CallClosePlayerMoveEvent()
    {
        ClosePlayerMoveEvent?.Invoke();
    }

    /// <summary>
    /// 开启人物移动控制的事件
    /// </summary>
    public static event Action OpenPlayerMoveEvent;

    public static void CallOpenPlayerMoveEvent()
    {
        OpenPlayerMoveEvent?.Invoke();
    }


#region 战斗事件
    /// <summary>
    /// 战斗开始的触发事件
    /// </summary>
    public static event Action<string,BattleAttributeDataList_SO> BattleStartEvent;
    
    public static void CallBattleStartEvent(string battleBack,BattleAttributeDataList_SO enemyTeam)
    {
        BattleStartEvent?.Invoke(battleBack,enemyTeam);
    }

    /// <summary>
    /// 战斗结束的事件（战斗胜利执行的）
    /// </summary>
    public static event Action BattleEndEvent;
    
    public static void CallBattleEndEvent()
    {
        BattleEndEvent?.Invoke();
    }

#endregion


#region 按键交互
    /// <summary>
    /// 对话交互键被按下事件
    /// </summary>
    public static event Action InteractButtonStartEvent;

    public static void CallInteractButtonStartEvent()
    {
        InteractButtonStartEvent?.Invoke();
    }

    /// <summary>
    /// 查看对话历史按键Y被按下
    /// </summary>
    public static event Action OpenDialogueEvent;

    public static void CallOpenDialogueEvent()
    {
        OpenDialogueEvent?.Invoke();
    }
#endregion


#region 对话事件
    
    public static event Action<DialoguePiece> ShowDialogueEvent;
    /// <summary>
    /// 对话片段UI显示的事件
    /// </summary>
    public static void CallShowDialogueEvent(DialoguePiece piece)
    {
        ShowDialogueEvent?.Invoke(piece);
    }

    
    public static event Action<DialogueOption,int> ShowDialogueOptionEvent;
    /// <summary>
    /// 对话选项UI显示的事件
    /// </summary>
    /// <param name="option">选项信息</param>
    /// <param name="determinant">1是剧情对话，2是游玩对话</param>
    public static void CallShowDialogueOptionEvent(DialogueOption option,int determinant)
    {
        ShowDialogueOptionEvent?.Invoke(option,determinant);
    }
    
    
    public static event Action DialogueOptionOneDownEvent;
    /// <summary>
    /// 对话选项一被按下(游玩型)
    /// </summary>
    public static void CallDialogueOptionOneDownEvent()
    {
        DialogueOptionOneDownEvent?.Invoke();
    }

    
    public static event Action DialogueOptionTwoDownEvent;
    /// <summary>
    /// 对话选项二被按下(游玩型)
    /// </summary>
    public static void CallDialogueOptionTwoDownEvent()
    {
        DialogueOptionTwoDownEvent?.Invoke();
    }

    
    public static event Action<int> PlotDialogueOptionDown;
    /// <summary>
    /// 对话选项被按下(剧情型)
    /// 1代表选择了选项一，以此类推
    /// </summary>
    public static void CallPlotDialogueOptionDown(int choose)
    {
        PlotDialogueOptionDown?.Invoke(choose);
    }

    /// <summary>
    /// 对话结束的事件
    /// </summary>
    public static event Action DialogueOverEvent;

    public static void CallDialogueOverEvent()
    {
        DialogueOverEvent?.Invoke();
    }


#endregion


#region 剧情对话触发事件
    public static event Action<int> PlotDialogueEvent;
    /// <summary>
    /// 剧情对话触发
    /// </summary>
    public static void CallPlotDialogueEvent(int plotIndex)
    {
        PlotDialogueEvent?.Invoke(plotIndex);
    }

#endregion

#region 游戏运行逻辑事件

    public static event Action NewGameEvent;
    /// <summary>
    /// 呼叫：游戏开始
    /// </summary>
    public static void CallNewGameEvent()
    {
        NewGameEvent?.Invoke();
    }

    public static event Action PlotOneEvent;
    /// <summary>
    /// 呼叫：剧情一开始
    /// </summary>
    public static void CallPlotOneEvent()
    {
        PlotOneEvent?.Invoke();
    }

    public static event Action PlotOneVisionOpen;
    public static void CallPlotOneVisionOpen()
    {
        PlotOneVisionOpen?.Invoke();
    }

    public static event Action Plot1_MJumpAndWalk;
    public static void CallPlot1_MJumpAndWalk()
    {
        Plot1_MJumpAndWalk?.Invoke();
    }

    public static event Action MPlot1_CameraAndMove;
    public static void CallMPlot1_CameraAndMove()
    {
        MPlot1_CameraAndMove?.Invoke();
    }

    public static event Action Plot1_MFindWhat;
    public static void CallPlot1_MFindWhat()
    {
        Plot1_MFindWhat?.Invoke();
    }

#endregion

}
