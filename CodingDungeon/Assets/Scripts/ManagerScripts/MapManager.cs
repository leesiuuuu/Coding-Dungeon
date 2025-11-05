using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : SceneSingleMono<MapManager>
{
    [Header("Tilemap Settings")]
    [SerializeField] private Tilemap walkableTilemap;
    [SerializeField] private Tilemap obstacleTilemap;

    private HashSet<Vector3Int> _walkableTiles = new();
    private HashSet<Vector3Int> _obstacleTiles = new();
    private Dictionary<Vector3Int, GameObject> _occupiedTiles = new();

    protected override void Awake()
    {
        base.Awake();
        InitializeTilemaps();
        CacheTiles();
    }

    private void InitializeTilemaps()
    {
        if (walkableTilemap == null || obstacleTilemap == null)
        {
            Tilemap[] tilemaps = FindObjectsOfType<Tilemap>();

            foreach (var tilemap in tilemaps)
            {
                string name = tilemap.gameObject.name.ToLower();

                if (walkableTilemap == null &&
                    (name.Contains("walkable") || name.Contains("ground") || name.Contains("floor")))
                {
                    walkableTilemap = tilemap;
                }

                if (obstacleTilemap == null &&
                    (name.Contains("obstacle") || name.Contains("wall") || name.Contains("block")))
                {
                    obstacleTilemap = tilemap;
                }
            }

            if (walkableTilemap == null && tilemaps.Length > 0)
                walkableTilemap = tilemaps[0];

            if (obstacleTilemap == null && tilemaps.Length > 1)
                obstacleTilemap = tilemaps[1];
        }
    }

    private void CacheTiles()
    {
        _walkableTiles.Clear();
        _obstacleTiles.Clear();

        if (walkableTilemap != null)
        {
            BoundsInt bounds = walkableTilemap.cellBounds;
            foreach (var pos in bounds.allPositionsWithin)
            {
                if (walkableTilemap.HasTile(pos))
                {
                    _walkableTiles.Add(pos);
                }
            }
        }

        if (obstacleTilemap != null)
        {
            BoundsInt bounds = obstacleTilemap.cellBounds;
            foreach (var pos in bounds.allPositionsWithin)
            {
                if (obstacleTilemap.HasTile(pos))
                {
                    _obstacleTiles.Add(pos);
                }
            }
        }

        Debug.Log($"Walkable Tiles: {_walkableTiles.Count}, Obstacle Tiles: {_obstacleTiles.Count}");
    }

    public bool IsTileWalkable(Vector3Int cellPos)
    {
        if (!_walkableTiles.Contains(cellPos))
            return false;

        if (_obstacleTiles.Contains(cellPos))
            return false;

        if (_occupiedTiles.ContainsKey(cellPos))
            return false;

        return true;
    }

    public bool OccupyTile(Vector3Int cellPos, GameObject occupant)
    {
        if (!IsTileWalkable(cellPos))
            return false;

        _occupiedTiles[cellPos] = occupant;
        return true;
    }

    public void FreeTile(Vector3Int cellPos)
    {
        _occupiedTiles.Remove(cellPos);
    }

    public void MoveOccupant(Vector3Int from, Vector3Int to, GameObject occupant)
    {
        FreeTile(from);
        OccupyTile(to, occupant);
    }

    public Vector3Int WorldToCell(Vector3 worldPos)
    {
        return walkableTilemap.WorldToCell(worldPos);
    }

    public Vector3 GetCellCenterWorld(Vector3Int cellPos)
    {
        return walkableTilemap.GetCellCenterWorld(cellPos);
    }

    public Tilemap GetWalkableTilemap() => walkableTilemap;
    public Tilemap GetObstacleTilemap() => obstacleTilemap;
}