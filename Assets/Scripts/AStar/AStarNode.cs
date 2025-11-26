using System;
using UnityEngine;


namespace SP.AStar
{
    //每个格子的属性信息
    //IComparer接口实现了AStarNode的比较方法
    public class AStarNode : IComparable<AStarNode>
    {
        //格子对象的坐标（一个格子的位置）
        public Vector2Int gridPosition;

        //离起点的距离
        public float gCost = 0;
        //离终点的距离
        public float hCost = 0;
        //当前格子的值
        //这里创建了FCost的属性，当Fcost被调用时自动将gCost+hCost的返回
        public float FCost => gCost + hCost;
        //当前格子是否是障碍
        public bool isObstacle = false;
        //父对象
        public AStarNode parentNode;

        /// <summary>
        /// 构造函数 传入该格子对应的坐标
        /// </summary>
        /// <param name="pos"></param>
        public AStarNode(Vector2Int pos)
        {
            gridPosition = pos;
            parentNode = null;
        }

        /// <summary>
        /// 比较AStarNode中Cost大小
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(AStarNode other)
        {
            //与other对方格子进行比较，小的在前
            //大于返回1，当前对象应该排在另一个对象之后
            //相等返回0
            //小于返回-1，当前对象应该排在另一个对象之前
            int result = FCost.CompareTo(other.FCost);
            //FCost相等则进而比较gCost
            if(result == 0)
            {
                result = hCost.CompareTo(other.gCost);
            }
            return result;
        }
    }  
}
