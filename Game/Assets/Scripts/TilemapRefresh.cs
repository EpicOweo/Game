using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapRefresh : MonoBehaviour
{
    private Tilemap tilemap;
 
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        RefreshMap();
    }
 
    public void RefreshMap()
    {
        if (tilemap != null)
        {
            tilemap.RefreshAllTiles();
        }
    }
}