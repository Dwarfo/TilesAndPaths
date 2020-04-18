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
    public TileField fieldScript;
    public SO_GameSettings gameSettings;

    void Start()
    {
        fieldScript = fieldScript == null ? gameObject.AddComponent<TileField>() : fieldScript;
        fieldScript.SetSettings(gameSettings);
        fieldScript.Process();

        //testPot.placeInTile(new Vector2Int(2,1));
    }

    public void PlayerReady(PlayerController pc) 
    {
        PathEvent pathChanged   = pc.getPathChangedEvent();
        PathEvent pathEnded     = pc.getPathEndedEvent();

        pc.SetSettings(gameSettings);
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
        fieldScript.ClearMap();
        string path = Application.persistentDataPath + "/map.mp";
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            MapData mapData = bf.Deserialize(stream) as MapData;
            Debug.Log("Mapdata size: " + mapData.tiles.Count);
            fieldScript.DrawTilesFromMap(mapData);
            stream.Close();
            fieldScript.SetNeighbours();
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
