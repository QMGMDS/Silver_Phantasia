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

    private int plotIndex;


    private void OnEnable()
    {
        EventHandler.InteractButtonStartEvent += OnInteractButtonStart;
        EventHandler.PlotDialogueEvent += OnPlotDialogueEvent;
    }

    private void OnDisable()
    {
        EventHandler.InteractButtonStartEvent -= OnInteractButtonStart;
        EventHandler.PlotDialogueEvent -= OnPlotDialogueEvent;
    }

    private void OnInteractButtonStart()
    {
        switch (plotIndex)
        {
            case 1:
                StartCoroutine(PlayPlotDialogue(onePlotStack,plotIndex));
                break;
            case 2:
                StartCoroutine(PlayPlotDialogue(twoPlotStack,plotIndex));
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
        }
    }

    /// <summary>
    /// 执行一次剧情对话
    /// </summary>
    private IEnumerator PlayPlotDialogue(Stack<DialoguePiece> plotStack,int plotIndex)
    {
        if(plotStack.TryPop(out DialoguePiece result))
        {
            //关闭人物控制
            EventHandler.CallClosePlayerMoveEvent();
            //对话片段传到UI显示对话
            EventHandler.CallShowDialogueEvent(result);
            yield return new WaitUntil(() => result.isDone);
        }
        else
        {
            //关闭对话框，退出对话状态
            EventHandler.CallShowDialogueEvent(result);
            //恢复人物控制
            //EventHandler.CallOpenPlayerMoveEvent();

            if (plotIndex == 1)
            {
                GamePlotManager.Instance.MTalkOneisOver = true;
            }
            else if (plotIndex == 2)
            {  
                GamePlotManager.Instance.MTalkTwoisOver = true;
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
        //倒序压入堆栈
        for (int i = plot.Count-1; i > -1; i--)
        {
            plot[i].isDone = false;
            plotStack.Push(plot[i]);
        }
    }

}
