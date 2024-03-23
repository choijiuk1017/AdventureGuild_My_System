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

    private List<int> usedEventIndices = new List<int>(); // ���� �̺�Ʈ �ε����� ����

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
            // ���� �̺�Ʈ�� �����ϰ� ������� �ʾ��� ���
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
            return -1; // �̺�Ʈ �迭�� ������� ��� -1�� ��ȯ�ϰų� ������ ó���� �����մϴ�.
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
                Debug.Log("Ư�� �Ƿ�");
            }
        }
        else
        {
            Debug.LogWarning("��ȿ���� ���� �̺�Ʈ �ε����Դϴ�." + eventNum);
        }

        

    }
}
