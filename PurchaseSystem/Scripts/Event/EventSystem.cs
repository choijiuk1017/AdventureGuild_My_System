using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public ItemEvents[] events;
    public Item[] items;

    public ItemSetting itemSetting;
    public DataParser dataParser;



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
 

    }


    public void TriggerRandomEvent()
    {
        List<int> availableEventIndices = new List<int>();

        for (int i = 0; i < events.Length; i++)
        {
            if (!usedEventIndices.Contains(i))
            {
                availableEventIndices.Add(i);
            }
        }

        int randomIndex = Random.Range(0, availableEventIndices.Count);
        int eventIndex = availableEventIndices[randomIndex];

        if(events[eventIndex].Event_Precede != 0)
        {
            usedEventIndices.Add(events[eventIndex].Event_Precede);

            ActiveEvent(events[eventIndex].Event_Precede);
        }
        else
        {

            usedEventIndices.Add(eventIndex);

            ActiveEvent(eventIndex);
        }
        

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
