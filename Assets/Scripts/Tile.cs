using UnityEngine;
using System.Collections;

public enum TileType { Grass, Empty}

public class Tile : MonoBehaviour
{
    public TileType type;

    public int x { get; set; }
    public int y { get; set; }

    void Awake()
    {
        x = (int)transform.position.x;
        y = (int)transform.position.y;
    }

    void Update()
    {

    }
}
