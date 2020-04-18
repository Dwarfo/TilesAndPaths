using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : Singleton_MB<EditorManager>
{
    public TerrainEditor terrainEditor;
    public TileChooser tileChooser;
    public SO_GameSettings gameSettings;

    void Start()
    {
        if (terrainEditor == null)
            terrainEditor = gameObject.AddComponent<TerrainEditor>();

        terrainEditor.SetSettings(gameSettings);
        foreach (SO_Tile tileInfo in gameSettings.allTileTypes)
        {
            TileToChoose ttcScript = tileChooser.AddTileToChoose(tileInfo);
            ttcScript.tileChosenEvent.AddListener(HandleTileChosen);
        }
    }

    public void HandleTileChosen(SO_Tile tileInfo) 
    {
        terrainEditor.SetCurrentTileType(tileInfo.tileType);
    }
}
