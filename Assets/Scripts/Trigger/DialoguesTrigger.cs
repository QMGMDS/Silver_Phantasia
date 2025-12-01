using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;


//存在分支选项的剧情对话：
//有主对话和分支对话
//流程 例如：
//先进行主对话，直到遇到选项，主对话结束
//根据剧情选项的选择播放对应的分支对话
//分支对话结束后再播放对应的主对话
//主对话结束后结束剧情对话，且对话不可重复触发，仅可通过Y键查看历史记录

//分支对话实现思路：
//含选项的对话片段存在一个布尔值：是否为选项片段
//为true则触发显示对话选项事件
//UI显示对话选项和对应文本内容（文本内容由对话选项片段决定）
//选择对应选项触发对应分支对话



//对话触发和对话控制脚本
public class DialoguesTrigger : MonoBehaviour
{
    //主对话中每一个对话片段的列表
    public List<DialoguePiece> dialogueList;
    //主对话堆栈
    private Stack<DialoguePiece> dialogueStack;

    //分支对话1中对话片段列表
    public List<DialoguePiece> dialogueBranchOne;
    //分支对话1堆栈
    private Stack<DialoguePiece> dialogueBranchOneStack;

    //分支对话2中对话片段列表
    public List<DialoguePiece> dialogueBranchTwo;
    //分支对话2堆栈
    private Stack<DialoguePiece> dialogueBranchTwoStack;

    //当前进行的对话堆栈（通过直接引用赋值）
    private Stack<DialoguePiece> currentDialogueStack;

    //对话选项显示片段
    public DialogueOption dialogueOption;

    //按键提示物体
    private GameObject uiSign;

    //是否可进行对话
    private bool canTalk;

    //是否在进行单次对话
    private bool isTalking;

    


    private void Awake()
    {
        uiSign = transform.GetChild(0).gameObject;
        //通过ref关键字，告诉方法该变量不是按值传递而是按引用传递
        //如果是按值传递：传递的是引用的副本，可以修改外部的堆栈对象本身，但不能修改引用的指向（即让它指向一个新对象）来影响原始引用
        //如果是按引用传递：传递的是原始引用的地址，既可以修改对象本身，也可以修改原始引用的指向。
        //方法FillDialogueStack()实现将dialogueBranchOneStack这个外部变量指向一个新的堆栈对象
        //这个新的堆栈对象即FillDialogueStack()方法内部New出来的dialogueStack
        FillDialogueStack(ref dialogueStack,dialogueList);
        FillDialogueStack(ref dialogueBranchOneStack,dialogueBranchOne);
        FillDialogueStack(ref dialogueBranchTwoStack,dialogueBranchTwo);

        //初始的对话堆栈为主对话堆栈
        //引用赋值：让currentDialogueStack和dialogueStack指向同一个对象
        //修改currentDialogueStack同时也会修改dialogueStack
        //但如果对currentDialogueStack重新赋值（即让它指向一个新的Stack对象），那么dialogueStack不会受到影响
        currentDialogueStack = dialogueStack;
    }

    private void Update()
    {
        uiSign.SetActive(canTalk);
    }

    private void OnEnable()
    {
        EventHandler.InteractButtonStartEvent += OnInteractButtonStart;
        EventHandler.DialogueOptionOneDownEvent += OnDialogueOptionOneDownEvent;
        EventHandler.DialogueOptionTwoDownEvent += OnDialogueOptionTwoDownEvent;
    }

    private void OnDisable()
    {
        EventHandler.InteractButtonStartEvent -= OnInteractButtonStart;
        EventHandler.DialogueOptionOneDownEvent -= OnDialogueOptionOneDownEvent;
        EventHandler.DialogueOptionTwoDownEvent -= OnDialogueOptionTwoDownEvent;
    }


#region 一次对话的触发
    /// <summary>
    /// 按下空格触发的一次对话
    /// </summary>
    private void OnInteractButtonStart()
    {
        if (canTalk && !isTalking)
        {
            StartCoroutine(DialogueRoutine(currentDialogueStack));
        }
    }

    /// <summary>
    /// 按下选项一立马触发的一次对话
    /// </summary>
    private void OnDialogueOptionOneDownEvent()
    {
        Debug.Log("按下分支1");
        currentDialogueStack = dialogueBranchOneStack;
        StartCoroutine(DialogueRoutine(dialogueBranchOneStack));
    }

    /// <summary>
    /// 按下选项二立马触发的一次对话
    /// </summary>
    private void OnDialogueOptionTwoDownEvent()
    {
        Debug.Log("按下分支2");
        currentDialogueStack = dialogueBranchTwoStack;
        StartCoroutine(DialogueRoutine(dialogueBranchTwoStack));
    }
#endregion



    /// <summary>
    /// 从目标堆栈中拿出单次对话
    /// </summary>
    /// <param name="currentDialogueStack">目标对话堆栈</param>
    /// <returns></returns>
    private IEnumerator DialogueRoutine(Stack<DialoguePiece> currentDialogueStack)
    {
        isTalking = true;
        //TryPop()尝试从栈顶移除一个元素，并将移除的元素通过out参数返回
        //返回一个布尔值，表示是否成功移除元素
        //如果栈不为空，则移除栈顶元素并返回true，如果栈为空，则返回false
        //弹出的元素存储到result变量中
        if(currentDialogueStack.TryPop(out DialoguePiece result))
        {
            //关闭人物控制
            EventHandler.CallClosePlayerMoveEvent();
            //对话片段传到UI显示对话
            EventHandler.CallShowDialogueEvent(result);

            //WaitUntil()用于等待直到给定的条件为真
            //WaitUntil()需要一个委托
            //() => result.isDone是一个无参函数，每次调用时，它返回当前result.isDone的值
            yield return new WaitUntil(() => result.isDone);

            //如果为含对话选项的片段
            if (result.hasToOption)
            {
                EventHandler.CallShowDialogueOptionEvent(dialogueOption);
                //等待选项选择
                yield return new WaitUntil(() => dialogueOption.isChoose);
            }
            

            isTalking = false;
        }
        else
        {
            //关闭对话框，退出对话状态
            EventHandler.CallShowDialogueEvent(result);
            isTalking = false;
            //恢复人物控制
            EventHandler.CallOpenPlayerMoveEvent();
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canTalk = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canTalk = false;
        }
    }

    /// <summary>
    /// 对话堆栈的构建
    /// </summary>
    /// <param name="dialogueStack">被存入的对话堆栈</param>
    /// <param name="dialogueList">要存入的对话内容列表</param>
    private void FillDialogueStack(ref Stack<DialoguePiece> dialogueStack,List<DialoguePiece> dialogueList)
    {
        dialogueStack = new Stack<DialoguePiece>();
        //倒序压入堆栈
        for (int i = dialogueList.Count-1; i > -1; i--)
        {
            dialogueList[i].isDone = false;
            dialogueStack.Push(dialogueList[i]);
        }
    }

}
