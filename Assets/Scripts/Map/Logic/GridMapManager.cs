using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SP.Map
{
    public class GridMapManager:Singleton<GridMapManager>
    {

        [Header("地图信息列表")]
        public List<MapData_SO> mapDataList;

        





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




    }
}
