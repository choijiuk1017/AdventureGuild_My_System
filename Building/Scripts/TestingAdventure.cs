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

    public float moveSpeed = 5f; // 이동 속도

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
            StartCoroutine(Thinking());
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
        isMoving = true;

        if(currentHp < maxHp | currentMp < maxMp)
        {
            destination = FindBuilding("Temple").transform.position;

        }

        if(willingness < maxWillingness)
        {
            destination = FindBuilding("Circus").transform.position;
        }

        int numRemainingBuildings = remainingBuildingIndices.Count;

        if(numRemainingBuildings > 0)
        {
            int randomIndex = Random.Range(0, numRemainingBuildings);
            int selectedBuildingIndex = remainingBuildingIndices[randomIndex];
            

            switch (selectedBuildingIndex)
            {
                case 0:
                    destination = FindBuilding("Smithy").transform.position;

                    if (destination == null)
                    {
                        StartCoroutine(Thinking());
                    }
                    remainingBuildingIndices.RemoveAt(randomIndex);
                    
                    break;

                case 1:
                    destination = FindBuilding("Inn").transform.position;

                    if (destination == null)
                    {
                        StartCoroutine(Thinking());
                    }
                    else
                    {
                        remainingBuildingIndices.RemoveAt(randomIndex);
                    }
                    
                    break;

                case 2:
                    destination = FindBuilding("PotionMarket").transform.position;

                    if (destination == null)
                    {
                        StartCoroutine(Thinking());
                    }
                    else
                    {
                        remainingBuildingIndices.RemoveAt(randomIndex);
                    }

                    break;

                case 3:
                    destination = FindBuilding("TrainingCenter").transform.position;

                    if (destination == null)
                    {
                        StartCoroutine(Thinking());
                    }
                    else
                    {
                        remainingBuildingIndices.RemoveAt(randomIndex);
                    }

                    break;

                case 4:
                    destination = FindBuilding("Library").transform.position;

                    if (destination == null)
                    {
                        StartCoroutine(Thinking());
                    }
                    else
                    {
                        remainingBuildingIndices.RemoveAt(randomIndex);
                    }

                    break;

                case 5:
                    destination = FindBuilding("Central Square").transform.position;

                    if (destination == null)
                    {
                        StartCoroutine(Thinking());
                    }
                    else
                    {
                        remainingBuildingIndices.RemoveAt(randomIndex);
                    }

                    break;

                default:
                    Debug.LogError("Invalid random building index!");
                    break;

            }
        }
        else 
        {
            remainingBuildingIndices = new List<int> { 0, 1, 2, 3, 4, 5 };
        }
        

        MoveToBuilidng(destination);

        yield return new WaitForSeconds(2f);

        
    }

    public void MoveToBuilidng(Vector2 destination)
    {
        if (destination != null)
        {
            Vector2 direction = new Vector2(destination.x - transform.position.x, destination.y - transform.position.y).normalized;

            rigid.velocity = direction * moveSpeed * Time.deltaTime;

           
        }
        else
        {
            Debug.Log("시발");
        }
        
    }

    // 특정 위치로 이동하는 함수
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
        if(col.CompareTag("Building"))
        {
            isInBuilding = true;
            isMoving = false;
        }
    }
}
