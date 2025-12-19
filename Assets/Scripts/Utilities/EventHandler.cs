using System;
using UnityEngine;

//EventHandler专门定义跨场景的+控制人物输入系统的  事件
public static class EventHandler
{

#region 场景加载事件
    public static event Action<string,Vector3> TransitionEvent;
    /// <summary>
    /// 场景切换事件
    /// </summary>
    /// <param name="sceneToGo">要加载的场景</param>
    /// <param name="posToGo">要去的目的坐标</param>
    public static void CallTransitionEvent(string sceneToGo,Vector3 posToGo)
    {
        TransitionEvent?.Invoke(sceneToGo,posToGo);
    }

    public static event Action<string> LoadSceneEvent;
    /// <summary>
    /// 加载对应的场景事件，只加载
    /// </summary>
    /// <param name="loadScene">要加载的场景</param>
    public static void CallLoadSceneEvent(string loadScene)
    {
        LoadSceneEvent?.Invoke(loadScene);
    }

    public static event Action<Vector3> MoveToPositionEvent;
    /// <summary>
    /// 场景切换时移动人物到目的坐标的事件
    /// </summary>
    /// <param name="targetPosition">目的坐标</param>
    public static void CallMoveToPosition(Vector3 targetPosition)
    {
        MoveToPositionEvent?.Invoke(targetPosition);
    }
#endregion


#region 人物移动控制
    public static event Action ClosePlayerMoveEvent;
    /// <summary>
    /// 关闭人物移动控制的事件
    /// </summary>
    public static void CallClosePlayerMoveEvent()
    {
        ClosePlayerMoveEvent?.Invoke();
    }

    public static event Action OpenPlayerMoveEvent;
    /// <summary>
    /// 开启人物移动控制的事件
    /// </summary>
    public static void CallOpenPlayerMoveEvent()
    {
        OpenPlayerMoveEvent?.Invoke();
    }
#endregion


#region 战斗事件
    public static event Action<string,EnemyTeam_SO> BattleStartEvent;
    /// <summary>
    /// 战斗开始的触发事件
    /// </summary>
    /// <param name="battleBack">战斗背景</param>
    /// <param name="enemyTeam">战斗敌人队伍</param>
    public static void CallBattleStartEvent(string battleBack,EnemyTeam_SO enemyTeam)
    {
        BattleStartEvent?.Invoke(battleBack,enemyTeam);
    }

    public static event Action BattleEndEvent;
    /// <summary>
    /// 战斗结束的事件（战斗胜利执行的）
    /// </summary>
    public static void CallBattleEndEvent()
    {
        BattleEndEvent?.Invoke();
    }
    public static event Action Battle_ShowPrepareAttack;
    /// <summary>
    /// 进入预攻击状态
    /// </summary>
    public static void CallBattle_ShowPrepareAttack()
    {
        Battle_ShowPrepareAttack?.Invoke();
    }

    public static event Action Battle_PlayerHUDUpdate;
    /// <summary>
    /// 更新玩家HUD
    /// </summary>
    public static void CallBattle_PlayerHUDUpdate()
    {
        Battle_PlayerHUDUpdate?.Invoke();
    }

    public static event Action<ItemDetials> PlayerUseItem;
    /// <summary>
    /// 玩家使用物品
    /// </summary>
    /// <param name="usedItem">被使用的物品</param>
    public static void CallPlayerUseItem(ItemDetials usedItem)
    {
        PlayerUseItem?.Invoke(usedItem);
    }

    public static event Action<SkillDetails> PlayerUseSkill;
    /// <summary>
    /// 玩家使用技能
    /// </summary>
    /// <param name="usedSkill">被使用的技能</param>
    public static void CallPlayerUseSkill(SkillDetails usedSkill)
    {
        PlayerUseSkill?.Invoke(usedSkill);
    }
#endregion


#region 按键交互
    public static event Action InteractButtonStartEvent;
    /// <summary>
    /// 对话交互键被按下事件
    /// </summary>
    public static void CallInteractButtonStartEvent()
    {
        InteractButtonStartEvent?.Invoke();
    }

    public static event Action GameSettings_ODown;
    /// <summary>
    /// 游戏设置菜单O键被按下
    /// </summary>
    public static void CallGameSettings_ODown()
    {
        GameSettings_ODown?.Invoke();
    }
#endregion


#region 对话事件
    public static event Action<DialoguePiece> ShowDialogueEvent;
    /// <summary>
    /// 对话片段UI显示的事件
    /// </summary>
    /// <param name="piece">要播放的对话</param>
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
    
    public static event Action<int> PlotDialogueOptionDown;
    /// <summary>
    /// 对话选项被按下
    /// 1代表选择了选项一，2代表选择了选项二
    /// </summary>
    public static void CallPlotDialogueOptionDown(int choose)
    {
        PlotDialogueOptionDown?.Invoke(choose);
    }

