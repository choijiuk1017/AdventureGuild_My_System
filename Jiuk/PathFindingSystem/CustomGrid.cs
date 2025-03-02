using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrid : MonoBehaviour
{
    public bool displayGridGizmos;

    public GameObject[] main;

    // 장애물 레이어
    public LayerMask OBSTACLE;

    // 화면의 크기
    public Vector2 gridWorldSize;


    // 반지름
    public float nodeRadius;
    Node[,] grid;

    // 격자의 지름
    float nodeDiameter;
    // x,y축 사이즈
    int gridSizeX, gridSizeY;

    // A*에서 사용할 PATH.
    [SerializeField]
    public List<Node> path;

    private void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        main = GameObject.FindGameObjectsWithTag("Adventure");
        // 격자 생성
        CreateGrid();
    }

    

    // Scene view 출력용 기즈모.
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(gridWorldSize.x, gridWorldSize.y));
        if (grid != null)
        {
            foreach (GameObject adventure in main)
            {
                Node mainNode = NodeFromWorldPoint(adventure.transform.position);
                foreach (Node n in grid)
                {
                    Gizmos.color = (n.walkable) ? new Color(1, 1, 1, 0.3f) : new Color(1, 0, 0, 0.3f);
                    if (n.walkable == false)

                        if (path != null)
                        {
                            if (path.Contains(n))
                            {
                                Gizmos.color = new Color(0, 0, 0, 0.3f);
                                Debug.Log("?");
                            }
                        }
                    if (mainNode == n) Gizmos.color = new Color(0, 1, 1, 0.3f);
                    Gizmos.DrawCube(n.worldPosition, Vector2.one * (nodeDiameter - 0.1f));
                }
            }
        }
    }

    // 격자 생성 함수
    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];

        Vector2 worldBottomLeft = (Vector2)transform.position - Vector2.right * gridWorldSize.x / 2 - Vector2.up * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);
                // 해당 격자가 Walkable한지 아닌지 판단.
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, OBSTACLE));
                // 노드 할당.
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    // node 상하 좌우 대각 노드를 반환하는 함수.
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    if (!grid[node.gridX, checkY].walkable && !grid[checkX, node.gridY].walkable) continue;
                    if (!grid[node.gridX, checkY].walkable || !grid[checkX, node.gridY].walkable) continue;

                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }


    // 입력으로 들어온 월드좌표를 node좌표계로 변환.
    public Node NodeFromWorldPoint(Vector2 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }
}
