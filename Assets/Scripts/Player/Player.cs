using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerInputControl playerInput;
    private Rigidbody2D rb;
    private Animator anim;

    [Header("移动方向")]
    private Vector2 inputDirection;
    [Header("移动速度")]
    public float speed;
    [SerializeField]private Vector2 currentSpeed;
    [Header("人物属性")]
    [SerializeField] private bool isMoving;
    [SerializeField] private bool canMoving;
    
    [Header("玩家阵营")]
    public BattleAttributeDataList_SO playerTeam;


    private void Awake()
    {
        playerInput = new PlayerInputControl();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //空格键按下时
        playerInput.GamePlay.Interact.started += Interact;
    }

    private void OnEnable()
    {
        playerInput.Enable();
        EventHandler.ClosePlayerMoveEvent += OnClosePlayerMoveEvent;
        EventHandler.OpenPlayerMoveEvent += OnOpenPlayerMoveEvent;
        EventHandler.MoveToPositionEvent += OnMoveToPositionEvent;
    }

    private void OnDisable()
    {
        playerInput.Disable();
        EventHandler.ClosePlayerMoveEvent -= OnClosePlayerMoveEvent;
        EventHandler.OpenPlayerMoveEvent += OnOpenPlayerMoveEvent;
        EventHandler.MoveToPositionEvent -= OnMoveToPositionEvent;
    }


    private void Update()
    {
        if (canMoving)
        {
            PlayerInput();
        }
        MoveAnimations();
    }

    private void FixedUpdate()
    {
        Movement();
    }
    
    
    /// <summary>
    /// 读取人物控制输入
    /// </summary>
    private void PlayerInput()
    {
        inputDirection = playerInput.GamePlay.Move.ReadValue<Vector2>();
        isMoving = (inputDirection != Vector2.zero);
    }

    /// <summary>
    /// 人物移动
    /// </summary>
    private void Movement()
    {
        currentSpeed = new Vector2(inputDirection.x*speed*Time.deltaTime, inputDirection.y*speed*Time.deltaTime);
        rb.velocity = currentSpeed;
    }

    /// <summary>
    /// 人物移动动画
    /// </summary>
    private void MoveAnimations()
    {
        anim.SetBool("IsMoving",isMoving);
        if (isMoving)
        {
            anim.SetFloat("X",inputDirection.x);
            anim.SetFloat("Y",inputDirection.y);
        }
    }


    #region 按键检测
    private void Interact(InputAction.CallbackContext context)
    {
        EventHandler.CallInteractButtonStartEvent();
    }
    #endregion

    /// <summary>
    /// 关闭人物移动控制
    /// </summary>
    private void OnClosePlayerMoveEvent()
    {
        canMoving = false;
        inputDirection = Vector2.zero;
        isMoving = false;
    }

    /// <summary>
    /// 开启人物移动控制
    /// </summary>
    /// <exception cref="System.NotImplementedException"></exception>
    private void OnOpenPlayerMoveEvent()
    {
        canMoving = true;
        isMoving = true;
    }

    /// <summary>
    /// 场景切换坐标传送
    /// </summary>
    /// <param name="positionToGo"></param>
    private void OnMoveToPositionEvent(Vector3 positionToGo)
    {
        transform.position = positionToGo;
    }

}