    public static event Action<int> PlotDialogueEvent;
    /// <summary>
    /// 剧情对话触发
    /// </summary>
    /// <param name="plotIndex">要对话的数据序号</param>
    public static void CallPlotDialogueEvent(int plotIndex)
    {
        PlotDialogueEvent?.Invoke(plotIndex);
    }
#endregion


#region 游戏运行剧情逻辑事件

    #region 摄像机事件
    public static event Action Kingdom_CameraOverview;
    /// <summary>
    /// 自下而上纵观全图：Kingdom
    /// </summary>
    public static void CallKingdom_CameraOverview()
    {
        Kingdom_CameraOverview?.Invoke();
    }

    public static event Action Kingdom_CameraFollowBrave;
    /// <summary>
    /// 王国：摄像机跟随勇者运镜移动
    /// </summary>
    public static void CallKingdom_CameraFollowBrave()
    {
        Kingdom_CameraFollowBrave?.Invoke();
    }

    public static event Action Dungeon_CameraFollowBrave;
    /// <summary>
    /// 地牢：摄像机跟随勇者移动
    /// </summary>
    public static void CallDungeon_CameraFollowBrave()
    {
        Dungeon_CameraFollowBrave?.Invoke();
    }

    public static event Action Dungeon_CameraFindStranger;
    /// <summary>
    /// 地牢：发现陌生人，摄像机移动
    /// </summary>
    public static void CallDungeon_CameraFindStranger()
    {
        Dungeon_CameraFindStranger?.Invoke();
    }

    public static event Action Dungeon_CameraFindStrangerEnd;
    /// <summary>
    /// 地牢：结束发现陌生人，摄像机移动
    /// </summary>
    public static void CallDungeon_CameraFindStrangerEnd()
    {
        Dungeon_CameraFindStrangerEnd?.Invoke();
    }
    #endregion

    #region 灯光控制事件
    public static event Action Kingdom_InitAllSpot;
    /// <summary>
    /// 初始化王国的所有灯光
    /// </summary>
    public static void CallKingdom_InitAllSpot()
    {
        Kingdom_InitAllSpot?.Invoke();
    }

    public static event Action Dungeon_InitAllSpot;
    /// <summary>
    /// 初始化地牢的所有灯光
    /// </summary>
    public static void CallDungeon_InitAllSpot()
    {
        Dungeon_InitAllSpot?.Invoke();
    }
    #endregion

    #region 剧情触发控制事件
    public static event Action Kingdom_BraveQuit;
    /// <summary>
    /// 勇者离开王宫
    /// </summary>
    public static void CallKingdom_BraveQuit()
    {
        Kingdom_BraveQuit?.Invoke();
    }

    public static event Action<int> BraveFaceChange;
    /// <summary>
    /// 玩家朝向单次修改
    /// </summary>
    /// <param name="choose">1为面朝上，2为面朝下，3为面朝左，4为面朝右</param>
    public static void CallBraveFaceChange(int choose)
    {
        BraveFaceChange?.Invoke(choose);
    }

    public static event Action Dungeon_FirstEntry;
    /// <summary>
    /// 地牢：初入（勇者熟悉周围环境）
    /// </summary>
    public static void CallDungeon_FirstEntry()
    {
        Dungeon_FirstEntry?.Invoke();
    }

    public static event Action<bool> PlayerShowImageChange;
    /// <summary>
    /// 是否显示玩家图片
    /// </summary>
    /// <param name="change">true为显示，false为不显示</param>
    public static void CallPlayerShowImageChange(bool change)
    {
        PlayerShowImageChange?.Invoke(change);
    }

    public static event Action<bool> PlayerSign;
    /// <summary>
    /// 打开关闭玩家Sign
    /// </summary>
    /// <param name="change"></param>
    public static void CallPlayerSign(bool change)
    {
        PlayerSign?.Invoke(change);
    }

    public static event Action FindStranger;
    /// <summary>
    /// 地牢：发现陌生人
    /// </summary>
    public static void CallFindStranger()
    {
        FindStranger?.Invoke();
    }

    public static event Action BridgeBreak;
    /// <summary>
    /// 地牢：断桥
    /// </summary>
    public static void CallBridgeBreak()
    {
        BridgeBreak?.Invoke();
    }
    #endregion

#endregion


#region 背包相关事件
    public static event Action<Treasure> ChestOpen;
    /// <summary>
    /// 宝箱打开，玩家背包内物品增加
    /// </summary>
    /// <param name="inChest_Truesure">被打开的宝箱内宝藏</param>
    public static void CallChestOpen(Treasure inChest_Truesure)
    {
        ChestOpen?.Invoke(inChest_Truesure);
    }
#endregion


}
