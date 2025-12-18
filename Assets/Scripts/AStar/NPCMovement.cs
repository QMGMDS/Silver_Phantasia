using System.Collections.Generic;
using SP.AStar;
using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

// 挂载在NPC身上

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class NPCMovement : MonoBehaviour
{
    //拿到NPC身上的组件
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D coll;
    private Animator anim;
    //拿到网格地图
    private Grid grid;


    [Header("观察")]
    //人物当前坐标所处场景
    [SerializeField]private string currentScene;
    //人物当前的网格坐标
    [SerializeField]private Vector3Int currentGridPosition;

    [Header("设定移动路径")]
    //人物开始的网格坐标
    public Vector3Int startGridPosition;
    //人物目的地的网格坐标
    public Vector3Int targetGridPosition;

    [Header("移动属性")]
    // NPC移动一个格子所花费的时间
    public float moveTime;
    // NPC的移动速度
    private Vector2 moveSpeed;

    private Stack<MovementStep> movementStep;
    //是否走到终点
    [HideInInspector]public bool moveToTarget;




    //堆栈是否已经塞入移动路径坐标
    private bool isInitMovementStep;
    
    private Stack<MovementStep> npcMovementStepStack;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        movementStep = new Stack<MovementStep>();
        npcMovementStepStack = new Stack<MovementStep>();
    }


    /// <summary>
    /// 初始化NPC的网格坐标
    /// </summary>
    public void InitNPC()
    {
        grid = FindObjectOfType<Grid>();
        //当前坐标转化成网格坐标，网格坐标在瓦片地图的节点上
        currentGridPosition = startGridPosition;
        //将人物当前坐标变成网格中心点坐标,让人物从网格中心点开始移动
        transform.position = new Vector3(currentGridPosition.x+Settings.gridCellSize/2,currentGridPosition.y+Settings.gridCellSize/2,0);
    }


    /// <summary>
    /// 递归：NPC行走
    /// </summary>
    /// <returns></returns>
    public IEnumerator Movement()
    {
        moveToTarget = false;
        if (!isInitMovementStep)
        {
            BuildPath();
            isInitMovementStep = true;
        }
        var step = movementStep.Pop();
        //求移动速度
        moveSpeed = new Vector2((step.gridCoordinate.x - currentGridPosition.x)/moveTime,(step.gridCoordinate.y - currentGridPosition.y)/moveTime);
        rb.velocity = moveSpeed;
        currentGridPosition = new Vector3Int(step.gridCoordinate.x,step.gridCoordinate.y,0);

        //设置动画
        anim.SetBool("IsMoving",true);
        anim.SetFloat("X",moveSpeed.x);
        anim.SetFloat("Y",moveSpeed.y);

        yield return new WaitForSeconds(moveTime);
        if(currentGridPosition != targetGridPosition)
        {
            StartCoroutine(Movement());
        }
        else
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("IsMoving",false);
            moveToTarget = true;
            isInitMovementStep = false;
        }
    }


    /// <summary>
    /// 获取行走路径，存入movementStep
    /// </summary>
    private void BuildPath()
    {
        movementStep.Clear();
        //获取路径，存入堆栈中
        AStar.Instance.BuildPath(currentScene,(Vector2Int)startGridPosition,(Vector2Int)targetGridPosition,movementStep);
    }

    
}
