// 一条行动轴
using UnityEngine;

public class AxisOfAction
{
    // 该条行动的ID（1、2、3分别对应敌人，0代表玩家）
    public int AxisID;
    // 行动轴上显示图片
    public Sprite axisSprite;
    // 回合制中当前角色走过的路程
    public float path;
    // 存储角色当前速度
    public float walkSpeed;

    // 换算成UI中走的距离
    public float walkImagePath;
    // 换算成UI中上一次走的距离
    public float lastWalkImagePath;
    // 换算成UI中走的速度
    public float walkImageSpeed;

    

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="player"></param>
    public AxisOfAction(PlayerBattleARB player)
    {
        AxisID = 0;
        path = 0;
        walkImagePath = 0;
        walkImageSpeed = 0;
        axisSprite = player.axisSprite;
        walkSpeed = player.baseSpeed;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="enemy"></param>
    public AxisOfAction(EnemyBattleARB enemy)
    {
        AxisID = enemy.enemyStandID;
        path = 0;
        walkImagePath = 0;
        walkImageSpeed = 0;
        axisSprite = enemy.axisSprite;
        walkSpeed = enemy.baseSpeed;
    }

    /// <summary>
    /// 记录上一次的值 walkImageSpeed
    /// </summary>
    public void Record_lastWalkImagePath()
    {
        lastWalkImagePath = walkImagePath;
    }


}
