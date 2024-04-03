using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Data;

//이벤트에 대해 다루는 스크립트
public class EventSystem : MonoBehaviour
{

    private Dictionary<int, ItemEvents> eventData;
    [SerializeField]
    private List<ItemEvents> debugData;

    public List<ItemEvents> eventList;

    private const string eventDataTableName = "Event_Master";

    private const string eventID = "Event_ID";
    private const string eventName = "Event_Name";
    private const string eventMainType = "Event_Main_Type";
    private const string eventSubType = "Event_Sub_Type";
    private const string eventDetailType = "Event_Detail_Type";
    private const string eventScript = "Event_Script";
    private const string eventPrecede = "Event_Precede";
    private const string eventProb = "Event_Prob";
    private const string resetEvent = "Reset_Event";

    public List<Item> itemList;
    public ItemSetting itemSetting;

    //중복 확인하는데 사용할 사용된 이벤트 리스트
    private HashSet<int> usedEventIndices = new HashSet<int>(); //중복된 값을 허용하지 않기 위해 HashSet 사용

    void Start()
    {

        InitEventData();

        GameObject itemSettingObject = GameObject.Find("ItemManager");

        itemSetting = itemSettingObject.GetComponent<ItemSetting>();
    }

    private void InitEventData()
    {
        eventData = new Dictionary<int, ItemEvents>();
        debugData = new();

        var events = DataParser.Parser(eventDataTableName);

        foreach (var itemEvent in events)
        {
            var iData = new ItemEvents()
            {
                Event_ID = DataParser.IntParse(itemEvent[eventID]),
                Event_Name = itemEvent[eventName].ToString(),
                Event_Main_Type = DataParser.IntParse(itemEvent[eventMainType]),
                Event_Sub_Type = DataParser.IntParse(itemEvent[eventSubType]),
                Event_Detail_Type = DataParser.IntParse(itemEvent[eventDetailType]),
                Event_Script = itemEvent[eventScript].ToString(),
                Event_Precede = DataParser.IntParse(itemEvent[eventPrecede]),
                Event_Prob = DataParser.IntParse(itemEvent[eventProb]),
                Reset_Event = DataParser.IntParse(itemEvent[resetEvent]),
            };

            debugData.Add(iData);
            eventData.Add(iData.Event_ID, iData);
        }
        eventList = debugData;
    }

    // 이벤트 발생 함수
    public void TriggerRandomEvent()
    {
        int randomEventIndex = GetRandomAvailableEventIndex();

        if (randomEventIndex != -1)
        {
            ActiveEvent(randomEventIndex);
            MarkEventAsUsed(randomEventIndex);
            CheckResetEvent(randomEventIndex);
        }
    }

    private int GetRandomAvailableEventIndex()
    {
        List<int> availableEventIndices = new List<int>();

        for (int i = 0; i < eventList.Count; i++)
        {
            if (!usedEventIndices.Contains(i))
            {
                availableEventIndices.Add(i);
            }
        }

        if (availableEventIndices.Count == 0)
        {
            // 사용 가능한 이벤트가 없는 경우
            return -1;
        }

        int randomIndex = Random.Range(0, availableEventIndices.Count);
        int randomEventIndex = availableEventIndices[randomIndex];

        // 선택된 이벤트가 선행 이벤트를 필요로 하는 경우
        if (eventList[randomEventIndex].Event_Precede != 0)
        {
            int precedeIndex = eventList[randomEventIndex].Event_Precede;

            for (int i = 0; i < eventList.Count; i++)
            {
                if (eventList[i].Event_ID == precedeIndex && !usedEventIndices.Contains(i))
                {
                    return i;
                }
            }

        }

        return randomEventIndex;
    }

    private void MarkEventAsUsed(int eventIndex)
    {
        usedEventIndices.Add(eventIndex);
    }

    //리셋 이벤트 관리 함수
    private void CheckResetEvent(int eventIndex)
    {
        int resetEventID = eventList[eventIndex].Reset_Event;

        if (resetEventID != 0 )
        {
            for(int i = 0; i < eventList.Count; i++)
            {
                if (eventList[i].Event_ID == resetEventID && usedEventIndices.Contains(i))
                {
                    usedEventIndices.Remove(i);
                }
            }  
        }
    }

    //이벤트 활성화 함수
    public void ActiveEvent(int eventNum)
    {
        if (eventNum >= 0 && eventNum < debugData.Count)
        {

            Debug.Log(debugData[eventNum].Event_Script);

            //이벤트 타입에 따른 분류
            //1 이면 증가, 2 이면 감소, 3 이면 특수 의뢰로 분류
            if (debugData[eventNum].Event_Main_Type == 1)
            {
                float increaseAmount = 1f + debugData[eventNum].Event_Detail_Type / 100f;
                itemSetting.ChangePrices(increaseAmount, debugData[eventNum].Event_Sub_Type);
            }
            else if (debugData[eventNum].Event_Main_Type == 2)
            {
                float decreaseAmount = 1f - debugData[eventNum].Event_Detail_Type / 100f;
                itemSetting.ChangePrices(decreaseAmount, debugData[eventNum].Event_Sub_Type);
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
