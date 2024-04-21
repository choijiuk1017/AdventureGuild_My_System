using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//아이템 매입에 대해 UI 창 팝업 밑 각종 정보를 종합하여 표기하는 스크립트, 사실상 메인
public class PurchaseSystem : MonoBehaviour
{
    //현재 가진 재화를 표기해주는 텍스트
    public Text moneyText;

    //총 가격 표기 텍스트
    public Text priceText;

    //재화
    public float money;

    //각 아이템의 개수 * 가격을 저장할 변수
    public float itemsPrice;

    //총 가격 저장 변수
    public float totalPrice;

    public ItemSetting itemSetting;

    //아이템 UI 프리펩
    public GameObject itemPrefab;

    public GameObject purchasePanel;

    //아이템 UI를 담을 공간 
    public RectTransform content;


    private void Start()
    {
        GameObject itemSettingObject = GameObject.Find("ItemManager");
        itemSetting = itemSettingObject.GetComponent<ItemSetting>();

        SpawnItems();
        UpdateMoneyDisplay();
    }



    private void Update()
    {
        CalculateTotalPrice();
    }


    //아이템 UI를 생성하는 함수
    void SpawnItems()
    {

        Dictionary<Item, int> itemCountMap = new Dictionary<Item, int>();

        foreach (Item item in itemSetting.itemList)
        {

            int itemCount = Random.Range(1, 31);

            itemCountMap[item] = itemCount;

            Debug.Log(item.Item_ID + " 아이템 생성: " + itemCountMap[item]);

            GameObject itemGo = Instantiate(itemPrefab, content);

            ItemUI itemUI = itemGo.GetComponent<ItemUI>();

            itemUI.Setup(this, item, itemCountMap[item]);

        }

    }

    //아이템 가격 계산해서 총 가격을 구해주는 함수
    public void CalculateTotalPrice()
    {
        // 초기화
        totalPrice = 0;
        
        foreach (Item item in itemSetting.itemList)
        {
            ItemUI itemUI = FindItemUI(item); //각 아이템을 담고있는 UI를 불러옴

            itemUI.UpdatePrice(item.Item_Price_Def); //변동된 가격이 있다면 변동사항 적용

            //매입가를 저장할 변수
            float purchasePrice;

            //매입가 입력여부 확인
            if (!string.IsNullOrEmpty(itemUI.purchasePrice.text) && float.TryParse(itemUI.purchasePrice.text, out purchasePrice))
            {

                //매입가와 개수를 곱하여 총 가격에 더함
                if (itemUI != null && itemUI.isOn)
                {
                    itemsPrice = itemUI.itemCount * purchasePrice;
                    totalPrice += itemsPrice;
                }
                if (!itemUI.isOn)
                {
                    itemsPrice = 0;
                }
            }
            
            
        }
        priceText.text = totalPrice.ToString();
    }

    //각 아이템을 담고있는 아이템 UI를 찾는 함수
    private ItemUI FindItemUI(Item item)
    {
        // content 아래에 있는 모든 ItemUI 컴포넌트를 검색하여 해당 아이템의 ItemUI를 반환
        foreach (Transform child in content)
        {
            ItemUI itemUI = child.GetComponent<ItemUI>();
            if (itemUI != null && itemUI.item == item)
            {
                return itemUI;
            }
        }
        return null;
    }

    //아이템 매입 함수
    public void PurchaseItem()
    {
        if(money < totalPrice)
        {
            Debug.Log("돈이 부족합니다.");

            return;
        }

        money -= totalPrice;

        UpdateMoneyDisplay();
    }

    //재화 변동 사항 업데이트 함수
    void UpdateMoneyDisplay()
    {
        moneyText.text = "소지금: " + money.ToString();
    }

    public void ExitButtonClick()
    {
        purchasePanel.gameObject.SetActive(false);
    }

    //가격 변동에 따른 이벤트 함수
    private void OnDestroy()
    {
        // 스크립트가 소멸될 때 핸들러 제거
        ItemSetting.OnPriceChanged -= HandlePriceChanged;
    }

    //가격 변동에 따른 이벤트 함수
    private void HandlePriceChanged()
    {
        Debug.Log("가격 변동됨");
        CalculateTotalPrice();
    }
}
