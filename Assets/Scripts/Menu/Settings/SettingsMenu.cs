using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class SettingsMenu : MonoBehaviour
{
    // 实现菜单淡入淡出
    private CanvasGroup fadeCanvas;
    // 菜单是否正在淡入淡出
    private bool isFade;
    // L键设置菜单
    private GameObject L;
    // F键背包
    private GameObject F;
    // 是否打开背包，让F键控制背包的打开和关闭
    private bool openF;



    [Header("设置菜单浮现/消失时间")]
    public float fadeDuration;

    private void Awake()
    {
        fadeCanvas = GetComponent<CanvasGroup>();
        L = transform.GetChild(0).gameObject;
        F = transform.GetChild(1).gameObject;
    }

    private void Start()
    {
        L.SetActive(false);
        F.SetActive(false);
        fadeCanvas.alpha = 0;
    }

    private void OnEnable()
    {
        EventHandler.GameSettings_LDown += OnL;
        EventHandler.GameSettings_FDown += OnF;
    }

    private void OnDisable()
    {
        EventHandler.GameSettings_LDown -= OnL;
        EventHandler.GameSettings_FDown -= OnF;
    }


    /// <summary>
    /// 按下L键
    /// </summary>
    private void OnL()
    {
        StartCoroutine(OpenSettings_L(1)); //打开游戏设置L
    }

    /// <summary>
    /// 按下F键
    /// </summary>
    private void OnF()
    {
        openF = !openF;
        if (openF)
        {
            StartCoroutine(OpenSettings_F(1)); //打开游戏背包F
        }
        else
        {
            StartCoroutine(OpenSettings_F(0)); //关闭游戏背包F
        }
    }

    /// <summary>
    /// 关闭游戏设置L
    /// </summary>
    public void CloseL()
    {
        StartCoroutine(OpenSettings_L(0));
    }

    /// <summary>
    /// 打开/关闭游戏设置L
    /// </summary>
    /// <param name="i">1为打开设置，0为关闭设置</param>
    private IEnumerator OpenSettings_L(int i)
    {
        switch (i)
        {
            case 0:
                yield return Fade(0);
                L.SetActive(false);
                break;
            case 1:
                L.SetActive(true);
                StartCoroutine(Fade(1));
                break;
        }
    }

    /// <summary>
    /// 打开/关闭游戏背包F
    /// </summary>
    /// <param name="i">1为打开设置，0为关闭设置</param>
    private IEnumerator OpenSettings_F(int i)
    {
        switch (i)
        {
            case 0:
                yield return Fade(0);
                F.SetActive(false);
                break;
            case 1:
                F.SetActive(true);
                StartCoroutine(Fade(1));
                break;
        }
    }

    /// <summary>
    /// 菜单的渐入渐出
    /// </summary>
    /// <param name="targetAlpha"></param>
    /// <returns></returns>
    private IEnumerator Fade(float targetAlpha)
    {
        isFade = true;
        var speed = (targetAlpha - fadeCanvas.alpha) / 20;
        for (int i = 0; i < 20; i++)
        {
            fadeCanvas.alpha += speed;
            yield return new WaitForSeconds(fadeDuration/20);
        }
        isFade = false;
    }
    
}
