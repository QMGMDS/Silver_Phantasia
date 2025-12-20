using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlotDialogueController : MonoBehaviour
{
    [Header("对话：勇者与国王")]
    public Dialogue_SO dialogue_1;
    private Stack<DialoguePiece> onePlotStack;
    [Header("对话：地牢_初探")]
    public Dialogue_SO dialogue_2;
    private Stack<DialoguePiece> twoPlotStack;
    [Header("对话：发现可疑之人")]
    public Dialogue_SO dialogue_3;
    private Stack<DialoguePiece> threePlotStack;
    [Header("对话：可疑之人断桥了")]
    public Dialogue_SO dialogue_4;
    private Stack<DialoguePiece> fourPlotStack;
    [Header("对话：遇到BOSS")]
    public Dialogue_SO dialogue_5;
    private Stack<DialoguePiece> fivePlotStack;
    [Header("逃跑对话")]
    public Dialogue_SO dialogue_6;
    private Stack<DialoguePiece> sixPlotStack;
    [Header("技能对话")]
    public Dialogue_SO dialogue_7;
    private Stack<DialoguePiece> sevenPlotStack;
    


    // 选项分支对话堆栈
    private Stack<DialoguePiece> currentDialogueStack;
    [Header("选项：勇者与国王-能与不能")]
    // 选项信息
    public DialogueOption dialogueOption1;
    
    // 选项一的对话
    public Dialogue_SO plot1_Option1_Choose1;
    // 选项二的对话
    public Dialogue_SO plot1_Option1_Choose2;
    //选项选择的堆栈
    private Stack<DialoguePiece> plot1_Option1_ChooseStack;



    // 确定播放的主对话，类似指向当前播放对话的一个指针
    private int plotIndex;
    private bool isChoose;
    // 当前片段动画是否播放结束
    [SerializeField]private bool pieceOver;



    [Header("BOSS战")]
    public string battleBack;
    public EnemyTeam_SO enemyTeam_SO;
    


    private void OnEnable()
    {
        EventHandler.InteractButtonStartEvent += OnInteractButtonStart;
        EventHandler.PlotDialogueEvent += OnPlotDialogueEvent;
        EventHandler.PlotDialogueOptionDown += OnPlotDialogueOptionDown;
    }

    private void OnDisable()
    {
        EventHandler.InteractButtonStartEvent -= OnInteractButtonStart;
        EventHandler.PlotDialogueEvent -= OnPlotDialogueEvent;
        EventHandler.PlotDialogueOptionDown += OnPlotDialogueOptionDown;
    }

    private void OnInteractButtonStart()
    {
        // 对话未播放完成则空格键无法起作用
        if(!pieceOver)
            return;
        
        
        switch (plotIndex)
        {
            case 1:
                if (isChoose)
                    StartCoroutine(PlayPlotDialogue(currentDialogueStack,plotIndex));
                else
                    StartCoroutine(PlayPlotDialogue(onePlotStack,plotIndex));
                break;

            case 2:
                StartCoroutine(PlayPlotDialogue(twoPlotStack,plotIndex));
                break;

            case 3:
                StartCoroutine(PlayPlotDialogue(threePlotStack,plotIndex));
                break;

            case 4:
                StartCoroutine(PlayPlotDialogue(fourPlotStack,plotIndex));
                break;

            case 5:
                StartCoroutine(PlayPlotDialogue(fivePlotStack,plotIndex));
                break;



        }
        
    }

    /// <summary>
    /// 标明是哪个剧情，自动进行第一次对话
    /// </summary>
    /// <param name="plotIndex"></param>
    private void OnPlotDialogueEvent(int plotIndex)
    {
        this.plotIndex = plotIndex;
        switch (plotIndex)
        {
            case 1: // 1即进行勇者与国王的对话
                InitPlotStack(ref onePlotStack,dialogue_1.dialoguePiecesList);
                StartCoroutine(PlayPlotDialogue(onePlotStack,plotIndex));
                break;
            case 2:
                InitPlotStack(ref twoPlotStack,dialogue_2.dialoguePiecesList);
                StartCoroutine(PlayPlotDialogue(twoPlotStack,plotIndex));
                break;
            case 3:
                InitPlotStack(ref threePlotStack,dialogue_3.dialoguePiecesList);
                StartCoroutine(PlayPlotDialogue(threePlotStack,plotIndex));
                break;
            case 4:
                InitPlotStack(ref fourPlotStack,dialogue_4.dialoguePiecesList);
                StartCoroutine(PlayPlotDialogue(fourPlotStack,plotIndex));
                break;
            case 5:
                InitPlotStack(ref fivePlotStack,dialogue_5.dialoguePiecesList);
                StartCoroutine(PlayPlotDialogue(fivePlotStack,plotIndex));
                break;
            case 6:
                InitPlotStack(ref sixPlotStack,dialogue_6.dialoguePiecesList);
                StartCoroutine(FleeTip());
                break;
            case 7:
                InitPlotStack(ref sevenPlotStack,dialogue_7.dialoguePiecesList);
                StartCoroutine(SkillTip());
                break;
        }
    }

    /// <summary>
    /// 逃跑提示对话
    /// </summary>
    /// <returns></returns>
    private IEnumerator FleeTip()
    {
        StartCoroutine(PlayPlotDialogue(sixPlotStack,plotIndex));
        yield return new WaitForSeconds(1.5f);
        // 关闭对话框，退出对话状态
        EventHandler.CallShowDialogueEvent(null);
    }

    /// <summary>
    /// 技能提示对话
    /// </summary>
    /// <returns></returns>
    private IEnumerator SkillTip()
    {
        StartCoroutine(PlayPlotDialogue(sevenPlotStack,plotIndex));
        yield return new WaitForSeconds(1.5f);
        // 关闭对话框，退出对话状态
        EventHandler.CallShowDialogueEvent(null);
    }

    /// <summary>
    /// 执行一次剧情对话
    /// </summary>
    private IEnumerator PlayPlotDialogue(Stack<DialoguePiece> plotStack,int plotIndex)
    {
        if(plotStack.TryPop(out DialoguePiece result))
        {
            pieceOver = false;
            //关闭人物控制
            EventHandler.CallClosePlayerMoveEvent();
            //对话片段传到UI显示对话
            EventHandler.CallShowDialogueEvent(result);
            yield return new WaitUntil(() => result.isDone);


            //如果为含对话选项的片段
            if (result.hasToOption)
            {
                switch (plotIndex)
                {
                    case 1:
                        EventHandler.CallShowDialogueOptionEvent(dialogueOption1,1); // 1是剧情型对话
                        // 等待选项选择
                        yield return new WaitUntil(() => dialogueOption1.isChoose);
                        dialogueOption1.isChoose = false;
                        break;
                }
            }

            pieceOver = true;
        }
        else
        {
            // 关闭对话框，退出对话状态
            EventHandler.CallShowDialogueEvent(result);
            // 恢复人物控制
            //EventHandler.CallOpenPlayerMoveEvent();

            switch (plotIndex)
            {
                case 1:
                    isChoose = false;
                    GamePlotManager.Instance.Kingdom_ChooseAndDialogueOver();
                    break;
                case 2:
                    EventHandler.CallOpenPlayerMoveEvent();
                    break;
                case 3:
                    EventHandler.CallFindStranger();
                    break;
                case 4:
                    EventHandler.CallOpenPlayerMoveEvent();
                    break;
                case 5: // BOSS战斗
                    EventHandler.CallBattleStartEvent(battleBack,enemyTeam_SO);
                    GamePlotManager.Instance.battle_BOSS = Battle_BOSS.Ing;
                    break;
                case 6:
                    //GamePlotManager.Instance.dialogue4 = true;
                    break;
            }
        }

    }



    /// <summary>
    /// 初始化堆栈
    /// </summary>
    /// <param name="plotStack"></param>
    /// <param name="plot"></param>
    private void InitPlotStack(ref Stack<DialoguePiece> plotStack,List<DialoguePiece> plot)
    {
        plotStack = new Stack<DialoguePiece>();
        // 倒序压入堆栈
        for (int i = plot.Count-1; i > -1; i--)
        {
            plot[i].isDone = false;
            plotStack.Push(plot[i]);
        }
    }

    /// <summary>
    /// 是第几个选项的剧情？
    /// </summary>
    /// <param name="choose"></param>
    private void OnPlotDialogueOptionDown(int choose)
    {
        isChoose = true;
        if(choose == 1)
        {
            // 玩家选择了能
            GamePlotManager.Instance.kingdom_PlayerChoose = Kingdom_PlayerChoose.Yes;
            InitPlotStack(ref plot1_Option1_ChooseStack,plot1_Option1_Choose1.dialoguePiecesList);
            currentDialogueStack = plot1_Option1_ChooseStack;
        }
        else if(choose == 2)
        {
            // 玩家选择了不能
            GamePlotManager.Instance.kingdom_PlayerChoose = Kingdom_PlayerChoose.No;
            InitPlotStack(ref plot1_Option1_ChooseStack,plot1_Option1_Choose2.dialoguePiecesList);
            currentDialogueStack = plot1_Option1_ChooseStack;
        }

        StartCoroutine(PlayPlotDialogue(currentDialogueStack,plotIndex));
    }
}
