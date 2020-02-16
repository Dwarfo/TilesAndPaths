using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton_MB<GameManager>
{
    public float playerSpeed;
    public PlayerController pc;

    void Start()
    {

    }

    public void PlayerReady(PlayerController pc) 
    {
        PathEvent ev = pc.getPathEvent();
        ev.AddListener(HandleChangedPath);
    }

    private void HandleChangedPath(Path path) 
    {
        UIManager.Instance.lineDrawer.DrawLine(path);
    }
}
