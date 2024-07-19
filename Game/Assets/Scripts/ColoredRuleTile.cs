using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Colored Rule Tile", menuName = "Tiles/Colored Rule Tile")]
public class ColoredRuleTile : RuleTile<RuleTile.TilingRule.Neighbor>
{

    public int pixelsPerTile = 32;


    public List<Texture2D> allTextures;

    private Sprite newSprite;



    private List<Sprite> GetSpritesFromTexture(Texture2D tex) {
        List<Sprite> sprites = new();
        float w = tex.width / pixelsPerTile;
        float h = tex.height / pixelsPerTile;

        for(int i = 0; i < w; i++) {
            for(int j = 0; j < h; j++) {
                Sprite newSprite = Sprite.Create(
                    tex,
                    new(pixelsPerTile * i, pixelsPerTile * j, pixelsPerTile, pixelsPerTile),
                    new(0.5f, 0.5f),
                    pixelsPerTile
                );
                sprites.Add(newSprite);
            }
        }

        return sprites;
    }

    public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData) {
        base.GetTileData(location, tileMap, ref tileData);

        List<Sprite> defaultSprites = GetSpritesFromTexture(allTextures[0]);

        int savedIndex = 0;

        for(int i = 0; i < defaultSprites.Count(); i++) {
            if(tileData.sprite.textureRect == defaultSprites[i].textureRect) {
                savedIndex = i;
            }
        }

        if(Level.instance != null) {
            List<Sprite> coloredSprites = GetSpritesFromTexture(allTextures[Level.instance.colorPreset]);

            newSprite = coloredSprites[savedIndex];
            
            tileData.sprite = newSprite;
        }
        
    }
}
