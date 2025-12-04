using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SP.Map
{
    public class GridMapManager : Singleton<GridMapManager>
    {

        [Header("地图信息列表")]
        public List<MapData_SO> mapDataList;

        private Dictionary<string, TileDetails> tileDetailsDict = new Dictionary<string, TileDetails>();


        private void Start()
        {

            foreach (var mapData in mapDataList)
            {
                InitTileDetailsDict(mapData);
            }
        }

        /// <summary>
        /// 根据地图场景名字构建地图宽度高度和左下角原点
        /// </summary>
        /// <param name="sceneName">地图名字</param>
        /// <param name="width">地图宽度</param>
        /// <param name="height">地图高度</param>
        /// <param name="gridOrigin">地图原点</param>
        /// <returns>是否有这个地图的信息</returns>
        public bool GetGridDimensions(string sceneName, out int width, out int height, out Vector2Int gridOrigin)
        {
            width = 0;
            height = 0;
            gridOrigin = Vector2Int.zero;

            foreach (var mapData in mapDataList)
            {
                if(mapData.sceneName == sceneName)
                {
                    width = mapData.gridWidth;
                    height = mapData.gridHeight;

                    gridOrigin.x = mapData.originX;
                    gridOrigin.y = mapData.originY;

                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// 初始化字典tileDetailsDict
        /// </summary>
        /// <param name="mapData"></param>
        private void InitTileDetailsDict(MapData_SO mapData)
        {
            foreach (TileProperty tileProperty in mapData.tileProperties)
            {
                //拿取地图信息mapData中的瓦片属性信息列表，构成key
                TileDetails tileDetails = new TileDetails
                {
                    gridX = tileProperty.tileCoordinate.x,
                    gridY = tileProperty.tileCoordinate.y,
                };
                string key = tileDetails.gridX + "x" + tileDetails.gridY + "y" + mapData.sceneName;

                
                if (GetTileDetails(key) != null)
                {
                    tileDetails = GetTileDetails(key);
                }
                
                // tileDetails同步MapData地图信息
                switch (tileProperty.gridType)
                {
                    case GridType.NPCObstacle:
                        tileDetails.isNPCObstacle = tileProperty.boolTypeValue;
                        break;
                    case GridType.NotAllowWalk:
                        tileDetails.isNotAllowWalk = tileProperty.boolTypeValue;
                        break;
                    case GridType.Transition:
                        tileDetails.isTransition = tileProperty.boolTypeValue;
                        break;
                }

                //如果该key在字典tileDetailsDict中已经存在
                //将tileDetails作为新元素覆盖字典key上原先的元素，更新原字典
                if (GetTileDetails(key) != null)
                {
                    tileDetailsDict[key] = tileDetails;
                }
                //如果该key在字典tileDetailsDict中不存在
                //添加新元素
                else
                {
                    tileDetailsDict.Add(key, tileDetails);
                }
            }
        }



        /// <summary>
        /// 根据key在字典tileDetailsDict中查找，返回对应的瓦片详情TileDetails
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TileDetails GetTileDetails(string key)
        {
            if (tileDetailsDict.ContainsKey(key))
            {
                return tileDetailsDict[key];
            }
            else return null;
        }

        


    }
}
