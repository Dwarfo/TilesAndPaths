using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton_MB<GameManager>
{
    public float playerSpeed;
    public PlayerController pc;
    public InventoryController ic;
    public UsableItem testPot;


    void Start()
    {
        TileField.Instance.Process();

        testPot.placeInTile(new Vector2Int(2,1));
    }

    public void PlayerReady(PlayerController pc) 
    {
        PathEvent pathChanged   = pc.getPathChangedEvent();
        PathEvent pathEnded     = pc.getPathEndedEvent();

        ic.SubscribeToPathEnded(pathEnded);
        pathChanged.AddListener(HandleChangedPath);
    }

    private void HandleChangedPath(Path path) 
    {
        UIManager.Instance.lineDrawer.DrawLine(path);
    }
}
