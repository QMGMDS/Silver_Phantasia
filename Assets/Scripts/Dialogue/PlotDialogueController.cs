using System.Collections;
using System.Collections.Generic;
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
    [Header("剧情一对话5")]
    public List<DialoguePiece> plotFive;
    private Stack<DialoguePiece> fivePlotStack;
    [Header("剧情一对话6")]
    public List<DialoguePiece> plotSix;
    private Stack<DialoguePiece> sixPlotStack;


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
    
    [Header("剧情一选项2")]
    // 选项信息
    public DialogueOption dialogueOption2;
    // 选项一的对话
    public List<DialoguePiece> plot1_Option2_Choose1;
    private Stack<DialoguePiece> plot1_Option2_Choose1_Stack;
    // 选项二的对话
    public List<DialoguePiece> plot1_Option2_Choose2;
    private Stack<DialoguePiece> plot1_Option2_Choose2_Stack;



    // 确定播放的主对话，类似指向当前播放对话的一个指针
    private int plotIndex;
    // 确定哪个剧情选项出现
    private int plotChooseIndex = 100;
    // 当前片段动画是否播放结束
    [SerializeField]private bool pieceOver;
    //跳过键是否按下,按下跳过键则进入跳过模式
    [SerializeField]private bool skip;
    


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

    /// <summary>
    /// 模式切换：对话是否跳过
    /// </summary>
    private void OnSkipDialogue()
    {
        skip = !skip;
    }

    private void OnInteractButtonStart()
    {
        // 对话未播放完成则空格键无法起作用
        if(!pieceOver)
            return;
        
        
        switch (plotIndex)
        {
            case 1:
                if (plotChooseIndex == 101)
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
                InitPlotStack(ref fivePlotStack,plotFive);
                StartCoroutine(PlayPlotDialogue(fivePlotStack,plotIndex));
                break;
            case 6:
                InitPlotStack(ref sixPlotStack,plotSix);
                StartCoroutine(PlayPlotDialogue(sixPlotStack,plotIndex));
                break;
        }
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
                        break;
                    case 2:
                        EventHandler.CallShowDialogueOptionEvent(dialogueOption2,1);
                        // 等待选项选择
                        yield return new WaitUntil(() => dialogueOption2.isChoose);
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
                case 5:
                    //GamePlotManager.Instance.dialogue3 = true;
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
        plotChooseIndex++;
        // 101说明此时为第一个对话选项出现并已经选择了
        // 102说明此时为第二个对话选项出现并已经选择了
        switch (plotChooseIndex)
        {
            case 101:
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
                break;
        }

        StartCoroutine(PlayPlotDialogue(currentDialogueStack,plotIndex));
    }
}
