using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotDialogueController : MonoBehaviour
{
    [Header("剧情一对话1")]
    public List<DialoguePiece> plotOne;
    private Stack<DialoguePiece> onePlotStack;
    [Header("剧情一对话2")]
    public List<DialoguePiece> plotTwo;
    private Stack<DialoguePiece> twoPlotStack;
    [Header("剧情一对话3")]
    public List<DialoguePiece> plotThree;
    private Stack<DialoguePiece> threePlotStack;
    [Header("剧情一对话4")]
    public List<DialoguePiece> plotFour;
    private Stack<DialoguePiece> fourPlotStack;
    [Header("剧情一对话5")]
    public List<DialoguePiece> plotFive;
    private Stack<DialoguePiece> fivePlotStack;
    [Header("剧情一对话6")]
    public List<DialoguePiece> plotSix;
    private Stack<DialoguePiece> sixPlotStack;


    // 选项分支对话堆栈
    private Stack<DialoguePiece> currentDialogueStack;
    [Header("剧情一选项1")]
    // 选项信息
    public DialogueOption dialogueOption1;
    
    // 选项一的对话
    public List<DialoguePiece> plot1_Option1_Choose1; //剧情一选项一的第一个选择触发的对话的信息
    private Stack<DialoguePiece> plot1_Option1_Choose1_Stack;
    // 选项二的对话
    public List<DialoguePiece> plot1_Option1_Choose2;
    private Stack<DialoguePiece> plot1_Option1_Choose2_Stack;
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
        if(!pieceOver)
            return;
        
        switch (plotIndex)
        {
            case 1:
                StartCoroutine(PlayPlotDialogue(onePlotStack,plotIndex));
                break;

            case 2:
                StartCoroutine(PlayPlotDialogue(twoPlotStack,plotIndex));
                break;

            case 3:
                if (plotChooseIndex == 101)
                    StartCoroutine(PlayPlotDialogue(currentDialogueStack,plotIndex));
                else
                    StartCoroutine(PlayPlotDialogue(threePlotStack,plotIndex));
                break;

            case 4:
                if (plotChooseIndex == 102)
                    StartCoroutine(PlayPlotDialogue(currentDialogueStack,plotIndex));
                else
                    StartCoroutine(PlayPlotDialogue(fourPlotStack,plotIndex));
                break;

            case 5:
                StartCoroutine(PlayPlotDialogue(fivePlotStack,plotIndex));
                break;

            case 6:
                StartCoroutine(PlayPlotDialogue(sixPlotStack,plotIndex));
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
            case 1:
                InitPlotStack(ref onePlotStack,plotOne);
                StartCoroutine(PlayPlotDialogue(onePlotStack,plotIndex));
                break;
            case 2:
                InitPlotStack(ref twoPlotStack,plotTwo);
                StartCoroutine(PlayPlotDialogue(twoPlotStack,plotIndex));
                break;
            case 3:
                InitPlotStack(ref threePlotStack,plotThree);
                StartCoroutine(PlayPlotDialogue(threePlotStack,plotIndex));
                break;
            case 4:
                InitPlotStack(ref fourPlotStack,plotFour);
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

            Debug.Log(plotIndex);

            //如果为含对话选项的片段
            if (result.hasToOption)
            {
                switch (plotIndex)
                {
                    case 3:
                        EventHandler.CallShowDialogueOptionEvent(dialogueOption1,1); // 1是剧情型对话
                        // 等待选项选择
                        yield return new WaitUntil(() => dialogueOption1.isChoose);
                        break;
                    case 4:
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
                    GamePlotManager.Instance.MTalkOneisOver = true;
                    break;
                case 2:
                    GamePlotManager.Instance.MTalkTwoisOver = true;
                    break;
                case 3:
                    GamePlotManager.Instance.dialogue1 = true;
                    break;
                case 4:
                    GamePlotManager.Instance.dialogue2 = true;
                    break;
                case 5:
                    GamePlotManager.Instance.dialogue3 = true;
                    break;
                case 6:
                    GamePlotManager.Instance.dialogue4 = true;
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
                    InitPlotStack(ref plot1_Option1_Choose1_Stack,plot1_Option1_Choose1);
                    currentDialogueStack = plot1_Option1_Choose1_Stack;
                }
                else if(choose == 2)
                {
                    InitPlotStack(ref plot1_Option1_Choose2_Stack,plot1_Option1_Choose2);
                    currentDialogueStack = plot1_Option1_Choose2_Stack;
                }
                break;
            case 102:
                if(choose == 1)
                {
                    InitPlotStack(ref plot1_Option2_Choose1_Stack,plot1_Option2_Choose1);
                    currentDialogueStack = plot1_Option2_Choose1_Stack;
                }
                else if(choose == 2)
                {
                    InitPlotStack(ref plot1_Option2_Choose2_Stack,plot1_Option2_Choose2);
                    currentDialogueStack = plot1_Option2_Choose2_Stack;
                }
                break;
        }

        StartCoroutine(PlayPlotDialogue(currentDialogueStack,plotIndex));
    }
}
