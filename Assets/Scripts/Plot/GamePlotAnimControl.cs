using System.Collections;
using UnityEngine;

public class GamePlotAnimControl : MonoBehaviour
{
    public GameObject newGamePanel;
    public TextAutoPlay newGameText;

    public string NextScene;

    private void OnEnable()
    {
        StartCoroutine(WaitPlayNewGamePlot());
    }

    private void OnDisable()
    {
        
    }




    /// <summary>
    /// 等待新游戏剧情播放完毕
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitPlayNewGamePlot()
    {
        // 等待开头剧情播放完毕
        yield return new WaitUntil(() => newGameText.playIsOver);
        // 缓冲时间
        yield return new WaitForSeconds(2f); 

        EventHandler.CallTransitionEvent(NextScene,new Vector3(0,0,0));
    }
}
