using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;


namespace SP.AStar
{
    public class AStarTest : MonoBehaviour
    {
        private AStar aStar;

        [Header("用于测试")]
        //瓦片地图中的起始坐标
        public Vector2Int startPos;
        public Vector2Int finishPos;

        //瓦片地图
        public Tilemap displayMap;
        //要绘制的瓦片
        public TileBase displayTile;

        //是否在瓦片地图中显示起始格子
        public bool displayStartAndFinish;
        //是否在瓦片地图中显示NPC格子路径
        public bool displayPath;

        private Stack<MovementStep> npcMovementStepStack;


        private void Awake()
        {
            aStar = GetComponent<AStar>();
            npcMovementStepStack = new Stack<MovementStep>();
        }

        private void Update()
        {
            ShowPathOnGridMap();
        }


        /// <summary>
        /// 可视化AStar算法路径
        /// </summary>
        private void ShowPathOnGridMap()
        {
            if (displayMap != null && displayTile != null)
            {
                if (displayStartAndFinish)
                {
                    displayMap.SetTile((Vector3Int)startPos,displayTile);
                    displayMap.SetTile((Vector3Int)finishPos,displayTile);
                }
                else
                {
                    displayMap.SetTile((Vector3Int)startPos,null);
                    displayMap.SetTile((Vector3Int)finishPos,null);
                }

                if (displayPath)
                {
                    var sceneName = SceneManager.GetActiveScene().name;
                    aStar.BuildPath(sceneName,startPos,finishPos,npcMovementStepStack);

                    foreach (var step in npcMovementStepStack)
                    {
                        displayMap.SetTile((Vector3Int)step.gridCoordinate,displayTile);
                    }

                }
                else
                {
                    if(npcMovementStepStack.Count > 0)
                    {
                        foreach (var step in npcMovementStepStack)
                        {
                            displayMap.SetTile((Vector3Int)step.gridCoordinate,null);
                        }
                        npcMovementStepStack.Clear();
                    }
                }
            }
        }

    }
}

