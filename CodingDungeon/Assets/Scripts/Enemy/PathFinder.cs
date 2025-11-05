using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [Header("Path Settings")]
    [SerializeField] private bool showDebugPath = true;
    
    [Header("Mover Module")]
    [SerializeField] private GridMovement gridMovement;
    
    private Queue<Vector3Int> _pathQueue = new();
    private Vector3Int _currentCell;

    private void Start()
    {
        if (gridMovement == null)
            gridMovement = GetComponent<GridMovement>();

        _currentCell = MapManager.Instance.WorldToCell(transform.position);
        MapManager.Instance.OccupyTile(_currentCell, gameObject);
    }

    /*private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int targetCell = MapManager.Instance.WorldToCell(mousePos);
            
            FindPath(targetCell);
            if (_pathQueue.Count > 0)
            {
                MoveTowards();
            }
        }
    }*/
    public void Move(Vector3Int position)
    {
        //Vector3Int targetCell = MapManager.Instance.WorldToCell(position);
            
        FindPath(position);
        if (_pathQueue.Count > 0)
        {
            MoveTowards();
        }
    }
    
    public void FindPath(Vector3Int targetCell)
    {
        Vector3Int startCell = MapManager.Instance.WorldToCell(transform.position);
        
        if (!MapManager.Instance.IsTileWalkable(targetCell))
        {
            //Debug.Log("목표 타일 막혀있음 - 주변 타일 탐색 중...");
            targetCell = FindNearestWalkableTile(targetCell);
            
            if (targetCell == Vector3Int.zero)
            {
                Debug.Log("갈 수 있는 타일이 없습니다!");
                _pathQueue.Clear();
                return;
            }
            
            //Debug.Log($"우회 경로 발견: {targetCell}");
        }
        
        List<Vector3Int> path = BFS(startCell, targetCell);
        
        _pathQueue.Clear();
        
        if (path != null && path.Count > 0)
        {
            foreach (var cell in path)
            {
                _pathQueue.Enqueue(cell);
            }
        }
        else
        {
            Debug.Log("경로를 찾을 수 없습니다!");
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
                
                if (visited.Contains(neighbor) || !MapManager.Instance.IsTileWalkable(neighbor))
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
}