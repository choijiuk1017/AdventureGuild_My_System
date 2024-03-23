using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public bool isEvent;

    public ItemEvents[] events;
    public Item[] items;

    public PurchaseSystem purchaseSystem;
    public DataParser dataParser;

    private float timeSinceLastEvent = 0f;
    private float eventInterval = 7f;

    private List<int> usedEventIndices = new List<int>(); // 사용된 이벤트 인덱스를 추적

    // Start is called before the first frame update
    void Start()
    {
       events = dataParser.List2Array<ItemEvents>(dataParser.events);

    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastEvent += Time.deltaTime;
        if (timeSinceLastEvent >= eventInterval)
        {
            timeSinceLastEvent = 0f;
            TriggerRandomEvent();
        }
    }

    private void TriggerRandomEvent()
    {
        int randomIndex;
        do
        {
            randomIndex = RandomEventIndex();
        } while (usedEventIndices.Contains(randomIndex));

        if (events[randomIndex].Event_Precede != 0 && !usedEventIndices.Contains(events[randomIndex].Event_Precede))
        {
            // 선행 이벤트가 존재하고 실행되지 않았을 경우
            ActiveEvent(events[randomIndex].Event_Precede);
            usedEventIndices.Add(events[randomIndex].Event_Precede);
        }
        else
        {
            ActiveEvent(randomIndex);
            usedEventIndices.Add(randomIndex);
        }
    }

    private int RandomEventIndex()
    {
        if (events.Length > 0)
        {
            return Random.Range(0, events.Length);
        }
        else
        {
            return -1; // 이벤트 배열이 비어있을 경우 -1을 반환하거나 적절한 처리를 수행합니다.
        }
    }


    public void ActiveEvent(int eventNum)
    {
        if (eventNum >= 0 && eventNum < events.Length)
        {

            Debug.Log(events[eventNum].Event_Script);

            if (events[eventNum].Event_Main_Type == 1)
            {
                float increaseAmount = 1f + events[eventNum].Event_Detail_Type / 100f;
                purchaseSystem.ChangePrices(increaseAmount, events[eventNum].Event_Sub_Type);
            }
            else if (events[eventNum].Event_Main_Type == 2)
            {
                float decreaseAmount = 1f - events[eventNum].Event_Detail_Type / 100f;
                purchaseSystem.ChangePrices(decreaseAmount, events[eventNum].Event_Sub_Type);
            }
            else
            {
                Debug.Log("특수 의뢰");
            }
        }
        else
        {
            Debug.LogWarning("유효하지 않은 이벤트 인덱스입니다." + eventNum);
        }

        

    }
}
