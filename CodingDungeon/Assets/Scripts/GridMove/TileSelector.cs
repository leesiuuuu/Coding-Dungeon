using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class TileSelector : MonoBehaviour
{
    [SerializeField] private LayerMask tilemapLayer;
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private GameObject selectTile;

    public event Action<Vector3Int> OnTileSelected;

    private Tilemap currentTilemap;
    private Vector3Int lastSelectedCell;

    void Update()
    {
        Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D col = Physics2D.OverlapPoint(point, tilemapLayer);

        if (col != null && IsInLayerMask(col.gameObject.layer, tilemapLayer))
        {
            // MapManager 사용으로 통일
            Vector3Int cellPos = MapManager.Instance.WorldToCell(point);
            Vector3 cellCenterWorld = MapManager.Instance.GetCellCenterWorld(cellPos);
            
            selectTile.transform.position = cellCenterWorld;
            transform.position = point;

            if (Input.GetMouseButtonDown(0))
            {
                OnTileSelected?.Invoke(cellPos);
                
                // Tilemap 정보가 필요하면 다시 가져오기
                Tilemap tilemap = col.GetComponent<Tilemap>();
                if (tilemap != null && tilemap.GetTile(cellPos) != null)
                {
                    currentTilemap = tilemap;
                    lastSelectedCell = cellPos;
                }
                
                gameObject.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        selectTile.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        selectTile.gameObject.SetActive(false);
    }

    private bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }
}