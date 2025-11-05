using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathFinder : MonoBehaviour
{
    [Header("Tilemap Settings")]
    [SerializeField] private Tilemap walkableTilemap;
    [SerializeField] private Tilemap obstacleTilemap;
    
    [Header("Path Settings")]
    [SerializeField] private bool showDebugPath = true;
    
    [Header("Mover Module")]
    [SerializeField] private GridMovement gridMovement;
    
    private Queue<Vector3Int> _pathQueue = new();
    
    //디버그용 코드
    /*private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int targetCell = walkableTilemap.WorldToCell(mousePos);
            
            FindPath(targetCell);
        }
        
        if (!isMoving && pathQueue.Count > 0)
        {
            StartCoroutine(FollowPath());
        }
    }*/
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int targetCell = walkableTilemap.WorldToCell(mousePos);
            
            FindPath(targetCell);
            if (_pathQueue.Count > 0)
            {
                MoveTowards();
            }
        }
        
    }
    
    public void FindPath(Vector3Int targetCell)
    {
        Vector3Int startCell = walkableTilemap.WorldToCell(transform.position);
        
        if (!IsTileWalkable(targetCell))
        {
            Debug.Log("목표 타일에 갈 수 없습니다!");
            return;
        }
        
        List<Vector3Int> path = BFS(startCell, targetCell);
        
        if (path != null && path.Count > 0)
        {
            _pathQueue.Clear();
            foreach (var cell in path)
            {
                _pathQueue.Enqueue(cell);
            }
            Debug.Log($"경로 찾기 성공! 총 {path.Count}개의 타일");
        }
        else
        {
            Debug.Log("경로를 찾을 수 없습니다!");
        }
    }
    
    private List<Vector3Int> BFS(Vector3Int start, Vector3Int target)
    {
        Queue<Vector3Int> queue = new Queue<Vector3Int>();
        Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();
        HashSet<Vector3Int> visited = new HashSet<Vector3Int>();
        
        queue.Enqueue(start);
        visited.Add(start);
        cameFrom[start] = start;
        
        Vector3Int[] directions = new Vector3Int[]
        {
            new Vector3Int(0, 1, 0),   
            new Vector3Int(0, -1, 0), 
            new Vector3Int(-1, 0, 0),  
            new Vector3Int(1, 0, 0)   
        };
        
        bool foundPath = false;
        
        while (queue.Count > 0)
        {
            Vector3Int current = queue.Dequeue();
            
            if (current == target)
            {
                foundPath = true;
                break;
            }
            
            foreach (var dir in directions)
            {
                Vector3Int neighbor = current + dir;
                
                if (visited.Contains(neighbor) || !IsTileWalkable(neighbor))
                    continue;
                
                queue.Enqueue(neighbor);
                visited.Add(neighbor);
                cameFrom[neighbor] = current;
            }
        }
        
        if (!foundPath)
            return null;
        
        return ReconstructPath(cameFrom, start, target);
    }
    
    private List<Vector3Int> ReconstructPath(Dictionary<Vector3Int, Vector3Int> cameFrom, Vector3Int start, Vector3Int target)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        Vector3Int current = target;
        
        while (current != start)
        {
            path.Add(current);
            current = cameFrom[current];
        }
        
        path.Reverse();
        return path;
    }
    
    private bool IsTileWalkable(Vector3Int cellPos)
    {
        if (!walkableTilemap.HasTile(cellPos))
            return false;
        
        if (obstacleTilemap != null && obstacleTilemap.HasTile(cellPos))
            return false;
        
        return true;
    }
    
    public void MoveTowards()
    {
        Vector3Int nextCell = _pathQueue.Dequeue();
        Vector3 targetPos = walkableTilemap.GetCellCenterWorld(nextCell); 
        var dir=targetPos-transform.position;
        Debug.Log(dir);
        gridMovement.Move(dir);
        transform.position = targetPos;
    }
    
    private void OnDrawGizmos()
    {
        if (!showDebugPath || _pathQueue == null || walkableTilemap == null)
            return;
        
        Gizmos.color = Color.yellow;
        foreach (var cell in _pathQueue)
        {
            Vector3 worldPos = walkableTilemap.GetCellCenterWorld(cell);
            Gizmos.DrawWireCube(worldPos, Vector3.one * 0.8f);
        }
    }
}