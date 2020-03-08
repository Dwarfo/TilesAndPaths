using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public PlayerController pc;
    public List<UsableItem> usableItems = new List<UsableItem>();

    private void Start()
    {

    }

    public void PickUpItem(Path path)
    {
        Tile t = path.GetCurrentTile();
        IPickable item = t.GetPickable();

        if (item != null)
            item.PickUp(this);
    }

    public void SubscribeToPathEnded(PathEvent pathEnded) 
    {
        pathEnded.AddListener(PickUpItem);
    }
}
