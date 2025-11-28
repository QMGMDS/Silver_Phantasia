using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//EventHandler专门定义跨场景的+控制人物输入系统的  事件
public static class EventHandler
{
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

    /// <summary>
    /// 对话结束的事件
    /// </summary>
    public static event Action DialogueOverEvent;

    public static void CallDialogueOverEvent()
    {
        DialogueOverEvent?.Invoke();
    }

    /// <summary>
    /// 场景切换事件
    /// </summary>
    public static event Action<string,Vector3> TransitionEvent;

    public static void CallTransitionEvent(string sceneToGo,Vector3 positionToGo)
    {
        TransitionEvent?.Invoke(sceneToGo,positionToGo);
    }

    /// <summary>
    /// 场景切换移动人物目的坐标事件
    /// </summary>
    public static event Action<Vector3> MoveToPositionEvent;

    public static void CallMoveToPosition(Vector3 targetPosition)
    {
        MoveToPositionEvent?.Invoke(targetPosition);
    }

    /// <summary>
    /// 交互键被按下事件
    /// </summary>
    public static event Action InteractButtonStartEvent;

    public static void CallInteractButtonStartEvent()
    {
        InteractButtonStartEvent?.Invoke();
    }

    /// <summary>
    /// 对话片段UI显示的事件
    /// </summary>
    public static event Action<DialoguePiece> ShowDialogueEvent;

    public static void CallShowDialogueEvent(DialoguePiece piece)
    {
        ShowDialogueEvent?.Invoke(piece);
    }

    
    
}
