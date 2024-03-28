using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public ItemEvents[] events;
    public Item[] items;

    public ItemSetting itemSetting;
    public DataParser dataParser;

    private float timeSinceLastEvent = 0f;
    private float eventInterval = 7f;

    private HashSet<int> usedEventIndices = new HashSet<int>(); //중복된 값을 허용하지 않기 위해 HashSet 사용



    // Start is called before the first frame update
    void Start()
    {
        GameObject dataParserObject = GameObject.Find("DataParser");

        if (dataParserObject != null)
        {
            dataParser = dataParserObject.GetComponent<DataParser>();
            if (dataParser != null)
            {
                events = dataParser.List2Array<ItemEvents>(dataParser.events);

            }
            else
            {
                Debug.LogError("DataParser 컴포넌트를 찾을 수 없습니다.");
            }
        }

        GameObject itemSettingObject = GameObject.Find("ItemManager");

        itemSetting = itemSettingObject.GetComponent<ItemSetting>();

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
        List<int> availableEventIndices = new List<int>();

        for (int i = 0; i < events.Length; i++)
        {
            if (!usedEventIndices.Contains(i))
            {
                if (events[i].Event_Precede != 0 && !usedEventIndices.Contains(events[i].Event_Precede))
                {
                    availableEventIndices.Add(events[i].Event_Precede);
                }
                else
                {
                    availableEventIndices.Add(i);
                }
            }

        }

        if (availableEventIndices.Count > 0)
        {
            int randomIndex = Random.Range(0, availableEventIndices.Count);
            int eventIndex = availableEventIndices[randomIndex];

            // 중복되는 이벤트 방지를 위해 사용된 이벤트로 기록
            usedEventIndices.Add(eventIndex);

            // 이벤트 활성화
            ActiveEvent(eventIndex);

            int resetEventID = events[eventIndex].Reset_Event;
            if (resetEventID != 0)
            {
                for (int i = 0; i < events.Length; i++)
                {
                    if (events[i].Event_ID == resetEventID && usedEventIndices.Contains(i))
                    {
                        usedEventIndices.Remove(i);
                        break;
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("모든 이벤트가 사용되었습니다.");
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
                itemSetting.ChangePrices(increaseAmount, events[eventNum].Event_Sub_Type);
            }
            else if (events[eventNum].Event_Main_Type == 2)
            {
                float decreaseAmount = 1f - events[eventNum].Event_Detail_Type / 100f;
                itemSetting.ChangePrices(decreaseAmount, events[eventNum].Event_Sub_Type);
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
