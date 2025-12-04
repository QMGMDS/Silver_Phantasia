using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

//该特性使得代码在编辑模式时运行
[ExecuteInEditMode] 
public class GridMap : MonoBehaviour
{
    public MapData_SO mapData;
    public GridType gridType;

    private Tilemap currentTilemap;

    //代码被激活时运行
    private void OnEnable()
    {
        // Application.IsPlaying(this)返回布尔值
        // 该布尔值表示当前是否在游戏运行播放模式（三角箭头是否被按下）
        // 如果不为播放模式才运行这段代码
        if (!Application.IsPlaying(this)) 
        {
            currentTilemap = GetComponent<Tilemap>();

            //方便随时更改瓦片地图的数据
            if(mapData != null)
                mapData.tileProperties.Clear();
        }
    }

    //代码被关闭时运行
    private void OnDisable()
    {
        if (!Application.IsPlaying(this))
        {
            currentTilemap = GetComponent<Tilemap>();

            UpdateTileProperties();

#if UNITY_EDITOR  // 只在Unity编辑器环境下编译和执行代码块中的内容
            if(mapData != null)
                EditorUtility.SetDirty(mapData);  // SetDirty()让mapData的数据可以实时保存修改
#endif
        }
    }


    private void UpdateTileProperties()
    {
        //压缩当前瓦片地图信息，仅保留已经绘制的信息
        currentTilemap.CompressBounds();
        if (!Application.IsPlaying(this))
        {
            if(mapData != null)
            {
                //已经绘制范围左下角坐标
                Vector3Int startPos = currentTilemap.cellBounds.min;
                //已经绘制范围右上角坐标
                Vector3Int endPos = currentTilemap.cellBounds.max;

                for (int x = startPos.x; x < endPos.x; x++)
                {
                    for (int y = startPos.y; y < endPos.y; y++)
                    {
                        //拿到瓦片地图中一块一块的瓦片
                        TileBase tile = currentTilemap.GetTile(new Vector3Int(x,y,0));

                        if(tile != null)
                        {
                            TileProperty newTile = new TileProperty
                            {
                                tileCoordinate = new Vector2Int(x,y),
                                gridType = this.gridType,
                                boolTypeValue = true,
                            };

                            mapData.tileProperties.Add(newTile);
                        }
                    }
                }
            }
        }
    }
}
