using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileToChoose : MonoBehaviour
{
    public Button tileButton;
    public Text tileDescr;

    public TileInfoEvent tileChosenEvent = new TileInfoEvent();

    private SO_Tile tileInfo;

    void Start()
    {
        if (tileButton == null)
        {
            tileButton = gameObject.GetComponent<Button>();
            tileDescr = gameObject.GetComponentInChildren<Text>();
        }
        tileButton.onClick.AddListener(ChooseTile);
    }

    public void ChooseTile() 
    {
        tileChosenEvent.Invoke(tileInfo);
    }

    public void SetTileInfo(SO_Tile tile)
    {
        tileInfo = tile;
        tileDescr.text = "Name: " + tile.tileName + "\n" + "Difficulty: " + tile.terrainDifficulty;
        tileButton.image.sprite = tile.tileImage;
    }
}