using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//对话触发和对话控制脚本
public class DialoguesTrigger : MonoBehaviour
{
    //每一个对话片段
    public List<DialoguePiece> dialogueList;

    //按键提示物体
    private GameObject uiSign;

    //是否可进行对话
    private bool canTalk;

    //是否在进行单次对话
    private bool isTalking;

    //对话堆栈
    private Stack<DialoguePiece> dialogueStack;


    private void Awake()
    {
        uiSign = transform.GetChild(0).gameObject;
        FillDialogueStack();
    }

    private void Update()
    {
        uiSign.SetActive(canTalk);
    }

    private void OnEnable()
    {
        EventHandler.InteractButtonStartEvent += OnInteractButtonStart;
    }

    private void OnDisable()
    {
        EventHandler.InteractButtonStartEvent -= OnInteractButtonStart;
    }

    /// <summary>
    /// 触发对话函数
    /// </summary>
    private void OnInteractButtonStart()
    {
        if (canTalk && !isTalking)
        {
            Debug.Log("对话开始");
            //进入对话
            StartCoroutine(DialogueRoutine());
        }
    }

    private IEnumerator DialogueRoutine()
    {
        isTalking = true;
        //TryPop()尝试从栈顶移除一个元素，并将移除的元素通过out参数返回
        //返回一个布尔值，表示是否成功移除元素
        //如果栈不为空，则移除栈顶元素并返回true，如果栈为空，则返回false
        //弹出的元素存储到result变量中
        if(dialogueStack.TryPop(out DialoguePiece result))
        {
            //关闭人物控制
            EventHandler.CallClosePlayerMoveEvent();
            //对话片段传到UI显示对话
            EventHandler.CallShowDialogueEvent(result);

            //WaitUntil()用于等待直到给定的条件为真
            //WaitUntil()需要一个委托
            //() => result.isDone是一个无参函数，每次调用时，它返回当前result.isDone的值
            yield return new WaitUntil(() => result.isDone);
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
    private void FillDialogueStack()
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
