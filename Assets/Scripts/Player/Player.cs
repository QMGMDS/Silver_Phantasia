using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerInputControl playerInput;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    private GameObject playerSign;

    [Header("移动方向")]
    private Vector2 inputDirection;
    [Header("移动速度")]
    public float speed;
    [SerializeField]private Vector2 currentSpeed;
    [Header("人物属性")]
    [SerializeField] private bool isMoving;
    [SerializeField] private bool canMoving;


    private void Awake()
    {
        playerInput = new PlayerInputControl();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        playerSign = transform.GetChild(1).gameObject;
    }

    private void OnEnable()
    {
        playerInput.Enable();
        EventHandler.ClosePlayerMoveEvent += OnClosePlayerMoveEvent;
        EventHandler.OpenPlayerMoveEvent += OnOpenPlayerMoveEvent;
        EventHandler.MoveToPositionEvent += OnMoveToPositionEvent;
        EventHandler.BraveFaceChange += OnBraveFaceChange;
        EventHandler.Dungeon_FirstEntry += OnDungeon_FirstEntry;
        EventHandler.PlayerShowImageChange += OnCallPlayerShowImageChange;
        EventHandler.PlayerSign += OnPlayerSign;

        //空格键按下时
        playerInput.UI.Interact.started += Interact;
        //L键按下时
        playerInput.UI.OpenGameSettings_O.started += OpenGameSettings_O;
    }


    private void OnDisable()
    {
        playerInput.Disable();
        EventHandler.ClosePlayerMoveEvent -= OnClosePlayerMoveEvent;
        EventHandler.OpenPlayerMoveEvent -= OnOpenPlayerMoveEvent;
        EventHandler.MoveToPositionEvent -= OnMoveToPositionEvent;
        EventHandler.BraveFaceChange -= OnBraveFaceChange;
        EventHandler.Dungeon_FirstEntry -= OnDungeon_FirstEntry;
        EventHandler.PlayerSign += OnPlayerSign;
    }


    /// <summary>
    /// 修改Player图片显示
    /// </summary>
    /// <param name="obj"></param>
    private void OnCallPlayerShowImageChange(bool change)
    {
        sprite.enabled = change;
    }

    /// <summary>
    /// PlayerSign是否可见
    /// </summary>
    /// <param name="change"></param>
    private void OnPlayerSign(bool change)
    {
        playerSign.SetActive(change);
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
    /// <summary>
    /// 空格键按下
    /// </summary>
    /// <param name="context"></param>
    private void Interact(InputAction.CallbackContext context)
    {
        EventHandler.CallInteractButtonStartEvent();
    }

    /// <summary>
    /// O键按下
    /// </summary>
    /// <param name="context"></param>
    private void OpenGameSettings_O(InputAction.CallbackContext context)
    {
        EventHandler.CallGameSettings_ODown();
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

    /// <summary>
    /// 玩家朝向单次修改
    /// </summary>
    /// <param name="faceChange">1为面朝上，2为面朝下，3为面朝左，4为面朝右</param>
    private void OnBraveFaceChange(int faceChange)
    {
        switch (faceChange)
        {
            case 1:
                anim.SetFloat("X",0f);
                anim.SetFloat("Y",1f);
                break;
            case 2:
                anim.SetFloat("X",0f);
                anim.SetFloat("Y",-1f);
                break;
            case 3:
                anim.SetFloat("X",-1f);
                anim.SetFloat("Y",0f);
                break;
            case 4:
                anim.SetFloat("X",1f);
                anim.SetFloat("Y",0f);
                break;
        }
    }

    /// <summary>
    /// 勇者四处张望
    /// </summary>
    /// <param name="time">每次转向的间隔时间</param>
    /// <returns></returns>
    private IEnumerator BraveLookAround(float time)
    {
        OnBraveFaceChange(3);
        yield return new WaitForSeconds(time);
        OnBraveFaceChange(1);
        yield return new WaitForSeconds(time);
        OnBraveFaceChange(4);
        yield return new WaitForSeconds(time);
        OnBraveFaceChange(2);
    }

    /// <summary>
    /// 地牢：初入，勇者熟悉环境
    /// </summary>
    private void OnDungeon_FirstEntry()
    {
        StartCoroutine(DungeonFirstEntry());
    }
    private IEnumerator DungeonFirstEntry()
    {
        StartCoroutine(BraveLookAround(0.4f));
        yield return new WaitForSeconds(0.4f*4);
        // 四处观察后进入对话
        EventHandler.CallPlotDialogueEvent(2);
    }

}
