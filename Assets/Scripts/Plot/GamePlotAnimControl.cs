using System.Collections;
using System.Collections.Generic;
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
        Debug.Log("游戏开始");
    }
}
