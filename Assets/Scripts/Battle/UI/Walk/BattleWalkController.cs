using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//在每个回合的开始之前，由BattleManager计算当前是谁的回合后，BattleWalkController拿取每一个角色的path
//实现角色行动轴动画

public class BattleWalkController : MonoBehaviour
{
    //行动图片
    public List<Image> Image;
    //图片的坐标
    public List<Rigidbody2D> Rb;

    //便于迭代
    private int i;
    
    /// <summary>
    /// 行动轴动画移动
    /// </summary>
    /// <returns></returns>
    public IEnumerator Move()
    {
        //46 236
        //724 236
        //动画播放未完成才播放动画

        //行动轴速度判断赋值
        i = 0;
        foreach (var character in BattleManager.Instance.battleList)
        {
            //确认终点的坐标
            //归零说明走到了终点，下次动画执行时要将之放到起点
            if(character.path == 0)
                character.walkPath = 678f;
            else
                character.walkPath = character.path / Settings.battleDistance * 678;

            //路径满了说明上一回合该角色抵达终点，初始要将之放回起点
            if(character.lastWalkPath == 678f)
            {
                Rb[i].position = new Vector2(46,236);
                character.lastWalkPath = 0f;
            }

            character.walkSpeed = Mathf.Abs(character.walkPath - character.lastWalkPath) / Settings.battleWalkTime;
            character.lastWalkPath = character.walkPath;
            i++;
        }
        //赋予行动轴速度
        i = 0;
        foreach (var character in BattleManager.Instance.battleList)
        {
            Rb[i].velocity = new Vector2(character.walkSpeed,0);
            i++;
        }
        yield return new WaitForSeconds(Settings.battleWalkTime);
        //速度归零
        i = 0;
        foreach (var character in BattleManager.Instance.battleList)
        {
            Rb[i].velocity = new Vector2(0,0);
            i++;
        }
        //动画播放完毕
        BattleManager.Instance.walking = false;
    }

    /// <summary>
    /// 战斗彻底结束时，行动轴动画的复原
    /// </summary>
    public void Recovery()
    {
        for (int i = 0; i < Rb.Count; i++)
        {
            Rb[i].position = new Vector2(46,236);
        }
    }

}
