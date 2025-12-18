using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    // 实现菜单淡入淡出
    private CanvasGroup fadeCanvas;
    // O键设置菜单
    private GameObject O;

    public Save_Audio_SO save_Audio_SO;

    public Slider BGMSilder;
    public Slider SESlider;




    [Header("设置菜单浮现/消失时间")]
    public float fadeDuration;

    private void Awake()
    {
        fadeCanvas = GetComponent<CanvasGroup>();
        O = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        O.SetActive(false);
        fadeCanvas.alpha = 0;
    }

    private void OnEnable()
    {
        EventHandler.GameSettings_ODown += OnO;
    }

    private void OnDisable()
    {
        EventHandler.GameSettings_ODown -= OnO;
    }


    /// <summary>
    /// 按下O键
    /// </summary>
    private void OnO()
    {
        StartCoroutine(OpenSettings_O(1)); //打开游戏设置L
        //同步音量数据
        BGMSilder.value = (save_Audio_SO.gameAudioVolume.BGMVolume + 80) / 100;
        SESlider.value = (save_Audio_SO.gameAudioVolume.SEVolume + 80) / 100;
    }

    /// <summary>
    /// 关闭游戏设置O
    /// </summary>
    public void CloseO()
    {
        StartCoroutine(OpenSettings_O(0));
    }


    /// <summary>
    /// 打开/关闭游戏设置L
    /// </summary>
    /// <param name="i">1为打开设置，0为关闭设置</param>
    private IEnumerator OpenSettings_O(int i)
    {
        switch (i)
        {
            case 0:
                yield return Fade(0);
                O.SetActive(false);
                break;
            case 1:
                O.SetActive(true);
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
        while (!Mathf.Approximately(fadeCanvas.alpha, targetAlpha))
        {
            fadeCanvas.alpha = Mathf.MoveTowards(fadeCanvas.alpha, targetAlpha, 2f * Time.deltaTime);
            yield return null;
        }
    }
    
}
