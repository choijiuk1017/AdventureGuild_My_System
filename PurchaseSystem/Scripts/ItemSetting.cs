using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//아이템의 가격 변동을 다루는 스크립트
public class ItemSetting : MonoBehaviour
{
    public delegate void PriceChangedEventHandler();
    public static event PriceChangedEventHandler OnPriceChanged;

    public float currentItemPrice;

    public DataParser dataParser;

    public Item[] items;

    // Start is called before the first frame update
    void Start()
    {
        GameObject dataParserObject = GameObject.Find("DataParser");

        if (dataParserObject != null)
        {
            dataParser = dataParserObject.GetComponent<DataParser>();
            if (dataParser != null)
            {
                items = dataParser.List2Array<Item>(dataParser.items);
            }
            else
            {
                Debug.LogError("DataParser 컴포넌트를 찾을 수 없습니다.");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ChangePrices(float factor, int itemID)
    {
        foreach (Item item in items)
        {
            if (item.Item_ID == itemID)
            {
                item.Item_Price_Def *= factor;
                Debug.Log(item.Item_ID + " 아이템 가격 변경: " + item.Item_Price_Def);

                if (OnPriceChanged != null)
                    OnPriceChanged();

                break;
            }
        }
    }
}
