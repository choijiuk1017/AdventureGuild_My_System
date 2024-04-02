using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�̺�Ʈ�� ���� �ٷ�� ��ũ��Ʈ
public class EventSystem : MonoBehaviour
{
    public ItemEvents[] events;
    public ItemSetting itemSetting;

    public DataParser dataParser;

    //�ߺ� Ȯ���ϴµ� ����� ���� �̺�Ʈ ����Ʈ
    private HashSet<int> usedEventIndices = new HashSet<int>(); //�ߺ��� ���� ������� �ʱ� ���� HashSet ���


    void Start()
    {
        GameObject dataParserObject = GameObject.Find("DataParser");

        if (dataParserObject != null)
        {
            dataParser = dataParserObject.GetComponent<DataParser>();
            if (dataParser != null)
            {
                //�̺�Ʈ ����Ʈ �޾ƿ�
                events = dataParser.List2Array<ItemEvents>(dataParser.events);

            }
            else
            {
                Debug.LogError("DataParser ������Ʈ�� ã�� �� �����ϴ�.");
            }
        }

        GameObject itemSettingObject = GameObject.Find("ItemManager");

        itemSetting = itemSettingObject.GetComponent<ItemSetting>();
    }

    // �̺�Ʈ �߻� �Լ�
    public void TriggerRandomEvent()
    {
        //��� ������ �̺�Ʈ���� Ȯ���ϴ� ����Ʈ
        List<int> availableEventIndices = new List<int>();

        //�̺�Ʈ ����Ʈ���� ������ ���� �̺�Ʈ�鸸 ��� ������ �̺�Ʈ�� �з�
        for (int i = 0; i < events.Length; i++)
        {
            if (!usedEventIndices.Contains(i))
            {
                availableEventIndices.Add(i);
            }
        }

        //��� ������ �̺�Ʈ �������� �ϳ� ����
        int randomIndex = Random.Range(0, availableEventIndices.Count);
        int eventIndex = availableEventIndices[randomIndex];

        //���� �̺�Ʈ�� ���� �̺�Ʈ�� �ʿ����� Ȯ��
        if(events[eventIndex].Event_Precede != 0) //�ʿ��ϴٸ�
        {
            usedEventIndices.Add(events[eventIndex].Event_Precede); //���� �̺�Ʈ�� ����� �̺�Ʈ�� Ȱ��

            ActiveEvent(events[eventIndex].Event_Precede);
        }
        else //�ʿ����� �ʴٸ�
        {
            usedEventIndices.Add(eventIndex); //���� �̺�Ʈ Ȱ��

            ActiveEvent(eventIndex);
        }
        
        //�̺�Ʈ ���� ���� Ȯ��
        int resetEventID = events[eventIndex].Reset_Event;

        //�̺�Ʈ ���� �̺�Ʈ�� �����ϴ���, ���Ǿ����� Ȯ��
        if (resetEventID != 0)
        {
            for (int i = 0; i < events.Length; i++)
            {
                //���� �̺�Ʈ�� �����ϰ� ���Ǿ��ٸ�
                if (events[i].Event_ID == resetEventID && usedEventIndices.Contains(i))
                {
                    //�ش� �̺�Ʈ�� ���� �̺�Ʈ���� ����
                    usedEventIndices.Remove(i); 
                    break;
                }
            }
        }
    }

    //�̺�Ʈ Ȱ��ȭ �Լ�
    public void ActiveEvent(int eventNum)
    {
        if (eventNum >= 0 && eventNum < events.Length)
        {

            Debug.Log(events[eventNum].Event_Script);

            //�̺�Ʈ Ÿ�Կ� ���� �з�
            //1 �̸� ����, 2 �̸� ����, 3 �̸� Ư�� �Ƿڷ� �з�
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
                Debug.Log("Ư�� �Ƿ�");
            }
        }
        else
        {
            Debug.LogWarning("��ȿ���� ���� �̺�Ʈ �ε����Դϴ�." + eventNum);
        }
    }

}
