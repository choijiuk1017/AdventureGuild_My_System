using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Data;


//�������� ������ �����ϴ� ��ũ��Ʈ
public class ItemSetting : MonoBehaviour
{
    //������ ���� ������ �˷��ִ� �̺�Ʈ
    public delegate void PriceChangedEventHandler();
    public static event PriceChangedEventHandler OnPriceChanged;

    private Dictionary<int, Item> itemData;
    [SerializeField]
    private List<Item> debugData;

    private const string itemDataTableName = "Item_Master";

    private const string itemID = "Item_ID";
    private const string itemName = "Item_Name";
    private const string itemNameUI = "Item_Name_UI";
    private const string itemIcon = "Item_Icon";
    private const string itemText = "Item_Text";
    private const string itemPriceDef = "Item_Price_Def";
    private const string itemPriceMin = "Item_Price_Min";
    private const string itemPriceMax = "Item_Price_Max";

    public List<Item> itemList;


    //Item�� ���� ������ ������ ������
    void Start()
    {
        InitItemData();
    }

    private void InitItemData()
    {
        itemData = new Dictionary<int, Item>();
        debugData = new();

        var items = DataParser.Parser(itemDataTableName);

        foreach (var item in items)
        {
            var iData = new Item()
            {
                Item_ID = DataParser.IntParse(item[itemID]),
                Item_Name = item[itemName].ToString(),
                Item_Name_UI = item[itemNameUI].ToString(),
                Item_Icon = item[itemIcon].ToString(),
                Item_Text = item[itemText].ToString(),
                Item_Price_Def = DataParser.FloatParse(item[itemPriceDef]),
                Item_Price_Min = DataParser.FloatParse(item[itemPriceMin]),
                Item_Price_Max = DataParser.FloatParse(item[itemPriceMax])
            };

            debugData.Add(iData);
            itemData.Add(iData.Item_ID, iData);
        }
        itemList = debugData;
    }

    public Item GetItemData(int itemID)
    {
        return itemData[itemID];
    }
    
    //���� ���� �Լ�, EventSystem ��ũ��Ʈ���� �����
    public void ChangePrices(float factor, int itemID)
    {
        foreach (Item item in itemList)
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
