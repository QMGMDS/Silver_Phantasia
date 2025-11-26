using UnityEngine;


namespace SP.AStar
{
    //存储地图中格子的容器的属性
    public class GridNoders : MonoBehaviour
    {
        //该地图的宽和高
        private int width;
        private int height;
        //存储地图格子的容器
        private AStarNode[,] gridNode;

        /// <summary>
        /// 构造函数 传入地图的宽高
        /// </summary>
        /// <param name="width">地图宽度</param>
        /// <param name="height">地图高度</param>
        public GridNoders(int width, int height)
        {
            this.width = width;
            this.height = height;

            //根据地图宽高生成指定大小的格子容器
            gridNode = new AStarNode[width,height];

            //容器中填入格子
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    gridNode[x,y] = new AStarNode(new Vector2Int(x,y));
                }
            }
        }

        /// <summary>
        /// 根据网格坐标返回格子的属性
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        /// <returns></returns>
        public AStarNode GetGridNode(int xPos,int yPos)
        {
            //判断给定坐标是否合法（在不在地图范围之内）
            if (xPos < width && yPos < height)
            {
                return gridNode[xPos,yPos];
            }
            Debug.Log("输入的格子坐标超出地图范围");
            return null;
        }
    }
}

