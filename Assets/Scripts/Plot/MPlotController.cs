using System.Collections;
using UnityEngine;

public class MPlotController : MonoBehaviour
{
    private Animator anim;
    private Animator signAnim;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private NPCMovement NPCMovement;
    private GameObject cameraObject;

    public Sprite MPlot1_WakeUped;

    //摄像机移动是否结束
    private bool cameraMoveIsOver;
    


    private void Awake()
    {
        anim = GetComponent<Animator>();
        signAnim = transform.GetChild(0).GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        NPCMovement = GetComponent<NPCMovement>();
        cameraObject = GameObject.FindWithTag("CameraObject");
    }

    private void OnEnable()
    {
        EventHandler.Plot1_MJumpAndWalk += OnPlot1_MJumpAndWalk;
        EventHandler.MPlot1_CameraAndMove += OnMPlot1_CameraAndMove;
        EventHandler.Plot1_MFindWhat += OnPlot1_MFindWhat;
        EventHandler.StrangeSound += OnStrangeSound;
        EventHandler.FindDragon += OnFindDragon;
    }

    private void OnDisable()
    {
        EventHandler.Plot1_MJumpAndWalk -= OnPlot1_MJumpAndWalk;
        EventHandler.MPlot1_CameraAndMove -= OnMPlot1_CameraAndMove;
        EventHandler.Plot1_MFindWhat -= OnPlot1_MFindWhat;
        EventHandler.StrangeSound -= OnStrangeSound;
        EventHandler.FindDragon -= OnFindDragon;
    }

    private void OnPlot1_MFindWhat()
    {
        StartCoroutine(MPlot1_MFindWhat());
    }

    private void OnMPlot1_CameraAndMove()
    {
        StartCoroutine(MPlot1_CameraAndMove());
    }

    private void OnPlot1_MJumpAndWalk()
    {
        StartCoroutine(MPlot1_JumpAndWalk());
    }

    public void OnMPlot1_LookAround()
    {
        StartCoroutine(MPlot1_LookAround());
    }

    public void OnMPlot1_AngryWalk()
    {
        StartCoroutine(MPlot1_AngryWalk());
    }

    
    private void OnFindDragon()
    {
        StartCoroutine(FindDragon());
    }


    /// <summary>
    /// 动画：苏醒_眼睛半睁
    /// </summary>
    public void MPlot1_WakeUp_1()
    {
        anim.enabled = true;
        anim.SetBool("WakeUp",true);
    }

    /// <summary>
    /// 动画：苏醒_眼睛全睁
    /// </summary>
    public void MPlot1_WakeUp_2()
    {
        anim.SetBool("WakeUp",false);
        anim.enabled = false;
        spriteRenderer.sprite = MPlot1_WakeUped;
    }

    /// <summary>
    /// 动作：跳起来，走几步
    /// </summary>
    private IEnumerator MPlot1_JumpAndWalk()
    {
        //跳起来
        // rb.velocity = new Vector2(0,20);
        // yield return new WaitForSeconds(0.1f);
        // rb.velocity = new Vector2(0,-20);
        // yield return new WaitForSeconds(0.1f);
        // rb.velocity = new Vector2(0,0);
        //yield return new WaitForSeconds(1f);

        // 站起来
        anim.enabled = true;
        anim.SetBool("IsIdle",true);

        yield return null;
        // 走几步
        // 给定起始坐标
        NPCMovement.startGridPosition = new Vector3Int(-7,-10);
        NPCMovement.targetGridPosition = new Vector3Int(-7,-13);
        //执行走路
        NPCMovement.InitNPC();
        StartCoroutine(NPCMovement.Movement());
        yield return new WaitUntil(() => NPCMovement.moveToTarget);
        
        Debug.Log("到达目的地");
        GamePlotManager.Instance.MJumpAndWalkisOver = true;
    }

    /// <summary>
    /// 动作：妹红环顾四周
    /// </summary>
    private IEnumerator MPlot1_LookAround()
    {
        // 朝左看
        anim.SetFloat("Y",0f);
        anim.SetFloat("X",-1f);
        yield return new WaitForSeconds(1f);
        // 朝上看
        anim.SetFloat("X",0f);
        anim.SetFloat("Y",1f);
        yield return new WaitForSeconds(1f);
        // 朝右看
        anim.SetFloat("Y",0f);
        anim.SetFloat("X",1f);
        yield return new WaitForSeconds(1f);
        // 回到原位置
        anim.SetFloat("X",0f);
        anim.SetFloat("Y",-1f);
        yield return new WaitForSeconds(1f);
    }

    /// <summary>
    /// 缓慢运镜 + 妹红移动
    /// </summary>
    /// <returns></returns>
    private IEnumerator MPlot1_CameraAndMove()
    {
        //摄像机移动
        StartCoroutine(CameraMove(false,-11f,0.125f,40));
        
        // 摄像机与人物移动的时间差
        yield return new WaitForSeconds(1.3f);

        //人物移动
        NPCMovement.startGridPosition = new Vector3Int(-7,-13);
        NPCMovement.targetGridPosition = new Vector3Int(-14,-20);
        //NPCMovement.InitNPC();
        StartCoroutine(NPCMovement.Movement());
        yield return new WaitUntil(() => NPCMovement.moveToTarget);

        GamePlotManager.Instance.MCameraAndWalkisOver = true;
    }

