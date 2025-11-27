using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ActionButton : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    //该按钮的类型
    public ButtonType buttonType;
    private Image highlight;
    public UnityEvent attackButtonStart;

    //父物体Action被激活时（玩家可操作时）一开始就执行的方法
    private void Awake()
    {
        highlight = transform.GetChild(0).GetComponent<Image>();
    }

    private void Start()
    {
        highlight.enabled = false;
    }


    /// <summary>
    /// 鼠标进入
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        highlight.enabled = true;
    }

    /// <summary>
    /// 鼠标离开
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        highlight.enabled = false;
    }

    //鼠标按下
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("按下了");
        //触发按下按钮的UI事件
        attackButtonStart?.Invoke();
        //告诉BattleManager现在是什么按钮模式
        BattleManager.Instance.currentButtonType = buttonType;
    }





    #region 鼠标射线检测UI
    // void Update()
    // {
    //     if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) // 使用新的输入系统检测鼠标左键按下
    //     {
    //         DebugRaycastUnderMouse();
    //     }
    // }
    // public void DebugRaycastUnderMouse()
    // {
    //     EventSystem eventSystem = EventSystem.current;
    //     if (eventSystem == null)
    //     {
    //         Debug.LogWarning("场景中没有EventSystem。");
    //         return;
    //     }

    //     PointerEventData pointerData = new PointerEventData(eventSystem);
    //     // 使用新的输入系统获取鼠标位置
    //     pointerData.position = Mouse.current.position.ReadValue();

    //     List<RaycastResult> results = new List<RaycastResult>();
    //     GraphicRaycaster raycaster = FindObjectOfType<GraphicRaycaster>();
    //     if (raycaster != null)
    //     {
    //         raycaster.Raycast(pointerData, results);
    //     }

    //     if (results.Count > 0)
    //     {
    //         Debug.Log("鼠标下方检测到的UI对象（按绘制顺序，最后一个是顶层）：");
    //         for (int i = 0; i < results.Count; i++)
    //         {
    //             Debug.Log($"{i + 1}. {results[i].gameObject.name} (Layer: {LayerMask.LayerToName(results[i].gameObject.layer)})");
    //         }
    //         Debug.Log($"最顶层对象是: {results[0].gameObject.name}");
    //     }
    //     else
    //     {
    //         Debug.Log("鼠标下方没有检测到UI对象。");
    //     }
    // }
    #endregion


}