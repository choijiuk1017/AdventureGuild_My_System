using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Algorithm;

public class TestingAdventure : MonoBehaviour
{
    //이 코드들은 플레이어의 수치를 조절할 
    public int willingness = 100;
    public int maxWillingness = 100;

    public int currentHp;
    public int maxHp = 3000;

    public int currentMp;
    public int maxMp = 3000;

    public int money;

    public int equipmentValue; //테스트용
    public int equipmentDurability; //테스트용
    public int maxEquipmentDurability;
  

    public bool isCompleteRequest;

    public bool isInBuilding;

    public bool isSetDestination  = false;

    public GameObject destination; // 목적지

    [SerializeField]
    private bool isMoving = false; // 이동 중인지 여부


    private Vector2 currentDestination;


    Rigidbody2D rigid;

    private List<int> remainingBuildingIndices = new List<int> { 0, 1, 2, 3, 4, 5 };


    PathFinding pathFinding;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        pathFinding = GetComponent<PathFinding>();

        destination = this.gameObject;

        StartCoroutine(DecreaseWillpowerRoutine());

        
    }

    void Update()
    {
        if (!isInBuilding && !isSetDestination)
        {
            UpdateDestination();
        }
        if (!isInBuilding && destination != null && !isMoving)
        {
            StartMovingToDestination();
        }
    }

    void StartMovingToDestination()
    {
        currentDestination = destination.transform.position;

        isMoving=true;

        pathFinding.StartFindPath(this.transform.position, currentDestination);
    }

    void FixedUpdate()
    {
        //FixedUpdate 내에서는 Rigidbody2D를 사용해 물리 기반 이동을 관리
        if (isMoving && pathFinding.wayQueue.Count > 0)
        {
            var dir = pathFinding.wayQueue.Peek() - (Vector2)this.transform.position;
            rigid.velocity = dir.normalized * pathFinding.moveSpeed * Time.fixedDeltaTime;
            if (Vector2.Distance(this.transform.position, pathFinding.wayQueue.Peek()) < 0.1f)
            {
                pathFinding.wayQueue.Dequeue();
            }
        }
    }

    private IEnumerator DecreaseWillpowerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(30f); 
            willingness -= 10; // 의지 감소
        }

        
    }

    void UpdateDestination()
    {
        isSetDestination = true;
        if (currentHp < maxHp || currentMp < maxMp)
        {
            // 체력이나 마력이 최대가 아닐 경우, 템플을 목적지로 설정
            destination = FindBuilding("Temple");
        }
        else if (willingness < maxWillingness)
        {
            // 의지력이 최대가 아닐 경우, 서커스를 목적지로 설정
            destination = FindBuilding("Circus");
        }
        else
        {
            SelectRandomDestination();
        }
    }

    void SelectRandomDestination()
    {
        if(remainingBuildingIndices.Count == 0)
        {
            remainingBuildingIndices = new List<int> { 0, 1, 2, 3, 4, 5 };
        }

        int randomIndex = Random.Range(0, remainingBuildingIndices.Count);
        int selectedBuildingIndex = remainingBuildingIndices[randomIndex];
        remainingBuildingIndices.RemoveAt(randomIndex);

        switch (selectedBuildingIndex)
        {
            case 0:
                destination = FindBuilding("Smithy");
                break;
            case 1:
                destination = FindBuilding("Inn");
                break;
            case 2:
                destination = FindBuilding("PotionMarket");
                break;
            case 3:
                destination = FindBuilding("TrainingCenter");
                break;
            case 4:
                destination = FindBuilding("Library");
                break;
            case 5:
                destination = FindBuilding("Central Square");
                break;
            default:
                Debug.LogError("Invalid random building index!");
                break;
        }
    }

    public GameObject FindBuilding(string buildingName)
    {
        GameObject findBuilding = GameObject.Find(buildingName);


        if(findBuilding == null)
        {
            Debug.LogError("건물 없음");
        }

        return findBuilding;

    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Building"))
        {
            // 플레이어가 건물에 들어갔다면 'isInBuilding'을 true로 설정
            isInBuilding = true;

            pathFinding.StopAllCoroutines();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Building"))
        {
            isInBuilding = false;
            // 건물을 나오면 목적지를 재설정하고 이동 시작
            UpdateDestination();
            StartMovingToDestination();
        }
    }
}