    /// <summary>
    /// 妹红朝左看，发出感叹号，随后镜头快速移动
    /// </summary>
    /// <returns></returns>
    private IEnumerator MPlot1_MFindWhat()
    {
        yield return new WaitForSeconds(1f);
        // 朝左看
        anim.SetFloat("Y",0f);
        anim.SetFloat("X",-1f);

        // 出现感叹号
        signAnim.SetBool("IsAmazing",true);
        signAnim.SetTrigger("Amazing");

        yield return new WaitForSeconds(0.3f);

        // 摄像机快速移动
        StartCoroutine(CameraMove(true,-8f,0.016f,30));

        yield return new WaitUntil(() => cameraMoveIsOver);
        signAnim.SetBool("IsAmazing",false);
        GamePlotManager.Instance.MAmazingAndCamera = true;
    }

    /// <summary>
    /// 妹红走上前去救辉夜
    /// </summary>
    public void MPlot1_WalkToHelpK()
    {
        NPCMovement.startGridPosition = new Vector3Int(-14,-20);
        NPCMovement.targetGridPosition = new Vector3Int(-16,-20);
        StartCoroutine(NPCMovement.Movement());
    }

    /// <summary>
    /// 妹红对辉夜的梦话感到尴尬
    /// </summary>
    public void MPlot1_Awkward()
    {
        // 出现问号
        signAnim.SetBool("IsProblem",true);
        signAnim.SetTrigger("Problem");
        signAnim.SetBool("IsProblem",false);
    }

    /// <summary>
    /// 妹红愤怒的踱步
    /// </summary>
    private IEnumerator MPlot1_AngryWalk()
    {
        // 后撤步
        NPCMovement.moveTime = 0.3f;
        NPCMovement.startGridPosition = new Vector3Int(-16,-19);
        NPCMovement.targetGridPosition = new Vector3Int(-12,-19);
        StartCoroutine(NPCMovement.Movement());

        yield return new WaitUntil(() => NPCMovement.moveToTarget);
        
        //猛的往前冲
        NPCMovement.moveTime = 0.1f;
        NPCMovement.startGridPosition = new Vector3Int(-12,-19);
        NPCMovement.targetGridPosition = new Vector3Int(-16,-19);
        StartCoroutine(NPCMovement.Movement());
        //NPCMovement.moveTime = 0.5f;
    }

    /// <summary>
    /// 妹红朝下看
    /// </summary>
    public void MPlot1_LookDown()
    {
        anim.SetFloat("X",0f);
        anim.SetFloat("Y",-1f);
    }

    /// <summary>
    /// 妹红朝左看
    /// </summary>
    public void MPlot1_LookLeft()
    {
        anim.SetFloat("X",-1f);
        anim.SetFloat("Y",0f);
    }

    /// <summary>
    /// 谁肚子叫？
    /// </summary>
    private void OnStrangeSound()
    {
        signAnim.SetBool("IsAmazing",true);
        signAnim.SetTrigger("Amazing");
        signAnim.SetBool("IsAmazing",false);
        //这里应该是等待音效结束后再去修改布尔值
        GamePlotManager.Instance.strangeSound = true;
    }

    /// <summary>
    /// 妹红发现敌人
    /// </summary>
    /// <returns></returns>
    private IEnumerator FindDragon()
    {
        signAnim.SetBool("IsAmazing",true);
        signAnim.SetTrigger("Amazing");
        signAnim.SetBool("IsAmazing",false);
        yield return new WaitForSeconds(0.5f);
        MPlot1_LookDown();
        GamePlotManager.Instance.findDragon = true;
    }





    
    /// <summary>
    /// 移动摄像机跟随的物体，moveTime*moveNumber为移动的时间总花费
    /// </summary>
    /// <param name="isHorizontal">是否横向移动</param>
    /// <param name="targetDistance">移动的距离</param>
    /// <param name="moveTime">每次移动的间隔时间，越小移动越快速</param>
    /// <param name="moveNumber">移动的次数，越大移动越流畅(可以看作是行动过程的帧数)</param>
    /// <returns></returns>
    private IEnumerator CameraMove(bool isHorizontal,float moveDistance,float moveTime,int moveNumber)
    {
        cameraMoveIsOver = false;
        if (isHorizontal)
        {
            float moveOnceDistance = moveDistance / moveNumber;
            for (int i = 0; i < moveNumber; i++)
            {
                cameraObject.transform.position += new Vector3(moveOnceDistance,0,0);
                yield return new WaitForSeconds(moveTime);
            }
        }
        else
        {
            float moveOnceDistance = moveDistance / moveNumber;
            for (int i = 0; i < moveNumber; i++)
            {
                cameraObject.transform.position += new Vector3(0,moveOnceDistance,0);
                yield return new WaitForSeconds(moveTime);
            }
        }
        cameraMoveIsOver = true;
    }

}
