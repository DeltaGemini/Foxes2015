using UnityEngine;
using System.Collections;

public enum TileType { Grass, Empty, Ground }

public class Tile : MonoBehaviour
{
    public TileType type;

    public int x { get; set; }
    public int y { get; set; }
    public bool isNavigable
    {
        get;
        set;
    }

    private Grid grid;

    public void Initialize(Grid grid)
    {
        this.grid = grid;
        x = (int)transform.position.x;
        y = (int)transform.position.y;
    }

    void Start()
    {
        switch(type)
        {
            case TileType.Empty:

                var bottomTile = grid[x, y - 1];
                if(bottomTile != null && bottomTile.type == TileType.Grass)
                {
                    isNavigable = true;
                }
                break;
        }
    }

}
