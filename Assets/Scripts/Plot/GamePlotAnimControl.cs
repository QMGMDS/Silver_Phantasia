using System.Collections;
using UnityEngine;

public class GamePlotAnimControl : MonoBehaviour
{
    public GameObject newGamePanel;
    public TextAutoPlay newGameText;

    private void OnEnable()
    {
        EventHandler.NewGameEvent += OnNewGameEvent;
    }

    private void OnDisable()
    {
        EventHandler.NewGameEvent -= OnNewGameEvent;
    }


    private void OnNewGameEvent()
    {
        newGamePanel.SetActive(true);
        StartCoroutine(WaitPlayNewGamePlot());
    }


    /// <summary>
    /// 等待新游戏剧情播放完毕
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitPlayNewGamePlot()
    {
        yield return new WaitUntil(() => newGameText.playIsOver);
        Debug.Log("游戏开局剧情播放完毕");
        // 缓冲时间
        yield return new WaitForSeconds(2f); 
        // 进入剧情1，准备工作
        EventHandler.CallPlotOneEvent();
        // 画面显示
        EntryPlotOne();
    }

    /// <summary>
    /// 进入剧情一，关闭开始剧情的Panel
    /// </summary>
    private void EntryPlotOne()
    {
        newGamePanel.SetActive(false);
    }
}
