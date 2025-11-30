using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    //淡入淡出画布
    public CanvasGroup fadeCanvasGroup;
    //是否在淡入淡出
    private bool isFade;

    private void Start()
    {
        fadeCanvasGroup = FindObjectOfType<CanvasGroup>();
    }

    private void OnEnable()
    {
        //场景切换显示效果
        EventHandler.TransitionEvent += OnTransitionEvent;
        //进入战斗显示效果
        EventHandler.BattleStartEvent += OnBattleStartEvent;
    }

    private void OnDisable()
    {
        EventHandler.TransitionEvent -= OnTransitionEvent;
        //进入战斗显示效果
        EventHandler.BattleStartEvent -= OnBattleStartEvent;
    }

    private void OnTransitionEvent(string sceneToGo, Vector3 positionToGo)
    {
        StartCoroutine(Transition(sceneToGo,positionToGo));
    }


    private void OnBattleStartEvent(string battleBack, BattleAttributeDataList_SO enemyTeam)
    {
        StartCoroutine(BattleStartFade());
    }


    private IEnumerator Transition(string sceneToGo,Vector3 positionToGo)
    {
        //场景逐渐变黑
        yield return Fade(1);

        //卸载当前激活的场景
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        //加载目标场景并激活
        yield return LoadSceneSetActive(sceneToGo);

        //移动人物坐标
        EventHandler.CallMoveToPosition(positionToGo);

        //场景逐渐出现
        yield return Fade(0);
    }


    /// <summary>
    /// 加载场景并激活
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadSceneSetActive(string sceneToGo)
    {
        yield return SceneManager.LoadSceneAsync(sceneToGo,LoadSceneMode.Additive);

        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);
    }


    private IEnumerator BattleStartFade()
    {
        //场景直接变黑
        fadeCanvasGroup.alpha = 1;

        yield return new WaitForSeconds(1f);

        //场景逐渐出现
        yield return Fade(0);
    }


    /// <summary>
    /// 淡入淡出场景
    /// </summary>
    /// <param name="targetAlpha">1是黑，0是透明</param>
    /// <returns></returns>
    private IEnumerator Fade(float targetAlpha)
    {
        isFade = true;

        //鼠标射线遮挡，鼠标无法互动场景中的物体
        fadeCanvasGroup.blocksRaycasts = true;

        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / Settings.fadeDuration; //Mathf.Abs()取绝对值

        //Mathf.Approximately()比较函数，比较两个数是否相等，返回布尔值Approximately表示近似比较
        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            //Mathf.MoveTowards()趋近函数，让fadeCanvasGroup.alpha以speed * Time.deltaTime的速度趋近targetAlpha
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }

        fadeCanvasGroup.blocksRaycasts = false;
        isFade = false;
    }
}
