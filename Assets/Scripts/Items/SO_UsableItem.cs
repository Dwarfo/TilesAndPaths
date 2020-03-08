using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Usable", menuName = "Items/Usable", order = 1)]
public class SO_UsableItem : ScriptableObject
{
    public Sprite pickableSprite;
    public Sprite inventorySprite;
    public int quantity;

}
