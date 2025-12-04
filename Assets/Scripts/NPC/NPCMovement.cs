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
    //临时存储信息
    //人物当前坐标所处场景
    [SerializeField]private string currentScene;
    //人物目的坐标所处场景
    private string targetScene;

    //人物开始的网格坐标
    [SerializeField]private Vector3Int startGridPosition;
    //人物当前的网格坐标
    [SerializeField]private Vector3Int currentGridPosition;
    //人物目标的网格坐标
    [SerializeField]private Vector3Int targetGridPosition;

    [Header("移动属性")]
    // NPC移动一个格子所花费的时间
    public float moveTime;
    // NPC的移动速度
    public Vector2 moveSpeed;


    //拿到NPC身上的组件
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D coll;
    private Animator anim;

    //拿到网格地图
    private Grid grid;

    //瓦片地图
    public Tilemap displayMap;
    //要绘制的瓦片
    public TileBase displayTile;


    //NPC的网格坐标是否被初始化过了
    private bool isInitialised;
    //堆栈是否已经塞入移动路径坐标
    private bool isInitMovementStep;
    //是否在瓦片地图中显示起始格子
    public bool displayStartAndFinish;
    //是否在瓦片地图中显示NPC格子路径
    public bool displayPath;
    private Stack<MovementStep> npcMovementStepStack;


    private Stack<MovementStep> movementStep;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        movementStep = new Stack<MovementStep>();
        npcMovementStepStack = new Stack<MovementStep>();
    }

    private void Update()
    {
        ShowPathOnGridMap();
    }

    private void Start()
    {
        InitNPC();
        StartCoroutine(Movement());
    }

    // private void OnEnable()
    // {
    //     EventHandler.AfterSceneloadedEvent += OnAfterSceneloadedEvent;
    // }

    // private void OnDisable()
    // {
    //     EventHandler.AfterSceneloadedEvent -= OnAfterSceneloadedEvent;
    // }

    // private void OnAfterSceneloadedEvent()
    // {
    //     //FindObjectOfType<T>()是指搜索当前加载场景中的Grid组件，即瓦片地图
    //     grid = FindObjectOfType<Grid>();
    //     CheckVisiable();

    //     if (!isInitialised)
    //     {
    //         InitNPC();
    //         isInitialised = true;
    //     }
    // }


    // /// <summary>
    // /// 切换对应场景显示对应NPC
    // /// </summary>
    // private void CheckVisiable() //添加到场景加载之后
    // {
    //     if(currentScene == SceneManager.GetActiveScene().name)
    //         SetActiveInScene();
    //     else
    //         SetInactiveScene();
    // }

    #region 设置NPC在场景中的显示情况
    //判断NPC是否显示在场景当中
    private void SetActiveInScene()
    {
        spriteRenderer.enabled = true;
        coll.enabled = true;
    }

    private void SetInactiveScene()
    {
        spriteRenderer.enabled = false;
        coll.enabled = false;
    }
    #endregion


    /// <summary>
    /// 初始化NPC的网格坐标
    /// </summary>
    private void InitNPC()
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
    private IEnumerator Movement()
    {
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
            Debug.Log("到了终点");
            rb.velocity = Vector2.zero;
            anim.SetBool("IsMoving",false);
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


    /// <summary>
    /// 可视化AStar算法路径
    /// </summary>
    private void ShowPathOnGridMap()
    {
        if (displayMap != null && displayTile != null)
        {
            if (displayStartAndFinish)
            {
                displayMap.SetTile(startGridPosition,displayTile);
                displayMap.SetTile(targetGridPosition,displayTile);
            }
            else
            {
                displayMap.SetTile(startGridPosition,null);
                displayMap.SetTile(targetGridPosition,null);
            }

            if (displayPath)
            {
                AStar.Instance.BuildPath(currentScene,(Vector2Int)startGridPosition,(Vector2Int)targetGridPosition,npcMovementStepStack);

                foreach (var step in npcMovementStepStack)
                {
                    displayMap.SetTile((Vector3Int)step.gridCoordinate,displayTile);
                }

            }
            else
            {
                if(npcMovementStepStack.Count > 0)
                {
                    foreach (var step in npcMovementStepStack)
                    {
                        displayMap.SetTile((Vector3Int)step.gridCoordinate,null);
                    }
                    npcMovementStepStack.Clear();
                }
            }
        }
    }
    
}
