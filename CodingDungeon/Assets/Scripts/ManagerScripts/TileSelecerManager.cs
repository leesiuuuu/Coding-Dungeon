using System;
using UnityEngine;

public class TileSelecerManager : SceneSingleMono<TileSelecerManager>
{
    public TileSelector tileSelector;

    private void Start()
    {
        tileSelector.gameObject.SetActive(false);
    }

    public void OnSetTile()
    {
        tileSelector.gameObject.SetActive(true);
    }
}
