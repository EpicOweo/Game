using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Colored Tile", menuName = "Tiles/Colored Tile")]
public class ColoredTile : Tile
{
    public List<Sprite> allSprites;
    private Sprite newSprite;
    
    public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData)
    {
        base.GetTileData(location, tileMap, ref tileData);

        newSprite = allSprites[Level.instance.colorPreset];
        
        tileData.sprite = newSprite;
    }
}
