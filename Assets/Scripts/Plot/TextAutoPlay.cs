using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

// 文本自动播放
public class TextAutoPlay : MonoBehaviour
{
    //拿取剧情
    public PlotText_SO plotText_SO;

    //拿取剧情显示处
    private Text playText;

    //记录每一句的剧情以及翻页标识
    private List<string> plotTextPiece = new List<string>();

    //该剧情是否播放完毕
    public bool playIsOver;

    private void Awake()
    {
        playText = GetComponent<Text>();
        InitPlotText();
    }


    private void Start()
    {
        playText.text = "";
        StartCoroutine(AutoPlayPlot());
    }

    /// <summary>
    /// 处理剧情字符串
    /// </summary>
    private void InitPlotText()
    {
        playIsOver = false;
        string temp = "";
        for (int i = 0; i < plotText_SO.plotText.Length; i++)
        {
            // 遇到\n添加语句片段
            if (plotText_SO.plotText[i] == '\n')
            {
                plotTextPiece.Add(temp);
                temp = "";
            }
            // 遇到#进行翻页标注
            else if(plotText_SO.plotText[i] == '#')
            {
                plotTextPiece.Add("#");
            }
            else
            {
                temp += plotText_SO.plotText[i];
            }
        }
    }


    /// <summary>
    /// 自动打印剧情
    /// </summary>
    /// <returns></returns>
    private IEnumerator AutoPlayPlot()
    {
        foreach (var piece in plotTextPiece)
        {
            if (piece == "#")
            {
                yield return new WaitForSeconds(2f);
                //翻页
                playText.text = "";
            }
            else
            {
                yield return PlayPiece(piece);
                yield return new WaitForSeconds(1f);
            }
        }
        Debug.Log("打印完成");
        //载入正式游戏
        playIsOver = true;
    }



    /// <summary>
    /// 播放一个剧情片段
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayPiece(string piece)
    {
        foreach (char c in piece)
        {
            playText.text += c;
            yield return new WaitForSeconds(0.1f);
        }
        playText.text += "\n";
    }


}
