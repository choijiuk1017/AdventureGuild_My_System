using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseSystem : MonoBehaviour
{
    public Text moneyText;
    public Text priceText;

    public float money;

    public Item[] items;

    public GameObject itemPrefab;
    public RectTransform content;

    public ItemSetting itemSetting;

    public float buttonSize = 50f;
    public float spacing = 10f;

    public float itemsPrice;
    public float totalPrice;

    private void Start()
    {
        GameObject itemSettingObject = GameObject.Find("ItemManager");
        itemSetting = itemSettingObject.GetComponent<ItemSetting>();

        StartCoroutine(WaitForItemSetting(itemSetting));
    }

    IEnumerator WaitForItemSetting(ItemSetting itemSetting)
    {
        // ItemSetting 클래스에서 아이템 리스트가 설정될 때까지 대기
        while (itemSetting.items == null)
        {
            yield return null;
        }

        // 아이템 리스트가 설정된 후에 아이템을 스폰하고 화면 갱신
        items = itemSetting.items;
        SpawnItems();
        UpdateMoneyDisplay();
    }

    private void Update()
    {
        CalculateTotalPrice();
    }



    void SpawnItems()
    {
        RectTransform contentRectTransform = content.GetComponent<RectTransform>();

        float startX = -contentRectTransform.rect.width * 0.5f;
        float startY = contentRectTransform.rect.height * 0.5f;

        float gap = spacing * 3;

        float contentHeight = items.Length * (buttonSize + spacing) + gap;

        content.sizeDelta = new Vector2(content.sizeDelta.x, Mathf.Max(content.sizeDelta.y, contentHeight));

        Dictionary<Item, int> itemCountMap = new Dictionary<Item, int>();

        foreach (Item item in items)
        {
            float itemY = startY - (itemCountMap.Count * (buttonSize + spacing) + gap);

            int itemCount = Random.Range(1, 31);

            itemCountMap[item] = itemCount;

            Debug.Log(item.Item_ID + " 아이템 생성: " + itemCountMap[item]);

            GameObject itemGo = Instantiate(itemPrefab, content);

            ItemUI itemUI = itemGo.GetComponent<ItemUI>();

            itemUI.Setup(this, item, itemCountMap[item]);

            RectTransform rectTransform = itemGo.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(buttonSize, buttonSize);
            rectTransform.anchoredPosition = new Vector2(startX, itemY);
        }

    }

    public void CalculateTotalPrice()
    {
        // 초기화
        totalPrice = 0;
        

        foreach (Item item in itemSetting.items)
        {
            ItemUI itemUI = FindItemUI(item); // 해당 아이템에 대한 ItemUI를 찾습니다.
            itemUI.UpdatePrice(item.Item_Price_Def);
            float purchasePrice;
            if (!string.IsNullOrEmpty(itemUI.purchasePrice.text) && float.TryParse(itemUI.purchasePrice.text, out purchasePrice))
            {
                if (itemUI != null && itemUI.isOn)
                {
                    itemsPrice = itemUI.itemCount * purchasePrice;
                    // 각 아이템의 가격을 곱하여 총 가격에 추가합니다.
                    totalPrice += itemsPrice;
                }
                if (!itemUI.isOn)
                {
                    itemsPrice = 0;
                }
            }
            
            
        }

        // 총 가격을 화면에 표시합니다.
        priceText.text = totalPrice.ToString();
    }

    private ItemUI FindItemUI(Item item)
    {
        // content 아래에 있는 모든 ItemUI 컴포넌트를 검색하여 해당 아이템의 ItemUI를 반환합니다.
        foreach (Transform child in content)
        {
            ItemUI itemUI = child.GetComponent<ItemUI>();
            if (itemUI != null && itemUI.item == item)
            {
                return itemUI;
            }
        }
        return null; // 해당 아이템을 찾지 못한 경우
    }


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


    void UpdateMoneyDisplay()
    {
        moneyText.text = "소지금: " + money.ToString();
    }

    private void OnDestroy()
    {
        // 스크립트가 소멸될 때 핸들러 제거
        ItemSetting.OnPriceChanged -= HandlePriceChanged;
    }

    private void HandlePriceChanged()
    {
        Debug.Log("가격 변동됨");
        // 가격이 변경되었을 때 호출되는 함수
        CalculateTotalPrice();
    }
}
