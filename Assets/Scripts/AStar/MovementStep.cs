using UnityEngine;


namespace SP.AStar
{
    //NPC在地图中走的每一步
    public class MovementStep
    {
        //所在的地图名字
        public string sceneName;

        //这一步对应瓦片地图的坐标
        public Vector2Int gridCoordinate;
    }
}
