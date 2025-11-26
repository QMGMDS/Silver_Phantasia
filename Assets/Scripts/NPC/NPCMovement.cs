using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using SP.AStar;
using UnityEngine;
using UnityEngine.SceneManagement;

//挂载在NPC身上

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class NPCMovement : MonoBehaviour
{
    //临时存储信息
    //人物当前坐标所处场景
    private string currentScene;
    //人物目的坐标所处场景
    private string targetScene;
    //人物当前的网格坐标
    private Vector3Int currentGridPosition;
    //人物目标的网格坐标
    private Vector3Int targetGridPosition;

    //创建一个属性，专门为currentScene赋值
    private string StartScene { set => currentScene = value; }

    [Header("移动属性")]
    //NPC的移动速度
    public float normalSpeed = 2f;
    //存储动画的X，Y
    private Vector2 dir;
    public bool isMoving;

    //拿到NPC身上的组件
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D coll;
    private Animator anim;
    //拿到网格地图
    private Grid grid;

    //NPC的网格坐标是否被初始化过了
    private bool isInitialised;


    private Stack<MovementStep> movementStep;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        InitNPC();
    }

    private void OnEnable()
    {
        //注册场景加载之后的事件
        EventHandler.AfterSceneloadedEvent += OnAfterSceneloadedEvent;
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneloadedEvent -= OnAfterSceneloadedEvent;
    }




    private void OnAfterSceneloadedEvent()
    {
        //FindObjectOfType<T>()是指搜索当前加载场景中的Grid组件，即瓦片地图
        grid = FindObjectOfType<Grid>();
        CheckVisiable();

        if (!isInitialised)
        {
            InitNPC();
            isInitialised = true;
        }
    }


    /// <summary>
    /// 切换对应场景显示对应NPC
    /// </summary>
    private void CheckVisiable() //添加到场景加载之后
    {
        if(currentScene == SceneManager.GetActiveScene().name)
            SetActiveInScene();
        else
            SetInactiveScene();
    }

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
        currentGridPosition = grid.WorldToCell(transform.position);
        //将人物当前坐标变成网格中心点坐标,让人物从网格中心点开始移动
        transform.position = new Vector3(currentGridPosition.x+Settings.gridCellSize/2f,currentGridPosition.y+Settings.gridCellSize/2,0);
    }

    public void BuildPath()
    {
        //先清空堆栈
        movementStep.Clear();
        //获取路径，存入堆栈中
        AStar.Instance.BuildPath(currentScene,(Vector2Int)currentGridPosition,(Vector2Int)targetGridPosition,movementStep);

        if (movementStep.Count > 1)
        {
            
        }
    }
}
