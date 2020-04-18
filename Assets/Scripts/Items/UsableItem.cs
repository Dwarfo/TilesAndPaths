using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableItem : MonoBehaviour, IUsable, IPickable
{
    public SO_UsableItem item;
    [SerializeField]
    private Vector2Int testPositionIndex;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite           = item.pickableSprite;
        GetComponent<SpriteRenderer>().sortingLayerName = "Items";
        //ToRemove
        //placeInTile(testPositionIndex);
    }
    public void placeInTile(Vector2Int positionIndex) 
    {
        Tile t = GameManager.Instance.fieldScript.GetTileByIndex(positionIndex);
        t.SetPickable(this);
        gameObject.transform.position = new Vector3Int(positionIndex.x, positionIndex.y, 0);
    }

    public void Use(InventoryController ic)
    {
        Debug.Log(ic.pc.entityName + " used healing potion");
    }

    public void PickUp(InventoryController ic) 
    {
        Debug.Log(ic.pc.entityName + " picked up healing potion");
    }
}
