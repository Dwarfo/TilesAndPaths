using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameManager : Singleton_MB<GameManager>
{
    public float playerSpeed;
    public PlayerController pc;
    public InventoryController ic;
    public UsableItem testPot;

    private TerrainEditor editor;

    void Start()
    {
        TileField.Instance.Process();
        editor = TerrainEditor.Instance;
        //testPot.placeInTile(new Vector2Int(2,1));
    }

    public void PlayerReady(PlayerController pc) 
    {
        PathEvent pathChanged   = pc.getPathChangedEvent();
        PathEvent pathEnded     = pc.getPathEndedEvent();

        ic.SubscribeToPathEnded(pathEnded);
        pathChanged.AddListener(HandleChangedPath);
    }

    public void SaveMap(MapData mapdata) 
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/map.mp";
        FileStream stream = new FileStream(path, FileMode.Create);

        bf.Serialize(stream, mapdata);
        stream.Close();
    }

    public void LoadMap() 
    {
        foreach (Tile tile in editor.duplicatesToDestroy)
        {
            Destroy(tile.gameObject);
        }
        TileField.Instance.ClearMap();
        string path = Application.persistentDataPath + "/map.mp";
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            MapData mapData = bf.Deserialize(stream) as MapData;
            Debug.Log("Mapdata size: " + mapData.tiles.Count);
            TileField.Instance.DrawTilesFromMap(mapData);
            stream.Close();
            TileField.Instance.SetNeighbours();
        }
        else
        {
            Debug.Log("No file found");
        }
    }

    private void HandleChangedPath(Path path) 
    {
        UIManager.Instance.lineDrawer.DrawLine(path);
    }
}
