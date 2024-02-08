using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PathFinding : MonoBehaviour
{
    public static PathFinding Instance { get; private set; }
    public GameObject prefab;
    public int xValue;
    public int yValue;
    public List<ShortestPath> shortestPathList;
    public List<PointFloat> shortestPath2;

    [Serializable]
    public struct Point
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    [Serializable]
    public struct PointFloat
    {
        public float x;
        public float y;
        public PointFloat(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
    [SerializeField]
    public List<string> gridReference;
    public static int[,] grid = new int[,]
    {
        { 0, 0, 0, 0, 0 ,0 , 0},
        { 0, 0, 0, 0, 0 ,0 , 0},
        { 0, 0, 0, 0, 0 ,0 , 0},
        { 0, 0, 0, 0, 0 ,0 , 0},
        { 0, 0, 0, 0, 0 ,0 , 0},
        { 0, 0, 0, 0, 0 ,0 , 0},
        { 0, 0, 0, 0, 0 ,0 , 0},
        { 0, 0, 0, 0, 0 ,0 , 0},
        { 0, 0, 0, 0, 0 ,0 , 0},
        { 0, 0, 0, 0, 0 ,0 , 0},
    };

    public void SetValueGrid()
    {
        int rows = grid.GetLength(0);    
        int columns = grid.GetLength(1); 

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                grid[i, j] = Convert.ToInt32(new string(gridReference[i][j], 1)); 
            }
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        SetValueGrid();
        UpdatePath();
    }
    
    public static List<Point> AStarPathFinding(Point start, Point end)
    {
        Dictionary<Point, Point> cameFrom = new Dictionary<Point, Point>();
        Dictionary<Point, float> gScore = new Dictionary<Point, float>();
        Dictionary<Point, float> fScore = new Dictionary<Point, float>();

        List<Point> openSet = new List<Point> { start };
        gScore[start] = 0;
        fScore[start] = HeuristicCostEstimate(start, end);

        while (openSet.Count > 0)
        {
            Point current = GetNodeWithLowestFScore(openSet, fScore);

            if (current.Equals(end))
            {
                return ReconstructPath(cameFrom, current);
            }

            openSet.Remove(current);

            foreach (Point neighbor in GetNeighbors(current))
            {
                float tentativeGScore = gScore[current] + DistanceBetween(current, neighbor);

                if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + HeuristicCostEstimate(neighbor, end);

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }

        return null;
    }

    private static float HeuristicCostEstimate(Point a, Point b)
    {
        return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
    }

    private static Point GetNodeWithLowestFScore(List<Point> openSet, Dictionary<Point, float> fScore)
    {
        Point nodeWithLowestFScore = openSet[0];
        float lowestFScore = float.MaxValue;

        foreach (Point node in openSet)
        {
            if (fScore[node] < lowestFScore)
            {
                lowestFScore = fScore[node];
                nodeWithLowestFScore = node;
            }
        }

        return nodeWithLowestFScore;
    }

    private static List<Point> ReconstructPath(Dictionary<Point, Point> cameFrom, Point current)
    {
        List<Point> totalPath = new List<Point> { current };

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            totalPath.Insert(0, current);
        }

        return totalPath;
    }

    private static List<Point> GetNeighbors(Point node)
    {
        List<Point> neighbors = new List<Point>();

        //Di chuyen theo 4 huong len xuong trai phai
        Point[] directions = new Point[]
        {
            new Point(-1, 0), // up
            new Point(1, 0), // down
            new Point(0, -1), // left
            new Point(0, 1) // right
        };

        foreach (var direction in directions)
        {
            int newX = node.x + direction.x;
            int newY = node.y + direction.y;

            // Check if the neighbor is within the grid
            if (newX >= 0 && newX < grid.GetLength(0) && newY >= 0 && newY < grid.GetLength(1))
            {
                //Kiem tra o khong bi chan boi vat can va co the di duoc
                if (grid[newX, newY] == 0)
                {
                    neighbors.Add(new Point(newX, newY));
                }
            }
        }

        return neighbors;
    }

    private static float DistanceBetween(Point a, Point b)
    {
        // Calculate the Euclidean distance between two points
        return Mathf.Sqrt(Mathf.Pow(a.x - b.x, 2) + Mathf.Pow(a.y - b.y, 2));
    }
    public static List<Point> BfsPathFinding(Point start, Point end)
    {
        Queue<Point> queue = new Queue<Point>();
        bool[,] visited = new bool[grid.GetLength(0), grid.GetLength(1)];
        Point[,] parent = new Point[grid.GetLength(0), grid.GetLength(1)];
    
        int[] dx = { 1, -1, 0, 0 };
        int[] dy = { 0, 0, 1, -1 };
    
        queue.Enqueue(start);
        visited[start.x, start.y] = true;
    
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                parent[i, j] = new Point(10000, 10000);
            }
        }
    
        while (queue.Count > 0)
        {
            Point current = queue.Dequeue();
    
            if (current.x == end.x && current.y == end.y)
                break;
    
            for (int i = 0; i < 4; i++)
            {
                int newX = current.x + dx[i];
                int newY = current.y + dy[i];
    
                if (newX >= 0 && newX < grid.GetLength(0) && newY >= 0 && newY < grid.GetLength(1) &&
                    grid[newX, newY] == 0 && !visited[newX, newY])
                {
                    queue.Enqueue(new Point(newX, newY));
                    visited[newX, newY] = true;
                    parent[newX, newY] = current;
                }
            }
        }
    
        List<Point> shortestPath = new List<Point>();
        Point currentPoint = end;
    
        while (currentPoint.x != start.x || currentPoint.y != start.y)
        {
            shortestPath.Insert(0, currentPoint);
            if (parent[currentPoint.x, currentPoint.y].x != 10000 && parent[currentPoint.x, currentPoint.y].y != 10000)
            {
                currentPoint = parent[currentPoint.x, currentPoint.y];
            }
            else
            {
                currentPoint = start;
            }
        }
        shortestPath.Insert(0, start);
        return shortestPath;
    }
