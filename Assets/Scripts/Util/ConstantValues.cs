using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConstantValues
{
    public static SO_ConstantValues settings;
}

[CreateAssetMenu(fileName = "New Settings", menuName = "Settings/Settings", order = 1)]
public  class SO_ConstantValues : ScriptableObject {
    public int xSize;
    public int ySize;
    public float pixelsOffset;
}