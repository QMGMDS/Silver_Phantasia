using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MenuBackManager : MonoBehaviour
{
    //图片仓库
    public BackImage_SO backImage_SO;

    //图片浮现时间
    public float fadeDuration;

    //图片改变间隔
    public float changeDuration;

    //避免相邻两张图片相同
    private int lastRandom;

    //在底部的图片
    [SerializeField]private Image backImageOne;
    //浮现出来的图片
    [SerializeField]private Image backImageTwo;

    //准备加载的图片
    [SerializeField]private Sprite currentSprite;
    

    private void Awake()
    {
        backImageOne = transform.GetChild(0).GetComponent<Image>();
        backImageTwo = transform.GetChild(1).GetComponent<Image>();
        backImageOne.color = new Color(255,255,255,0);
        backImageTwo.color = new Color(255,255,255,0);
    }

    private void OnEnable()
    {
        StartCoroutine(InitMenuBack());
    }

    /// <summary>
    /// 刚进入游戏的初始菜单背景
    /// </summary>
    private IEnumerator InitMenuBack()
    {
        //初次随机
        int randomInt = Random.Range(0,backImage_SO.BackImages.Count);
        currentSprite = backImage_SO.BackImages[randomInt];
        backImageTwo.sprite = currentSprite;
        lastRandom = randomInt;
        // 缓慢浮现图片
        backImageTwo.DOFade(1,fadeDuration);
        yield return new WaitForSeconds(changeDuration);
        StartCoroutine(ImageChangeLoop());
    }


    /// <summary>
    /// 图片循环改变
    /// </summary>
    private IEnumerator ImageChangeLoop()
    {
        ChangeImage();
        yield return new WaitForSeconds(changeDuration);
        StartCoroutine(ImageChangeLoop());
    }


    private void ChangeImage()
    {
        //交换图片，画面不变，让backImageTwo图片可以浮现
        backImageOne.sprite = backImageTwo.sprite;
        backImageOne.color = new Color(255,255,255,255);
        backImageTwo.color = new Color(255,255,255,0);
        //浮现图片
        GetSprite();
        backImageTwo.sprite = currentSprite;
        backImageTwo.DOFade(1,fadeDuration);
    }

    /// <summary>
    /// 随机不重复的获得下一张图片
    /// </summary>
    private void GetSprite()
    {
        //在[0,backImage_SO.BackImages.Count)范围内生成一个随机整数
        int randomInt = Random.Range(0,backImage_SO.BackImages.Count);
        
        if(lastRandom != randomInt)
        {
            currentSprite = backImage_SO.BackImages[randomInt];
            lastRandom = randomInt;
        }
        else
        {
            GetSprite();
        }
    }

}
