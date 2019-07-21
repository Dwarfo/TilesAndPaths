using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float pixelsOffset;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("In floats: " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
            //Debug.Log("In ints: " + IndexOfMouse(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
            var p = TileField.Instance.GetPath(new Vector2Int(0, 1), IndexOfMouse(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
            foreach (var pp in p.fullPath)
            {
                Debug.Log(pp);
            }
        }

        if (Input.GetMouseButtonDown(1))
            Debug.Log("Pressed secondary button.");

        if (Input.GetMouseButtonDown(2))
            Debug.Log("Pressed middle click.");
    }

    private Vector2Int IndexOfMouse(Vector3 mp)
    {
        return new Vector2Int(Mathf.FloorToInt(mp.x + pixelsOffset), Mathf.FloorToInt(mp.y + pixelsOffset));
    }
}
