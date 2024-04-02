using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�������� ������ �����ϴ� ��ũ��Ʈ
public class ItemSetting : MonoBehaviour
{
    //������ ���� ������ �˷��ִ� �̺�Ʈ
    public delegate void PriceChangedEventHandler();
    public static event PriceChangedEventHandler OnPriceChanged;

    public DataParser dataParser;

    public Item[] items;


    //Item�� ���� ������ ������ ������
    void Start()
    {
        GameObject dataParserObject = GameObject.Find("DataParser");
        
        if (dataParserObject != null)
        {
            dataParser = dataParserObject.GetComponent<DataParser>();
            if (dataParser != null)
            {
                //������ ����Ʈ �޾ƿ�
                items = dataParser.List2Array<Item>(dataParser.items);
            }
            else
            {
                Debug.LogError("DataParser ������Ʈ�� ã�� �� �����ϴ�.");
            }
        }
    }
    
    //���� ���� �Լ�, EventSystem ��ũ��Ʈ���� �����
    public void ChangePrices(float factor, int itemID)
    {
        foreach (Item item in items)
        {
            if (item.Item_ID == itemID)
            {
                item.Item_Price_Def *= factor;
                Debug.Log(item.Item_ID + " ������ ���� ����: " + item.Item_Price_Def); //Ȯ�ο� �α�

                if (OnPriceChanged != null)
                    OnPriceChanged();

                break;
            }
        }
    }
}