//    


    
    public void CreatePath(Point start, Point end, out List<Point> _shortestPath)
    {
        List<Point> shortestPath = BfsPathFinding(start, end);
        List<PointFloat> shortestPath3 = new List<PointFloat>();
        if (shortestPath.Count > 2)
        {
            for (int i = 1; i < shortestPath.Count - 1; i++)
            {
                Vector3 posArrow = new Vector3(shortestPath[i].x, 0.7f, shortestPath[i].y);
                Vector3 targetDirection = new Vector3(shortestPath[i + 1].x, 0.7f, shortestPath[i + 1].y) - posArrow;
                Vector3 newDirection = Vector3.RotateTowards(posArrow, targetDirection, 5, 0.0f);
                Quaternion rotation = Quaternion.Euler(0, 90, 0);
                newDirection = rotation * newDirection;
                if (!PosEqualGatePos(posArrow))
                {
                    float offset = 0;
                    for (int k = 0; k < shortestPath[i].x; k++)
                    {
                        offset += 1.4f;
                    }
                    GameObject newArrow = ObjectPool.instance.GetFromObjectPool(ObjectPool.instance.arrow, new Vector3(offset + 0.2f, 0.7f, shortestPath[i].y));
                    shortestPath3.Add(new PointFloat(offset, shortestPath[i].y));
                    newArrow.transform.rotation = Quaternion.LookRotation(newDirection);
                }
            }
            _shortestPath = shortestPath;
        }
        else
        {
            _shortestPath = null;
        }
    }

    public bool PosEqualGatePos(Vector3 pos)
    {
/*        foreach (var item in GameManager.Instance.gatePositions)
        {
            Vector3 posCheck = new Vector3(pos.x, 1, pos.z);
            if (item.localPosition == posCheck)
            {
                return true;
            }
        }*/
        return false;
    }
    public bool HasPath(Point start, Point end)
    {
        List<Point> shortestPath = BfsPathFinding(start, end);

        if (shortestPath.Count > 3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool HasAllPath(List<ShortestPath> shortestPathList)
    {
        foreach (var item in shortestPathList)
        {
            if (!HasPath(item.start, item.end))
            {
                return false;
            }
        }
        return true;
    }

    public void DestroyMarkers()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("marker");
        foreach (var obj in objs)
        {
            ObjectPool.instance.Return(obj);
        }
    }
    public void ChangeValueGrid()
    {
        if (grid[xValue, yValue] == 1)
        {
            grid[xValue, yValue] = 0;
        }
        else
        {
            grid[xValue, yValue] = 1;
        }
    }
    public void ChangeValueOne()
    {
        grid[xValue, yValue] = 1;
    }
    public void ChangeValueZero()
    {
        grid[xValue, yValue] = 0;
    }

    public void UpdateAllPath()
    {
        ChangeValueGrid();
        UpdatePath();
    }
    public void UpdatePath()
    {
        if (HasAllPath(shortestPathList))
        {
            DestroyMarkers();
            for (int i = 0; i < shortestPathList.Count; i++)
            {
                GameManager gameManager = GameManager.Instance;
                if (gameManager.waves[gameManager.waveCount - 1].gates[i])
                {
                    CreatePath(shortestPathList[i].start, shortestPathList[i].end, out shortestPathList[i].points);
                }
            }
        }
        else
        {
            // Debug.Log("null");
        }
    }
}