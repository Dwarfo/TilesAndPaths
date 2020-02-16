using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton_MB<UIManager>
{
    public LineDrawer lineDrawer;
    void Start()
    {
        if (lineDrawer == null)
            lineDrawer = GetComponentInChildren<LineDrawer>();
    }

}
