using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//이벤트에 대해 다루는 스크립트
public class EventSystem : MonoBehaviour
{
    public ItemEvents[] events;
    public ItemSetting itemSetting;

    public DataParser dataParser;

    //중복 확인하는데 사용할 사용된 이벤트 리스트
    private HashSet<int> usedEventIndices = new HashSet<int>(); //중복된 값을 허용하지 않기 위해 HashSet 사용


    void Start()
    {
        GameObject dataParserObject = GameObject.Find("DataParser");

        if (dataParserObject != null)
        {
            dataParser = dataParserObject.GetComponent<DataParser>();
            if (dataParser != null)
            {
                //이벤트 리스트 받아옴
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

    // 이벤트 발생 함수
    public void TriggerRandomEvent()
    {
        //사용 가능한 이벤트인지 확인하는 리스트
        List<int> availableEventIndices = new List<int>();

        //이벤트 리스트에서 사용되지 않은 이벤트들만 사용 가능한 이벤트로 분류
        for (int i = 0; i < events.Length; i++)
        {
            if (!usedEventIndices.Contains(i))
            {
                availableEventIndices.Add(i);
            }
        }

        //사용 가능한 이벤트 내에서만 하나 뽑음
        int randomIndex = Random.Range(0, availableEventIndices.Count);
        int eventIndex = availableEventIndices[randomIndex];

        //뽑은 이벤트가 선행 이벤트가 필요한지 확인
        if(events[eventIndex].Event_Precede != 0) //필요하다면
        {
            usedEventIndices.Add(events[eventIndex].Event_Precede); //선행 이벤트로 저장된 이벤트를 활성

            ActiveEvent(events[eventIndex].Event_Precede);
        }
        else //필요하지 않다면
        {
            usedEventIndices.Add(eventIndex); //뽑은 이벤트 활성

            ActiveEvent(eventIndex);
        }
        
        //이벤트 리셋 정보 확인
        int resetEventID = events[eventIndex].Reset_Event;

        //이벤트 리셋 이벤트가 존재하는지, 사용되었는지 확인
        if (resetEventID != 0)
        {
            for (int i = 0; i < events.Length; i++)
            {
                //리셋 이벤트가 존재하고 사용되었다면
                if (events[i].Event_ID == resetEventID && usedEventIndices.Contains(i))
                {
                    //해당 이벤트를 사용된 이벤트에서 제거
                    usedEventIndices.Remove(i); 
                    break;
                }
            }
        }
    }

    //이벤트 활성화 함수
    public void ActiveEvent(int eventNum)
    {
        if (eventNum >= 0 && eventNum < events.Length)
        {

            Debug.Log(events[eventNum].Event_Script);

            //이벤트 타입에 따른 분류
            //1 이면 증가, 2 이면 감소, 3 이면 특수 의뢰로 분류
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
