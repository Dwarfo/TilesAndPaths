using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileChooser : MonoBehaviour
{
    public GameObject content;
    public GameObject tileToChoosePrefab;

    private int numberOfItems = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public TileToChoose AddTileToChoose(SO_Tile tileInfo) 
    {
        GameObject tileToChoose = Instantiate(tileToChoosePrefab, content.transform);
        RectTransform rt = tileToChoose.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector3(-85, 90 - 120 * numberOfItems);
        TileToChoose tileToChooseScript = tileToChoose.GetComponent<TileToChoose>();
        tileToChooseScript.SetTileInfo(tileInfo);

        numberOfItems++;

        return tileToChooseScript;
    }
}
