using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton_MB<UIManager>
{
    public LineDrawer lineDrawer;

    void Start()
    {
        if (lineDrawer == null)
            lineDrawer = GetComponentInChildren<LineDrawer>();


    }

    void Update()
    {
        
    }
}
