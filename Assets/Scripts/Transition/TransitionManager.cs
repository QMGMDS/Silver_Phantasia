using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransitionManager : MonoBehaviour
{
    [Header("游戏启动时初始加载的场景")]
    //初始场景
    public string InitScene;

    // 进入战斗的淡入淡出画布
    private Image fadeImage;

    // 正常的淡入淡出
    private CanvasGroup normalFade;

    private void Start()
    {
        normalFade = GameObject.FindWithTag("NormalFade").GetComponent<CanvasGroup>();
        fadeImage = GameObject.FindWithTag("FadeImage").GetComponent<Image>();
        EventHandler.CallLoadSceneEvent(InitScene);
    }


    private void OnEnable()
    {
        //场景切换显示效果
        EventHandler.TransitionEvent += OnTransitionEvent;
        //进入战斗显示效果
        EventHandler.BattleStartEvent += OnBattleStartEvent;
        //加载场景
        EventHandler.LoadSceneEvent += OnLoadSceneEvent;
    }

    private void OnDisable()
    {
        //场景切换显示效果
        EventHandler.TransitionEvent -= OnTransitionEvent;
        //进入战斗显示效果
        EventHandler.BattleStartEvent -= OnBattleStartEvent;
        //加载场景
        EventHandler.LoadSceneEvent -= OnLoadSceneEvent;
    }


    private void OnTransitionEvent(string sceneToGo,Vector3 posToGo)
    {
        StartCoroutine(Transition(sceneToGo,posToGo));
    }


    private void OnBattleStartEvent(string battleBack, BattleAttributeDataList_SO enemyTeam)
    {
        StartCoroutine(BattleStartFade());
    }

    private void OnLoadSceneEvent(string loadScene)
    {
        StartCoroutine(LoadSceneSetActive(loadScene));
    }


    /// <summary>
    /// 切换场景
    /// </summary>
    /// <param name="sceneToGo"></param>
    /// <param name="positionToGo"></param>
    /// <returns></returns>
    private IEnumerator Transition(string sceneToGo,Vector3 posToGo)
    {
        //画面逐渐变黑
        yield return StartCoroutine(NormalFade(1));

        yield return new WaitForSeconds(1f);

        //移动玩家到指定位置
        EventHandler.CallMoveToPosition(posToGo);

        // 卸载当前激活的场景
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        

        // 加载目标场景并激活
        yield return LoadSceneSetActive(sceneToGo);

        

        //画面逐渐变亮
        yield return StartCoroutine(NormalFade(0));
    }


    /// <summary>
    /// 切换战斗模式 “场景 ”
    /// </summary>
    /// <returns></returns>
    private IEnumerator BattleStartFade()
    {
        //场景逐渐变黑
        yield return Fade(1);

        yield return new WaitForSeconds(1f);

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


    /// <summary>
    /// 战斗加载动画
    /// </summary>
    /// <param name="targetAlpha">1是黑，0是透明</param>
    /// <returns></returns>
    private IEnumerator Fade(float targetFillAmount)
    {
        //鼠标射线遮挡，鼠标无法互动场景中的物体
        fadeImage.raycastTarget = true;

        float speed = Mathf.Abs(fadeImage.fillAmount - targetFillAmount) / Settings.fadeDuration; //Mathf.Abs()取绝对值

        //Mathf.Approximately()比较函数，比较两个数是否相等，返回布尔值Approximately表示近似比较
        while (!Mathf.Approximately(fadeImage.fillAmount, targetFillAmount))
        {
            //Mathf.MoveTowards()趋近函数，让fadeImage.fillAmount以speed * Time.deltaTime的速度趋近targetFillAmount
            fadeImage.fillAmount = Mathf.MoveTowards(fadeImage.fillAmount, targetFillAmount, 12 * speed * Time.deltaTime);
            yield return null;
        }

        fadeImage.raycastTarget = false;
    }

    /// <summary>
    /// 正常渐入渐出动画
    /// </summary>
    /// <param name="targetAlpha">1是黑，0是透明</param>
    /// <returns></returns>
    private IEnumerator NormalFade(float targetAlpha)
    {

        while (!Mathf.Approximately(normalFade.alpha, targetAlpha))
        {
            normalFade.alpha = Mathf.MoveTowards(normalFade.alpha, targetAlpha, 2f * Time.deltaTime);
            yield return null;
        }

    }


}
