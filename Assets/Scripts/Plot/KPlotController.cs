using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KPlotController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator signAnim;

    public Sprite wakeUpSprite; 

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        signAnim = transform.GetChild(0).GetComponent<Animator>();
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
}
