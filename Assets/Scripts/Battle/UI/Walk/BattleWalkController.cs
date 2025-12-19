using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using UnityEngine.UI;

//在每个回合的开始之前，由BattleManager计算当前是谁的回合后，BattleWalkController拿取每一个角色的path
//实现角色行动轴动画

public class BattleWalkController : MonoBehaviour
{
    //行动图片
    public List<Image> AllWalkImage;
    //图片的坐标
    public List<Rigidbody2D> AllWalkImageRb;

    public bool walkAnimIsOver;

    //便于迭代
    private int i;



    private void OnEnable()
    {
        InitWalkAnim();
    }

    

    // /// <summary>
    // /// 行动轴动画移动
    // /// </summary>
    // /// <returns></returns>
    // public IEnumerator Move()
    // {
    //     //30.5 235
    //     //735 235
    //     yield return new WaitForSeconds(1.5f);
    //     walkAnimIsOver = false;

    //     // 记录上一次的值 lastWalkImagePath
    //     foreach (var axisOfAction in BattleManager.Instance.allAxisOfAction)
    //     {
    //         axisOfAction.Record_lastWalkImagePath();
    //     }

    //     // 行动轴速度判断赋值
    //     i = 0;
    //     foreach (var axisOfAction in BattleManager.Instance.allAxisOfAction)
    //     {
    //         //确认终点的坐标
    //         //归零说明走到了终点，下次动画执行时要将之放到起点
    //         if(axisOfAction.path == 0)
    //             axisOfAction.walkImagePath = 720f;
    //         else
    //             axisOfAction.walkImagePath = axisOfAction.path / Settings.battleDistance * 720f;

    //         // 路径满了说明上一回合该角色抵达终点，初始要将之放回起点
    //         if(axisOfAction.lastWalkImagePath == 720f)
    //         {
    //             AllWalkImageRb[i].position = new Vector2(30.5f,235f);
    //             axisOfAction.walkImagePath = 0f;
    //         }

    //         axisOfAction.walkImageSpeed = Mathf.Abs(axisOfAction.walkImagePath - axisOfAction.lastWalkImagePath) / Settings.battleWalkTime;
    //         i++;
    //     }


    //     //赋予行动轴速度
    //     i = 0;
    //     foreach (var axisOfAction in BattleManager.Instance.allAxisOfAction)
    //     {
    //         AllWalkImageRb[i].velocity = new Vector2(axisOfAction.walkImageSpeed,0);
    //         i++;
    //     }
    //     yield return new WaitForSeconds(Settings.battleWalkTime);

    //     //速度归零
    //     i = 0;
    //     foreach (var axisOfAction in BattleManager.Instance.allAxisOfAction)
    //     {
    //         AllWalkImageRb[i].velocity = new Vector2(0,0);
    //         i++;
    //     }

    //     //动画播放完毕
    //     walkAnimIsOver = true;
    // }

    /// <summary>
    /// 行动轴动画的复原
    /// </summary>
    public void Recovery()
    {
        // 重置行动位置
        for (i = 0; i < AllWalkImageRb.Count; i++)
        {
            AllWalkImageRb[i].position = new Vector2(30.5f,235f);
        }
    }

    /// <summary>
    /// 初始化行动轴动画
    /// </summary>
    private void InitWalkAnim()
    {
        walkAnimIsOver = false;
        // 给图片
        int i = 0;
        foreach (var axisOfAction in BattleManager.Instance.allAxisOfAction)
        {
            AllWalkImage[i].enabled = true;
            AllWalkImage[i].sprite = axisOfAction.axisSprite;
            i++;
        }

        Recovery();

        for (int j = i; j < 4; j++)
        {
            AllWalkImage[j].enabled = false;
        }
    }



    
    /// <summary>
    /// 行动轴动画移动
    /// </summary>
    /// <returns></returns>
    public IEnumerator Move()
    {
        walkAnimIsOver = false;
        //30.5 235
        //735 235
        //动画播放未完成才播放动画

        //行动轴速度判断赋值
        i = 0;
        foreach (var axis in BattleManager.Instance.allAxisOfAction)
        {
            //确认终点的坐标
            //归零说明走到了终点，下次动画执行时要将之放到起点
            if(axis.path == 0)
                axis.walkImagePath = 735f;
            else
                axis.walkImagePath = axis.path / Settings.battleDistance * 735;

            //路径满了说明上一回合该角色抵达终点，初始要将之放回起点
            if(axis.lastWalkImagePath == 735f)
            {
                AllWalkImageRb[i].position = new Vector2(30.5f,235);
                axis.lastWalkImagePath = 0f;
            }

            axis.walkSpeed = Mathf.Abs(axis.walkImagePath - axis.lastWalkImagePath) / Settings.battleWalkTime;
            axis.lastWalkImagePath = axis.walkImagePath;
            i++;
        }
        //赋予行动轴速度
        i = 0;
        foreach (var axis in BattleManager.Instance.allAxisOfAction)
        {
            AllWalkImageRb[i].velocity = new Vector2(axis.walkSpeed,0);
            i++;
        }
        yield return new WaitForSeconds(Settings.battleWalkTime);
        //速度归零
        i = 0;
        foreach (var axis in BattleManager.Instance.allAxisOfAction)
        {
            AllWalkImageRb[i].velocity = new Vector2(0,0);
            i++;
        }

        walkAnimIsOver = true;
    }

    
    
}
