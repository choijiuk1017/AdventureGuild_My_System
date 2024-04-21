using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Utility.Algorithm
{
    public class PathFinding : MonoBehaviour
    {
        [Header("Path Finding")]
        public GameObject target;

        [SerializeField]

        // 맵을 격자로 분할
        CustomGrid grid;

        // 남은거리를 넣을 큐
        public Queue<Vector2> wayQueue = new Queue<Vector2>();

        [Header("Player Ctrl")]

        [SerializeField]
        // 뭔가와 상호작용 하고 있을때는 walkable이 false 가 됨.
        public static bool walkable = true;

        public float moveSpeed;

        // 장애물이나 NPC 판단시 멈추게 할 범위
        public float range;

        public bool isWalking;

        private void Awake()
        {
            this.grid = GameObject.Find("Grid").GetComponent<CustomGrid>();
            walkable = true;
        }
        private void Start()
        {
            this.isWalking = false;
            this.moveSpeed = 15f;
            this.range = 4f;
        }


       
        public void StartFindPath(Vector2 startPos, Vector2 targetPos)
        {
            this.StopAllCoroutines();
            this.StartCoroutine(FindPath(startPos, targetPos));
        }

        // 길찾기 로직
        public IEnumerator FindPath(Vector2 startPos, Vector2 targetPos)
        {
            Debug.Log("길찾기 시작, " + startPos + targetPos);

            // start, target의 좌표를 grid로 분할한 좌표로 지정
            Node startNode = grid.NodeFromWorldPoint(startPos);
            Node targetNode = grid.NodeFromWorldPoint(targetPos);

            // target에 도착했는지 확인하는 변수
            bool pathSuccess = false;


            if (!startNode.walkable)
            {
                Debug.Log("Unwalkable StartNode 입니다.");
            }

            // walkable한 targetNode인 경우 길찾기 시작
            if (targetNode.walkable)
            {
                // openSet, closedSet 생성
                // closedSet = 이미 계산 고려한 노드
                // openSet = 계산할 가치가 있는 노드
                List<Node> openSet = new List<Node>();
                HashSet<Node> closedSet = new HashSet<Node>();

                openSet.Add(startNode);

                while (openSet.Count > 0)
                {
                    //현재 노드를 계산 후 openSet에서 뺌
                    Node currentNode = openSet[0];

                    for (int i = 1; i < openSet.Count; i++)
                    {
                        if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                            currentNode = openSet[i];
                    }

                    openSet.Remove(currentNode);
                    closedSet.Add(currentNode);

                    // 현재 노드가 목적지 인 경우
                    if (currentNode == targetNode)
                    {
                        //도착못했다면
                        if (pathSuccess == false)
                        {
                            // wayQueue에 PATH를 넣어줌
                            PushWay(RetracePath(startNode, targetNode));
                        }
                        pathSuccess = true;

                        Debug.Log("길찾기 성공 여부" + pathSuccess);
                        break;
                    }

                    // current의 상하좌우 노드들에 대하여 g,h cost를 고려
                    foreach (Node neighbour in grid.GetNeighbours(currentNode))
                    {
                        if (!neighbour.walkable || closedSet.Contains(neighbour)) continue;

                        // F cost 생성
                        int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);

                        if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                        {
                            neighbour.gCost = newMovementCostToNeighbour;
                            neighbour.hCost = GetDistance(neighbour, targetNode);
                            neighbour.parent = currentNode;

                            // openSet에 추가.
                            if (!openSet.Contains(neighbour)) openSet.Add(neighbour);
                        }
                    }
                }
            }

            yield return null;

            // 길을 찾았을 경우
            if (pathSuccess == true)
            {
                Debug.Log("길찾기 성공 여부" + pathSuccess);
                this.isWalking = true;

                // wayQueue를 따라 이동
                while (this.wayQueue.Count > 0)
                {
                    var dir = this.wayQueue.Peek() - (Vector2)this.transform.position;
                    this.gameObject.GetComponent<Rigidbody2D>().velocity = dir.normalized * moveSpeed;
                    if (Vector2.Distance(this.transform.position, this.wayQueue.Peek()) < 0.1f)  // 일정 거리 내에 도달했는지 확인
                    {
                        Debug.Log("Dequeue");
                        this.wayQueue.Dequeue();
                    }
                    yield return null;  
                }

                this.isWalking = false;
            }
        }


        void PushWay(Vector2[] array)
        {
            this.wayQueue.Clear();
            foreach (Vector2 item in array) this.wayQueue.Enqueue(item);
        }

        Vector2[] RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;
            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }
            path.Reverse();
            // Grid의 path에 찾은 길을 등록
            this.grid.path = path;
            Vector2[] wayPoints = SimplifyPath(path);
            return wayPoints;
        }

        Vector2[] SimplifyPath(List<Node> path)
        {
            List<Vector2> wayPoints = new List<Vector2>();

            for (int i = 0; i < path.Count; i++)
            {
                wayPoints.Add(path[i].worldPosition);
            }
            return wayPoints.ToArray();
        }

        int GetDistance(Node nodeA, Node nodeB)
        {
            int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
            int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
            // 대각선 - 14, 상하좌우 - 10.
            if (dstX > dstY) return 14 * dstY + 10 * (dstX - dstY);
            return 14 * dstX + 10 * (dstY - dstX);
        }



    }
}

