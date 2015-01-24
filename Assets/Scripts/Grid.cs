using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
    public List<Tile> tiles;

    public int rows { get; set; }
    public int columns { get; set; }

    public Tile this[int xIndex, int yIndex]
    {
        get
        {
            var tile = tiles[xIndex + yIndex * 10];
            if (tile.x == xIndex && tile.y == yIndex)
                return tile;
            else
                return null;
        }
    }

    void Awake()
    {
        Tile[] childrenTiles = transform.GetComponentsInChildren<Tile>();
        tiles = new List<Tile>(childrenTiles.Length);

        int maxRows = 0;
        int maxColumns = 0;
        foreach (Tile tile in childrenTiles)
        {
            tile.Initialize(this);

            int index = tile.x + tile.y * 10;
            tile.gameObject.name += index.ToString();


            if (index <= tiles.Count)
            {
                int stop = tiles.Count - 1;
                while (tiles[stop].x + tiles[stop].y * 10 > index)
                {
                    stop--;
                    if (stop < 0)
                        break;
                }
                tiles.Insert(stop + 1, tile);
            }
            else
                tiles.Add(tile);

            if (tile.x > maxRows)
                maxRows = tile.y;
            if (tile.y > maxColumns)
                maxColumns = tile.x;
        }

        rows = maxRows + 1;
        columns = maxColumns + 1;
    }
}
