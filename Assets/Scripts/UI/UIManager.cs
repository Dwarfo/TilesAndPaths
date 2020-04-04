using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton_MB<UIManager>
{
    public LineDrawer lineDrawer;
    public Button saveData;
    public Button loadData;

    void Start()
    {
        if (lineDrawer == null)
            lineDrawer = GetComponentInChildren<LineDrawer>();
        saveData.onClick.AddListener(SaveOnClick);
        loadData.onClick.AddListener(LoadOnClick);
    }

    public void SaveOnClick() 
    {
        MapData md = TerrainEditor.Instance.GetEditedMapData();
        GameManager.Instance.SaveMap(md);
    }

    public void LoadOnClick() 
    {
        GameManager.Instance.LoadMap();
    }

    
}
