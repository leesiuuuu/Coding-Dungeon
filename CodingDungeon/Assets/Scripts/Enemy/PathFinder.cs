using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [Header("Path Settings")]
    [SerializeField] private bool showDebugPath = true;
    [SerializeField] private bool showObstacles = true;
    
    [Header("Mover Module")]
    [SerializeField] private GridMovement gridMovement;
    
    private Queue<Vector3Int> _pathQueue = new();
    private Vector3Int _currentCell;
    private List<Vector3Int> _lastPath = new();
    private HashSet<Vector3Int> _checkedTiles = new();

    private void Start()
    {
        if (gridMovement == null)
            gridMovement = GetComponent<GridMovement>();

        _currentCell = MapManager.Instance.WorldToCell(transform.position);
        MapManager.Instance.OccupyTile(_currentCell, gameObject);
    }

    public void Move(Vector3Int position)
    {
        FindPath(position);
        if (_pathQueue.Count > 0)
        {
            MoveTowards();
        }
    }
    
    public void FindPath(Vector3Int targetCell)
    {
        Vector3Int startCell = MapManager.Instance.WorldToCell(transform.position);
        Vector3Int originalTarget = targetCell;
        
        _lastPath.Clear();
        _checkedTiles.Clear();
        
        if (!MapManager.Instance.IsTileWalkable(targetCell))
        {
            //Debug.Log($"목표 타일 {targetCell} 막혀있음 - 주변 타일 탐색 중...");
            targetCell = FindNearestWalkableTile(targetCell);
            
            if (targetCell == Vector3Int.zero)
            {
                //Debug.Log("주변에 갈 수 있는 타일이 없습니다 - 강제 이동합니다!");
                targetCell = originalTarget;
            }
            else
            {
                //Debug.Log($"우회 경로 발견: {targetCell}");
            }
        }
        
        List<Vector3Int> path = BFS(startCell, targetCell);
        
        _pathQueue.Clear();
        
        if (path != null && path.Count > 0)
        {
            _lastPath = new List<Vector3Int>(path);
            foreach (var cell in path)
            {
                _pathQueue.Enqueue(cell);
            }
//            Debug.Log($"경로 찾기 성공! 경로 길이: {path.Count}");
        }
        else
        {

            Debug.Log("경로를 찾을 수 없어 직선으로 강제 이동합니다!");
            List<Vector3Int> forcedPath = CreateStraightPath(startCell, targetCell);
            _lastPath = new List<Vector3Int>(forcedPath);
            
            foreach (var cell in forcedPath)
            {
                _pathQueue.Enqueue(cell);
            }
        }
    }
    
    private Vector3Int FindNearestWalkableTile(Vector3Int blockedCell)
    {
        Queue<Vector3Int> searchQueue = new Queue<Vector3Int>();
        HashSet<Vector3Int> searched = new HashSet<Vector3Int>();
        
        searchQueue.Enqueue(blockedCell);
        searched.Add(blockedCell);
        
        Vector3Int[] directions = new Vector3Int[]
        {
            new Vector3Int(0, 1, 0),
            new Vector3Int(0, -1, 0),
            new Vector3Int(-1, 0, 0),
            new Vector3Int(1, 0, 0),
            new Vector3Int(1, 1, 0),
            new Vector3Int(-1, 1, 0),
            new Vector3Int(1, -1, 0),
            new Vector3Int(-1, -1, 0)
        };
        
        int maxSearchDepth = 10;
        int currentDepth = 0;
        
        while (searchQueue.Count > 0 && currentDepth < maxSearchDepth)
        {
            int levelSize = searchQueue.Count;
            
            for (int i = 0; i < levelSize; i++)
            {
                Vector3Int current = searchQueue.Dequeue();
                
                if (MapManager.Instance.IsTileWalkable(current))
                {
                    return current;
                }
                
                foreach (var dir in directions)
                {
                    Vector3Int neighbor = current + dir;
                    
                    if (!searched.Contains(neighbor))
                    {
                        searchQueue.Enqueue(neighbor);
                        searched.Add(neighbor);
                    }
                }
            }
            
            currentDepth++;
        }
        
        return Vector3Int.zero;
    }
    private List<Vector3Int> CreateStraightPath(Vector3Int start, Vector3Int target)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        
        Vector3Int current = start;
        
        while (current != target)
        {
            int dx = target.x - current.x;
            int dy = target.y - current.y;
            
            Vector3Int step = Vector3Int.zero;
            
            if (dx != 0)
            {
                step.x = dx > 0 ? 1 : -1;
            }
            
            if (dy != 0)
            {
                step.y = dy > 0 ? 1 : -1;
            }
            
            current += step;
            path.Add(current);
        }
        
        return path;
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
            new Vector3Int(1, 0, 0),
            new Vector3Int(1, 1, 0),
            new Vector3Int(-1, 1, 0),
            new Vector3Int(1, -1, 0),
            new Vector3Int(-1, -1, 0)
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
                
                if (visited.Contains(neighbor))
                    continue;
                
                bool isWalkable = MapManager.Instance.IsTileWalkable(neighbor);
                _checkedTiles.Add(neighbor);
                
                if (!isWalkable)
                {
                    //Debug.Log($"타일 {neighbor} 막혀있음");
                    continue;
                }
                
                queue.Enqueue(neighbor);
                visited.Add(neighbor);
                cameFrom[neighbor] = current;
            }
        }
        
        if (!foundPath)
        {
            Debug.Log($"경로 찾기 실패: {start} -> {target}, 체크한 타일 수: {_checkedTiles.Count}");
            return null;
        }
        
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
    
    public void MoveTowards()
    {
        StartCoroutine(MoveFlow());
    }

    private IEnumerator MoveFlow()
    {
        Vector3Int nextCell = _pathQueue.Dequeue();
        Vector3 targetPos = MapManager.Instance.GetCellCenterWorld(nextCell);
        var dir = targetPos - transform.position;
        gridMovement.Move(dir);
        yield return new WaitForSeconds(0.2f);
        transform.position = targetPos;

        MapManager.Instance.MoveOccupant(_currentCell, nextCell, gameObject);
        _currentCell = nextCell;
    }

    private void OnDestroy()
    {
        if (MapManager.Instance != null)
        {
            MapManager.Instance.FreeTile(_currentCell);
        }
    }
    
    /*private void OnDrawGizmos()
    {
        if (!Application.isPlaying || MapManager.Instance == null)
            return;
        Gizmos.color = Color.blue;
        Vector3 currentPos = MapManager.Instance.GetCellCenterWorld(_currentCell);
        Gizmos.DrawWireCube(currentPos, Vector3.one * 0.8f);
        
        if (showDebugPath && _lastPath.Count > 0)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < _lastPath.Count; i++)
            {
                Vector3 pos = MapManager.Instance.GetCellCenterWorld(_lastPath[i]);
                Gizmos.DrawWireCube(pos, Vector3.one * 0.6f);
                
                if (i > 0)
                {
                    Vector3 prevPos = MapManager.Instance.GetCellCenterWorld(_lastPath[i - 1]);
                    Gizmos.DrawLine(prevPos, pos);
                }
            }
        }
        
        // 체크한 타일 중 막힌 타일 표시 (빨간색)
        if (showObstacles && _checkedTiles.Count > 0)
        {
            Gizmos.color = Color.red;
            foreach (var tile in _checkedTiles)
            {
                if (!MapManager.Instance.IsTileWalkable(tile))
                {
                    Vector3 pos = MapManager.Instance.GetCellCenterWorld(tile);
                    Gizmos.DrawCube(pos, Vector3.one * 0.4f);
                }
            }
        }
        
        Gizmos.color = Color.yellow;
        Vector3Int[] directions = new Vector3Int[]
        {
            new Vector3Int(0, 1, 0),
            new Vector3Int(0, -1, 0),
            new Vector3Int(-1, 0, 0),
            new Vector3Int(1, 0, 0),
            new Vector3Int(1, 1, 0),
            new Vector3Int(-1, 1, 0),
            new Vector3Int(1, -1, 0),
            new Vector3Int(-1, -1, 0)
        };
        
        foreach (var dir in directions)
        {
            Vector3Int neighbor = _currentCell + dir;
            bool isWalkable = MapManager.Instance.IsTileWalkable(neighbor);
            
            if (!isWalkable)
            {
                Vector3 pos = MapManager.Instance.GetCellCenterWorld(neighbor);
                Gizmos.color = new Color(1f, 0.5f, 0f, 0.5f); 
                Gizmos.DrawCube(pos, Vector3.one * 0.5f);
            }
        }
    }*/
}