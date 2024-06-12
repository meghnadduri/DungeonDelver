using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    static public Tile[] DELVER_TILES;
    static public Dictionary<char, Tile> COLL_TILE_DICT;

    [Header("Inscribed")]
    public Tilemap visualMap;
    public Tilemap collisionMap;

    private TileBase[] visualTileBaseArray;
    private TileBase[] collTileBaseArray;
    void Awake()
    {
        LoadTiles();
    }

    void LoadTiles()
    {
        int num;

        Tile[] tempTiles = Resources.LoadAll<Tile>("Tiles_Visual");

        DELVER_TILES = new Tile[tempTiles.Length];
        for (int i = 0; i < tempTiles.Length; i++)
        {
            string[] bits = tempTiles[i].name.Split('_');
            if (int.TryParse(bits[1], out num))
            {
                DELVER_TILES[num] = tempTiles[i];
            }
            else
            {
                Debug.LogError("Failed to parse num of: " + tempTiles[i].name);
            }
        }
        Debug.Log("Parsed " + DELVER_TILES.Length + " tiles into TILES_VISUAL.");

    }
    // Start is called before the first frame update
    void Start()
    {
        ShowTiles();
    }

    void ShowTiles()
    {
        visualTileBaseArray = GetMapTiles();
        visualMap.SetTilesBlock(MapInfo.GET_MAP_BOUNDS(), visualTileBaseArray);
    }

    public TileBase[] GetMapTiles()
    {
        int tileNum;
        Tile tile;
        TileBase[] mapTiles = new TileBase[MapInfo.W * MapInfo.H];
        for (int y = 0; y < MapInfo.H; y++)
        {
            for (int x = 0; x < MapInfo.W; x++)
            {
                tileNum = MapInfo.MAP[x, y];
                tile = DELVER_TILES[tileNum];
                mapTiles[y * MapInfo.W + x] = tile;
            }
        }
        return mapTiles;
    }
}
