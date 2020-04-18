using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Setting", menuName = "Settings/CustomSettings", order = 1)]
public class SO_GameSettings : ScriptableObject
{
    public float xSize;
    public float ySize;
    public float pixelOffset;
    public float tileSize;
    public GameObject tilePrefab;
    public SO_Tile[] allTileTypes;
}
