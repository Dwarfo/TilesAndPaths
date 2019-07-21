using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewTile", menuName = "Tiles/NewTile", order = 1)]
public class TileSO : ScriptableObject
{
    public string tileName;
    public int terrainDifficulty;
    public bool impassible;
    public TileTypes tileType;
    public Sprite tileImage;
}

public enum TileTypes
{
    Empty = 0,
    Mountain = 1,
    Grass = 2
}
