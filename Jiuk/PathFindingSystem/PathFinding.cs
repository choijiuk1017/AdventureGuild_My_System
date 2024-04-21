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

        // ���� ���ڷ� ����
        CustomGrid grid;

        // �����Ÿ��� ���� ť
        public Queue<Vector2> wayQueue = new Queue<Vector2>();

        [Header("Player Ctrl")]

        [SerializeField]
        // ������ ��ȣ�ۿ� �ϰ� �������� walkable�� false �� ��.
        public static bool walkable = true;

        public float moveSpeed;

        // ��ֹ��̳� NPC �Ǵܽ� ���߰� �� ����
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

        // ��ã�� ����
        public IEnumerator FindPath(Vector2 startPos, Vector2 targetPos)
        {
            Debug.Log("��ã�� ����, " + startPos + targetPos);

            // start, target�� ��ǥ�� grid�� ������ ��ǥ�� ����
            Node startNode = grid.NodeFromWorldPoint(startPos);
            Node targetNode = grid.NodeFromWorldPoint(targetPos);

            // target�� �����ߴ��� Ȯ���ϴ� ����
            bool pathSuccess = false;


            if (!startNode.walkable)
            {
                Debug.Log("Unwalkable StartNode �Դϴ�.");
            }

            // walkable�� targetNode�� ��� ��ã�� ����
            if (targetNode.walkable)
            {
                // openSet, closedSet ����
                // closedSet = �̹� ��� ����� ���
                // openSet = ����� ��ġ�� �ִ� ���
                List<Node> openSet = new List<Node>();
                HashSet<Node> closedSet = new HashSet<Node>();

                openSet.Add(startNode);

                while (openSet.Count > 0)
                {
                    //���� ��带 ��� �� openSet���� ��
                    Node currentNode = openSet[0];

                    for (int i = 1; i < openSet.Count; i++)
                    {
                        if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                            currentNode = openSet[i];
                    }

                    openSet.Remove(currentNode);
                    closedSet.Add(currentNode);

                    // ���� ��尡 ������ �� ���
                    if (currentNode == targetNode)
                    {
                        //�������ߴٸ�
                        if (pathSuccess == false)
                        {
                            // wayQueue�� PATH�� �־���
                            PushWay(RetracePath(startNode, targetNode));
                        }
                        pathSuccess = true;

                        Debug.Log("��ã�� ���� ����" + pathSuccess);
                        break;
                    }

                    // current�� �����¿� ���鿡 ���Ͽ� g,h cost�� ���
                    foreach (Node neighbour in grid.GetNeighbours(currentNode))
                    {
                        if (!neighbour.walkable || closedSet.Contains(neighbour)) continue;

                        // F cost ����
                        int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);

                        if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                        {
                            neighbour.gCost = newMovementCostToNeighbour;
                            neighbour.hCost = GetDistance(neighbour, targetNode);
                            neighbour.parent = currentNode;

                            // openSet�� �߰�.
                            if (!openSet.Contains(neighbour)) openSet.Add(neighbour);
                        }
                    }
                }
            }

            yield return null;

            // ���� ã���� ���
            if (pathSuccess == true)
            {
                Debug.Log("��ã�� ���� ����" + pathSuccess);
                this.isWalking = true;

                // wayQueue�� ���� �̵�
                while (this.wayQueue.Count > 0)
                {
                    var dir = this.wayQueue.Peek() - (Vector2)this.transform.position;
                    this.gameObject.GetComponent<Rigidbody2D>().velocity = dir.normalized * moveSpeed;
                    if (Vector2.Distance(this.transform.position, this.wayQueue.Peek()) < 0.1f)  // ���� �Ÿ� ���� �����ߴ��� Ȯ��
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
            // Grid�� path�� ã�� ���� ���
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
            // �밢�� - 14, �����¿� - 10.
            if (dstX > dstY) return 14 * dstY + 10 * (dstX - dstY);
            return 14 * dstX + 10 * (dstY - dstX);
        }



    }
}

