using System.Collections;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public Canvas mainMenu;
    public Canvas mainCanvas;
    public Canvas dialogueCanvas;
    public Canvas fadeCanvas;

    private void Awake()
    {
        // 游戏一开始，主菜单的画布打开，其他画布关闭
        mainMenu.enabled = true;
        mainCanvas.enabled = false;
        dialogueCanvas.enabled = false;
        fadeCanvas.enabled = false;
    }


    /// <summary>
    /// 新的传奇按钮按下
    /// </summary>
    public void NewGameButtonDown()
    {
        //游戏开始
        mainMenu.enabled = false;
        mainCanvas.enabled = true;
        dialogueCanvas.enabled = true;
        fadeCanvas.enabled = true;
    }


    /// <summary>
    /// 旧的回忆按钮按下
    /// </summary>
    public void LoadGameButtonDown()
    {
        Debug.Log("旧的回忆");
    }


    /// <summary>
    /// 退出游戏按钮按下
    /// </summary>
    public void ExitGameButtonDown()
    {
        Debug.Log("退出游戏");
    }

}
