using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KPlotController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private Animator signAnim;

    public Sprite wakeUpSprite; 

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        signAnim = transform.GetChild(0).GetComponent<Animator>();
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    private void OnEnable()
    {
        EventHandler.StrangeSound += OnStrangeSound;
        EventHandler.FindDragon += OnFindDragon;
    }

    private void OnDisable()
    {
        EventHandler.StrangeSound -= OnStrangeSound;
        EventHandler.FindDragon -= OnFindDragon;
    }

    public void KPlot1_WakeUp_1()
    {
        spriteRenderer.sprite = wakeUpSprite;
    }

    public void KPlot1_WakeUp_2()
    {
        signAnim.SetBool("IsAmazing",true);
        signAnim.SetTrigger("Amazing");
        signAnim.SetBool("IsAmazing",false);
    }

    /// <summary>
    /// 谁肚子叫
    /// </summary>
    private void OnStrangeSound()
    {
        signAnim.SetBool("IsAmazing",true);
        signAnim.SetTrigger("Amazing");
        signAnim.SetBool("IsAmazing",false);
    }

    /// <summary>
    /// 辉夜愤怒的起身
    /// </summary>
    public void KPlot1_WakeUp_3()
    {
        anim.enabled = true;
        anim.SetBool("IsIdle",true);
        anim.SetFloat("X",0f);
        anim.SetFloat("Y",-1f);
    }

    /// <summary>
    /// 辉夜的疑惑
    /// </summary>
    public void KProblem()
    {
        signAnim.SetBool("IsProblem",true);
        signAnim.SetTrigger("Problem");
        signAnim.SetBool("IsProblem",false);
    }

    private void OnFindDragon()
    {
        signAnim.SetBool("IsAmazing",true);
        signAnim.SetTrigger("Amazing");
        signAnim.SetBool("IsAmazing",false);
    }

}
