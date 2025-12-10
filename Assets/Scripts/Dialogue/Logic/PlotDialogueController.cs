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
    [Header("剧情一选项1")]
    // 选项信息
    public DialogueOption dialogueOption1;
    // 选项分支对话堆栈
    private Stack<DialoguePiece> currentDialogueStack;
    // 选项一的对话
    public List<DialoguePiece> plot1_Option1_Choose1; //剧情一选项一的第一个选择触发的对话的信息
    private Stack<DialoguePiece> plot1_Option1_Choose1_Stack;
    // 选项二的对话
    public List<DialoguePiece> plot1_Option1_Choose2;
    private Stack<DialoguePiece> plot1_Option1_Choose2_Stack;



    // 确定播放的主对话
    private int plotIndex;
    // 当前片段动画是否播放结束
    private bool pieceOver;
    


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
                StartCoroutine(PlayPlotDialogue(threePlotStack,plotIndex));
                break;
            case 0:
                StartCoroutine(PlayPlotDialogue(currentDialogueStack,plotIndex));
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
                    case 3:
                        EventHandler.CallShowDialogueOptionEvent(dialogueOption1,1);
                        break;
                }
                // 等待选项选择
                yield return new WaitUntil(() => dialogueOption1.isChoose);
            }

            pieceOver = true;
        }
        else
        {
            // 关闭对话框，退出对话状态
            EventHandler.CallShowDialogueEvent(result);
            // 恢复人物控制
            //EventHandler.CallOpenPlayerMoveEvent();

            if (plotIndex == 1)
            {
                GamePlotManager.Instance.MTalkOneisOver = true;
            }
            else if (plotIndex == 2)
            {
                GamePlotManager.Instance.MTalkTwoisOver = true;
            }
            else if (plotIndex == 0)
            {
                GamePlotManager.Instance.dialogue1 = true;
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
    /// 根据选项选择对应剧情对话
    /// </summary>
    /// <param name="choose"></param>
    private void OnPlotDialogueOptionDown(int choose)
    {
        // 设置为播放选项对话
        plotIndex = 0;
        switch (choose)
        {
            case 1:
                InitPlotStack(ref plot1_Option1_Choose1_Stack,plot1_Option1_Choose1);
                currentDialogueStack = plot1_Option1_Choose1_Stack;
                break;
            case 2:
                InitPlotStack(ref plot1_Option1_Choose2_Stack,plot1_Option1_Choose2);
                currentDialogueStack = plot1_Option1_Choose2_Stack;
                break;
        }
        StartCoroutine(PlayPlotDialogue(currentDialogueStack,3));
    }


}
