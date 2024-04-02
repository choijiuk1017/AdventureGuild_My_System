using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//아이템의 가격을 관리하는 스크립트
public class ItemSetting : MonoBehaviour
{
    //아이템 가격 변동을 알려주는 이벤트
    public delegate void PriceChangedEventHandler();
    public static event PriceChangedEventHandler OnPriceChanged;

    public DataParser dataParser;

    public Item[] items;


    //Item에 대한 정보를 받으며 시작함
    void Start()
    {
        GameObject dataParserObject = GameObject.Find("DataParser");
        
        if (dataParserObject != null)
        {
            dataParser = dataParserObject.GetComponent<DataParser>();
            if (dataParser != null)
            {
                //아이템 리스트 받아옴
                items = dataParser.List2Array<Item>(dataParser.items);
            }
            else
            {
                Debug.LogError("DataParser 컴포넌트를 찾을 수 없습니다.");
            }
        }
    }
    
    //가격 변동 함수, EventSystem 스크립트에서 사용함
    public void ChangePrices(float factor, int itemID)
    {
        foreach (Item item in items)
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
