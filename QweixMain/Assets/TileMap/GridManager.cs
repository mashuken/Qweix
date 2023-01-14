using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Grid))]
[RequireComponent(typeof(Tilemap))]

public class GridManager : MonoBehaviour
{
    Tilemap tilemap;
    Grid grid;
    [SerializeField] TileBase tileBase;
    [SerializeField] TileBase tileBase2;


    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        grid = GetComponent<Grid>();
        grid.Init(2, 2);
        grid.Set(1, 1, true);
        UpdateTileMap();
    }

    void UpdateTileMap()
    {
        for(int x = 0; x < grid.length; x++)
        {
            for (int y =0; y < grid.height; y++)
            {
                if(grid.Get(x, y) == true)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tileBase);
                }
                else
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tileBase2);
                }
                
            }
        }
    }
}
