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

    private void Awake()
    {
        continuteBox.SetActive(false);
    }

    private void OnEnable()
    {
        EventHandler.ShowDialogueEvent += OnShowDialogueEvent;
    }

    private void OnDisable()
    {
        EventHandler.ShowDialogueEvent -= OnShowDialogueEvent;
    }

    private void OnShowDialogueEvent(DialoguePiece piece)
    {
        StartCoroutine(ShowDialogue(piece));
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
            //输出文本时先清空文本预览显示
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
    
            piece.isDone = true;

            if(piece.hasToPause && piece.isDone)
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
    
}
