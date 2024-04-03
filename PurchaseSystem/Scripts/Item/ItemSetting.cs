using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Data;


//아이템의 가격을 관리하는 스크립트
public class ItemSetting : MonoBehaviour
{
    //아이템 가격 변동을 알려주는 이벤트
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


    //Item에 대한 정보를 받으며 시작함
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
    
    //가격 변동 함수, EventSystem 스크립트에서 사용함
    public void ChangePrices(float factor, int itemID)
    {
        foreach (Item item in itemList)
        {
            if (item.Item_ID == itemID)
            {
                item.Item_Price_Def *= factor;
                Debug.Log(item.Item_ID + " 아이템 가격 변경: " + item.Item_Price_Def); //확인용 로그

                if (OnPriceChanged != null)
                    OnPriceChanged();

                break;
            }
        }
    }
}
