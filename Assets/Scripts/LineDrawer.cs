using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public LineRenderer lr;

    void Start()
    {
        if (lr == null)
            lr = GetComponentInChildren<LineRenderer>();
    }

    public void DrawLine(Path path)
    {
        if (path == null)
            return;

        lr.startWidth = 0.2f;
        lr.endWidth = 0.2f;
        lr.positionCount = path.fullPath.Count - path.currentPos;
        lr.startColor = Color.cyan;
        lr.endColor = Color.cyan;
        Tile[] vec = path.fullPath.ToArray();
        List<Vector3> v2 = new List<Vector3>();
        for(int i = path.currentPos; i < vec.Length; i++)
            v2.Add((Vector2)vec[i].Index);

        lr.SetPositions(v2.ToArray());

        foreach (var pp in path.fullPath)
        {
            Debug.Log(pp);
        }

    }

    public void ClearLine()
    {
        lr.positionCount = 0;
    }
}