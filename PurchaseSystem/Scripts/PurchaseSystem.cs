using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseSystem : MonoBehaviour
{
    public Text moneyText;

    public int money;

    public Item[] items;
    public GameObject itemPrefab;
    public RectTransform content;

    public ItemDataParser dataParser;

    public float buttonSize = 50f;
    public float spacing = 10f;

    private void Start()
    {
        items = dataParser.List2Array();
        SpawnItems();
        UpdateMoneyDisplay(); 
    }


    void SpawnItems()
    {
        // Content의 RectTransform 컴포넌트 가져오기
        RectTransform contentRectTransform = content.GetComponent<RectTransform>();

        // Content의 상단을 기준으로 시작 위치 계산
        float startX = -contentRectTransform.rect.width * 0.5f;
        float startY = contentRectTransform.rect.height * 0.5f;

        // content와 아이템 간 간격 추가
        float gap = spacing * 3;

        // 아이템을 배치할 Content의 높이 계산
        float contentHeight = items.Length * (buttonSize + spacing) + gap;

        // Content의 크기를 기존의 크기로 설정
        content.sizeDelta = new Vector2(content.sizeDelta.x, Mathf.Max(content.sizeDelta.y, contentHeight));

        Dictionary<Item, int> itemCountMap = new Dictionary<Item, int>(); // 각 아이템의 발생 횟수를 추적하기 위한 딕셔너리

        foreach (Item item in items)
        {
            float itemY = startY - (itemCountMap.Count * (buttonSize + spacing) + gap);

            int itemCount = Random.Range(1, 31); // 각 아이템의 개수를 1에서 30까지 랜덤하게 설정

            itemCountMap[item] = itemCount;

            Debug.Log(item.Item_ID + " 아이템 생성: " + itemCountMap[item]);

            GameObject itemGo = Instantiate(itemPrefab, content);

            ItemUI itemUI = itemGo.GetComponent<ItemUI>();

            itemUI.Setup(this, item, itemCountMap[item]); // 아이템 개수 전달

            RectTransform rectTransform = itemGo.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(buttonSize, buttonSize);
            rectTransform.anchoredPosition = new Vector2(startX, itemY);
        }
    }


    public void PurchaseItem(Item item)
    {
        //if(money < item.price)
        //{
        //    Debug.Log("돈이 부족합니다.");

        //    return;
        //}

        //money -= item.price;

        //UpdateMoneyDisplay();
    }


    void UpdateMoneyDisplay()
    {
        moneyText.text = "소지금: " + money.ToString();
    }
}
