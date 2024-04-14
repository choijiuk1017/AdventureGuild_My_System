using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestingAdventure : MonoBehaviour
{
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
    

    public Vector2 destination; // 목적지

    public int moveSpeed = 5; // 이동 속도

    private bool isMoving = false; // 이동 중인지 여부


    Rigidbody2D rigid;

    private List<int> remainingBuildingIndices;

    // Start is called before the first frame update
    void Start()
    {

        rigid = GetComponent<Rigidbody2D>();
        remainingBuildingIndices = new List<int> { 0, 1, 2, 3, 4, 5 };
        if (currentHp < maxHp | currentMp < maxMp)
        {
            destination = FindBuilding("Temple").transform.position;
        }

        if (willingness < maxWillingness)
        {
            destination = FindBuilding("Circus").transform.position;
        }
        StartCoroutine(DecreaseWillpowerRoutine());
        

    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(Thinking());
        }

        if (isInBuilding == true)
        {
            // 건물에 있으면 이동을 멈춘다.
            destination = transform.position;

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



    private IEnumerator Thinking()
    {

        if (!isInBuilding)
        {
            if (currentHp < maxHp | currentMp < maxMp)
            {
                destination = FindBuilding("Temple").transform.position;
            }

            if (willingness < maxWillingness)
            {
                destination = FindBuilding("Circus").transform.position;
            }

            int numRemainingBuildings = remainingBuildingIndices.Count;

            if (numRemainingBuildings > 0)
            {
                int randomIndex = Random.Range(0, numRemainingBuildings);
                int selectedBuildingIndex = remainingBuildingIndices[randomIndex];

                switch (selectedBuildingIndex)
                {
                    case 0:
                        destination = FindBuilding("Smithy").transform.position;
                        break;
                    case 1:
                        destination = FindBuilding("Inn").transform.position;
                        break;
                    case 2:
                        destination = FindBuilding("PotionMarket").transform.position;
                        break;
                    case 3:
                        destination = FindBuilding("TrainingCenter").transform.position;
                        break;
                    case 4:
                        destination = FindBuilding("Library").transform.position;
                        break;
                    case 5:
                        destination = FindBuilding("Central Square").transform.position;
                        break;
                    default:
                        Debug.LogError("Invalid random building index!");
                        break;
                }

                remainingBuildingIndices.RemoveAt(randomIndex);
            }
            else
            {
                remainingBuildingIndices = new List<int> { 0, 1, 2, 3, 4, 5 };
            }
        }

        while (Vector2.Distance(transform.position, destination) > 0.1f)
        {
            MoveToBuilidng(destination);
            yield return null;
        }

        isMoving = false;
    }

    public void MoveToBuilidng(Vector2 destination)
    {
        if (destination != null)
        {
            Vector2 direction = (destination - (Vector2)transform.position).normalized;

            rigid.MovePosition(rigid.position + direction * moveSpeed * Time.deltaTime);
        }
        else
        {
            Debug.Log("Destination is null");
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
            isInBuilding = true;
        }
    }
}
