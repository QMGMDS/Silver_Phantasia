using System.Collections.Generic;
using UnityEngine;
using SP.Map;

namespace SP.AStar
{
    public class AStar : Singleton<AStar>
    {
        //一张地图存储所有格子信息的容器
        private GridNoders gridNoders;
        //计算最短路径的起点格子和终点格子
        private AStarNode startNode;
        private AStarNode targetNode;
        //该地图的宽度和高度
        private int gridWidth;
        private int gridHeight;
        //该地图的左下角原点坐标
        private int originX;
        private int originY;

        //开启列表
        //存入当前选中的格子周围的八个格子
        private List<AStarNode> openNodeList;
        //关闭列表
        //存入所有被选中的点
        private HashSet<AStarNode> closeNodeList;

        //通过AStar算法是否找到路径
        private bool pathFound;




        /// <summary>
        /// 构建路径 更新Stack的每一步
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <param name="npcMovementStack"></param>
        public void BuildPath(string sceneName, Vector2Int startPos, Vector2Int endPos, Stack<MovementStep> npcMovementStack)
        {
            pathFound = false;

            if(GenerateGridNodes(sceneName, startPos, endPos))
            {
                //查找最短路径
                if (FindShortestPath())
                {
                    //构建NPC移动路径
                    UpdataPathOnMovementStepStack(sceneName,npcMovementStack);
                    
                }
            }
        }


        /// <summary>
        /// 生成网格节点信息，初始化存储所有格子的容器
        /// </summary>
        /// <param name="sceneName">场景名字</param>
        /// <param name="startPos">起点网格位置</param>
        /// <param name="endPos">终点网格位置</param>
        /// <returns></returns>
        private bool GenerateGridNodes(string sceneName, Vector2Int startPos, Vector2Int endPos)
        {
            if(GridMapManager.Instance.GetGridDimensions(sceneName,out int width,out int height,out Vector2Int gridOrigin))
            {
                //根据地图的范围信息构造格子移动节点范围数组
                gridNoders = new GridNoders(width,height);
                gridWidth = width;
                gridHeight = height;
                originX = gridOrigin.x;
                originY = gridOrigin.y;

                openNodeList = new List<AStarNode>();
                closeNodeList = new HashSet<AStarNode>();
            }
            else
                return false;
            //gridNodes的范围是从0，0开始所以要减去原点坐标得到实际位置
            startNode = gridNoders.GetGridNode(startPos.x - originX, startPos.y - originY);
            targetNode = gridNoders.GetGridNode(endPos.x - originX, endPos.y - originY);

            //遍历地图中的所有格子，判断每个格子AStar是否是障碍物
            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    Vector3Int tilePos = new Vector3Int(x + originX, y + originY, 0);
                    var key = tilePos.x + "x" + tilePos.y + "y" + sceneName;

                    TileDetails tile = GridMapManager.Instance.GetTileDetails(key);

                    if (tile != null)
                    {
                        AStarNode node = gridNoders.GetGridNode(x, y);

                        if (tile.isNPCObstacle)
                            node.isObstacle = true;
                    }
                }
            }

            return true;
        }


        /// <summary>
        /// 找到最短路径，所有被选择过的AStarNode格子添加到CloseNodeList中
        /// </summary>
        /// <returns></returns>
        private bool FindShortestPath()
        {
            //添加起点
            openNodeList.Add(startNode);

            while(openNodeList.Count > 0)
            {
                //节点排序，Node内含比较函数
                openNodeList.Sort();
                
                AStarNode closeNode = openNodeList[0];

                openNodeList.RemoveAt(0);
                closeNodeList.Add(closeNode);

                if(closeNode == targetNode)
                {
                    pathFound = true;
                    break;
                }

                //计算周围8个格子补充到OpenList中
                EvaluateNeighbourNodes(closeNode);
            }

            return pathFound;
        }


        /// <summary>
        /// 评估周围八个格子并生成对应的FCost
        /// </summary>
        /// <param name="currentNode">被选中的格子</param>
        private void EvaluateNeighbourNodes(AStarNode currentNode)
        {
            Vector2Int currentNodePos = currentNode.gridPosition;
            AStarNode validNeighbourNode;

            for(int x = -1; x <= 1; x++)
            {
                for(int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;
                    
                    //判断currentNode格子周围的格子是否为障碍或者超出边界
                    validNeighbourNode = GetValidNeighbourNode(currentNodePos.x + x, currentNodePos.y + y);

                    if(validNeighbourNode != null)
                    {
                        if (!openNodeList.Contains(validNeighbourNode))
                        {
                            validNeighbourNode.gCost = currentNode.gCost + GetDistance(currentNode,validNeighbourNode);
                            validNeighbourNode.hCost = GetDistance(targetNode,validNeighbourNode);
                            //链接父节点
                            validNeighbourNode.parentNode = currentNode;
                            openNodeList.Add(validNeighbourNode);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 判断该格子是否合法，非障碍，非已选择
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private AStarNode GetValidNeighbourNode(int x, int y)
        {
            //格子超出地图范围的排除
            if (x >= gridWidth || y >= gridHeight || x < 0 || y < 0)
                return null;
            
            AStarNode neighbourNode = gridNoders.GetGridNode(x,y);

            //格子是障碍物或者在关闭列表中存在的排除
            if(neighbourNode.isObstacle || closeNodeList.Contains(neighbourNode))
                return null;
            else
                return neighbourNode;
        }


        /// <summary>
        /// 返回两格子之间的距离值
        /// </summary>
        /// <param name="nodeA"></param>
        /// <param name="nodeB"></param>
        /// <returns></returns>
        private int GetDistance(AStarNode nodeA,AStarNode nodeB)
        {
            int xDistance = Mathf.Abs(nodeA.gridPosition.x-nodeB.gridPosition.x);
            int yDistance = Mathf.Abs(nodeA.gridPosition.y-nodeB.gridPosition.y);

            if (xDistance > yDistance)
            {
                return 14 * yDistance + 10 * xDistance;
            }
            return 14 * xDistance +10 * yDistance;
        }

        
        /// <summary>
        /// 更新NPC路径每一步的坐标和场景名字
        /// </summary>
        /// <param name="sceneNmae"></param>
        /// <param name="npcMovementStack"></param>
        private void UpdataPathOnMovementStepStack(string sceneNmae, Stack<MovementStep> npcMovementStep)
        {
            //从目标格子开始
            AStarNode nextNode = targetNode;

            while (nextNode != null)
            {
                //生成一个MovementStep并将nextNode的数据传入
                MovementStep newStep = new MovementStep();
                newStep.sceneName = sceneNmae;
                newStep.gridCoordinate = new Vector2Int(nextNode.gridPosition.x + originX,nextNode.gridPosition.y + originY);
                //压入堆栈，将走过的路径反过来压入栈中
                npcMovementStep.Push(newStep);
                //通过父对象更新下一步
                nextNode = nextNode.parentNode;
            }
        }


    }
}
