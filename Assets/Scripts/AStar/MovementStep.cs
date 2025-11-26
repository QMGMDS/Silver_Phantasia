using UnityEngine;


namespace SP.AStar
{
    //NPC在地图中走的每一步
    public class MovementStep
    {
        public string sceneName;

        public int hour;

        public int minute;

        public int second;

        //这一步对应瓦片地图的坐标
        public Vector2Int gridCoordinate;
    }
}
