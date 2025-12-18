using System.Collections.Generic;
using UnityEngine;


//存储每张地图信息
[CreateAssetMenu(fileName = "MapData_SO", menuName = "Map/MapData")]
public class MapData_SO : ScriptableObject 
{
    //地图名字
    public string sceneName;
    [Header("地图信息")]
    //地图的宽度
    public int gridWidth;
    //地图的长度
    public int gridHeight;
    [Header("地图左下角原点坐标")]
    public int originX;
    public int originY;
    
    public List<TileProperty> tileProperties;
}
