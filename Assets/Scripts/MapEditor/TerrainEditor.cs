using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainEditor : MonoBehaviour
{
    private Vector2Int currentMousePositionIndex;
    private Vector2Int lastDrawnIndex;
    private TileTypes[,] tileTerrain;
    private int minx;
    private int miny;
    private Rect mousePosRectangle;
    private float rectangleOffset;

    void Start()
    {
        lastDrawnIndex = new Vector2Int(0, 0);
        rectangleOffset = TileField.Instance.tileSize / 2;
    }

    // Update is called once per frame
    void Update()
    {
        currentMousePositionIndex = TileField.Instance.IndexOfPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (lastDrawnIndex != currentMousePositionIndex) 
        {
            lastDrawnIndex = currentMousePositionIndex;
            UIManager.Instance.lineDrawer.DrawRectangle(lastDrawnIndex, rectangleOffset, rectangleOffset);
        }

    }

    private Rect DrawTileBounds(Vector2Int index) {
        Rect newRectangle = new Rect();
        newRectangle.center = index.ToVec2();
        newRectangle.width = TileField.Instance.xSize;
        newRectangle.height = TileField.Instance.ySize;

        return newRectangle;
    }
}
