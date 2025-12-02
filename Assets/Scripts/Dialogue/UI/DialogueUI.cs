using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class DialogueUI : MonoBehaviour
{
    //对话框
    public GameObject dialogueBox;
    //对话人名字
    public TextMeshProUGUI dialogueName;
    //对话内容
    public Text dialogueText;
    //对话头像
    public Image faceLeft,faceRight;
    //对话按键提示
    public GameObject continuteBox;


    //对话选择文本一
    public TextMeshProUGUI optionText1;
    //对话选择文本二
    public TextMeshProUGUI optionText2;

    //选项按钮是否被按下
    public bool isButtonDown;


    //添加历史对话
    public TextMeshProUGUI historyContent;

    

    private void Awake()
    {
        continuteBox.SetActive(false);
    }

    private void OnEnable()
    {
        EventHandler.ShowDialogueEvent += OnShowDialogueEvent;
        EventHandler.ShowDialogueOptionEvent += OnShowDialogueOptionEvent;
    }

    private void OnDisable()
    {
        EventHandler.ShowDialogueEvent -= OnShowDialogueEvent;
        EventHandler.ShowDialogueOptionEvent -= OnShowDialogueOptionEvent;
    }

    private void OnShowDialogueEvent(DialoguePiece piece)
    {
        StartCoroutine(ShowDialogue(piece));
    }

    private void OnShowDialogueOptionEvent(DialogueOption option)
    {
        StartCoroutine(ShowOption(option));
    }

    /// <summary>
    /// 对话UI显示
    /// </summary>
    /// <param name="piece"></param>
    /// <returns></returns>
    private IEnumerator ShowDialogue(DialoguePiece piece)
    {
        if (piece != null)
        {
            piece.isDone = false;
            //输出文本前先清空文本预览显示
            dialogueText.text = string.Empty;

            dialogueBox.SetActive(true);
            continuteBox.SetActive(false);
            
            if (piece.onLeft)
            {
                faceLeft.gameObject.SetActive(true);
                faceRight.gameObject.SetActive(false);
                faceLeft.sprite = piece.faceImage;
                dialogueName.text = piece.dialogueName;
            }
            else
            {
                faceLeft.gameObject.SetActive(false);
                faceRight.gameObject.SetActive(true);
                faceRight.sprite = piece.faceImage;
                dialogueName.text = piece.dialogueName;
            }
            //DOText()实现了逐渐打印对话内容
            //yield return等待DOText()这个方法的完成WaitForCompletion()
            yield return dialogueText.DOText(piece.dialogueText, 1f).WaitForCompletion();
            //记录历史对话
            historyContent.text += "\n\n" + dialogueName.text + "：" + dialogueText.text;
    
            piece.isDone = true;

            if(piece.hasToPause && piece.isDone && !piece.hasToOption)
                continuteBox.SetActive(true);
        }
        else
        {
            //piece为空则关闭对话框
            dialogueBox.SetActive(false);
            faceLeft.gameObject.SetActive(false);
            faceRight.gameObject.SetActive(false);
            continuteBox.SetActive(false);
            yield break;
        }
    }


    /// <summary>
    /// 对话选择显示
    /// </summary>
    public IEnumerator ShowOption(DialogueOption option)
    {
        if(option != null)
        {
            option.isChoose = false;
            //该方法被调用时说明执行了对话选项显示的事件
            //打开对话显示物体
            optionText1.gameObject.SetActive(true);
            optionText2.gameObject.SetActive(true);

            //同步选项内容
            optionText1.text = option.option1Text;
            optionText2.text = option.option2Text;


            //做出选择...
            Debug.Log("选择ing");

            yield return new WaitUntil(() => isButtonDown);

            Debug.Log("选项被按下了");

            //选择结束后关闭选项框
            optionText1.gameObject.SetActive(false);
            optionText2.gameObject.SetActive(false);

            option.isChoose = true;
        }
        else
        {
            //option为空关闭选项框
            optionText1.gameObject.SetActive(false);
            optionText2.gameObject.SetActive(false);
            yield break;
        }
        
    }


    /// <summary>
    /// 选项一被按下
    /// </summary>
    public void ButtonStartOne()
    {
        isButtonDown = true;
        EventHandler.CallDialogueOptionOneDownEvent();

        //记录历史选项
        historyContent.text += "\n\n" + optionText1.text;
    }

    /// <summary>
    /// 选项二被按下
    /// </summary>
    public void ButtonStartTwo()
    {
        isButtonDown = true;
        EventHandler.CallDialogueOptionTwoDownEvent();

        //记录历史选项
        historyContent.text += "\n\n" + optionText2.text;
    }
    
}
