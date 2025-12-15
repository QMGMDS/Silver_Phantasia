using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    //渐入渐出
    private CanvasGroup canvasGroup;


    //对话选择文本一
    public TextMeshProUGUI optionText1;
    //对话选择文本二
    public TextMeshProUGUI optionText2;

    //选项按钮是否被按下
    public bool isButtonDown;
    //对话框出现动画是否播放完毕
    private bool dialogueShowIsOver;
    //对话框消失动画是否播放完毕
    private bool dialogueCloseIsOver;


    //添加历史对话
    public TextMeshProUGUI historyContent;

    // 判断选项为剧情选项还是游玩选项
    private int optionDeterminant;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        continuteBox.SetActive(false);
        canvasGroup.alpha = 0f;
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

    private void OnShowDialogueOptionEvent(DialogueOption option,int determinant)
    {
        Debug.Log(option.option1Text);
        StartCoroutine(ShowOption(option));
        optionDeterminant = determinant;
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
            StartCoroutine(DialogueCloseAnim());
            yield return new WaitUntil(() => dialogueCloseIsOver); // 等待消失动画播放完毕
            dialogueCloseIsOver = false; // 复原

            yield return new WaitForSeconds(0.2f); // 黑屏时间

            StartCoroutine(DialogueShowAnim(piece));
            yield return new WaitUntil(() => dialogueShowIsOver); // 等待出现动画播放完毕
            dialogueShowIsOver = false; // 复原

            piece.isDone = false;
            
            
            //DOText()实现了逐渐打印对话内容
            //yield return等待DOText()这个方法的完成WaitForCompletion()
            yield return dialogueText.DOText(piece.dialogueText, 0.01f).WaitForCompletion();
            //触发对话后的事件
            piece.afterTalkEvent.Invoke();
            //记录历史对话
            historyContent.text += "\n\n" + dialogueName.text + "：" + dialogueText.text;
    
            piece.isDone = true;

            if(piece.hasToPause && piece.isDone && !piece.hasToOption)
                continuteBox.SetActive(true);
        }
        else
        {
            //piece为空则关闭对话框
            StartCoroutine(DialogueCloseAnim());
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
            isButtonDown = false;
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
        // 判断是剧情型对话还是游玩型对话
        switch (optionDeterminant)
        {
            case 1:
                EventHandler.CallPlotDialogueOptionDown(1);
                break;
            case 2:
                EventHandler.CallDialogueOptionOneDownEvent();
                break;
        }
        

        //记录历史选项
        historyContent.text += "\n\n" + optionText1.text;
    }

    /// <summary>
    /// 选项二被按下
    /// </summary>
    public void ButtonStartTwo()
    {
        isButtonDown = true;
        switch (optionDeterminant)
        {
            case 1:
                EventHandler.CallPlotDialogueOptionDown(2);
                break;
            case 2:
                EventHandler.CallDialogueOptionTwoDownEvent();
                break;
        }
        

        //记录历史选项
        historyContent.text += "\n\n" + optionText2.text;
    }

    /// <summary>
    /// 利用CanvasGroup.alpha实现对话框逐渐出现动画
    /// </summary>
    /// <returns></returns>
    private IEnumerator DialogueShowAnim(DialoguePiece piece)
    {
        if(dialogueShowIsOver == true)
            yield return null;

        dialogueBox.SetActive(true);
        if (piece.onLeft)
        {
            if (piece.faceImage != null)
            {
                faceLeft.gameObject.SetActive(true);
                faceRight.gameObject.SetActive(false);
                faceLeft.sprite = piece.faceImage;
            }
            else
            {
                faceLeft.gameObject.SetActive(false);
                faceRight.gameObject.SetActive(false);
            }
            dialogueName.text = piece.dialogueName;
        }
        else
        {
            if (piece.faceImage != null)
            {
                faceLeft.gameObject.SetActive(false);
                faceRight.gameObject.SetActive(true);
                faceRight.sprite = piece.faceImage;
            }
            else
            {
                faceLeft.gameObject.SetActive(false);
                faceRight.gameObject.SetActive(false);
            }
            dialogueName.text = piece.dialogueName;
        }

        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += 0.1f;
            yield return new WaitForSeconds(0.01f);
        }

        dialogueShowIsOver = true;
    }

    /// <summary>
    /// 利用CanvasGroup.alpha实现对话框逐渐消失动画
    /// </summary>
    /// <returns></returns>
    private IEnumerator DialogueCloseAnim()
    {
        if(dialogueCloseIsOver == true)
            yield return null;

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= 0.1f;
            yield return new WaitForSeconds(0.01f);
        }

        //关闭对话之前清空文本
        dialogueText.text = string.Empty;

        dialogueBox.SetActive(false);
        faceLeft.gameObject.SetActive(false);
        faceRight.gameObject.SetActive(false);
        continuteBox.SetActive(false);

        dialogueCloseIsOver = true;
    }
    
}
