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
        // Content�� RectTransform ������Ʈ ��������
        RectTransform contentRectTransform = content.GetComponent<RectTransform>();

        // Content�� ����� �������� ���� ��ġ ���
        float startX = -contentRectTransform.rect.width * 0.5f;
        float startY = contentRectTransform.rect.height * 0.5f;

        // content�� ������ �� ���� �߰�
        float gap = spacing * 3;

        // �������� ��ġ�� Content�� ���� ���
        float contentHeight = items.Length * (buttonSize + spacing) + gap;

        // Content�� ũ�⸦ ������ ũ��� ����
        content.sizeDelta = new Vector2(content.sizeDelta.x, Mathf.Max(content.sizeDelta.y, contentHeight));

        Dictionary<Item, int> itemCountMap = new Dictionary<Item, int>(); // �� �������� �߻� Ƚ���� �����ϱ� ���� ��ųʸ�

        foreach (Item item in items)
        {
            float itemY = startY - (itemCountMap.Count * (buttonSize + spacing) + gap);

            int itemCount = Random.Range(1, 31); // �� �������� ������ 1���� 30���� �����ϰ� ����

            itemCountMap[item] = itemCount;

            Debug.Log(item.Item_ID + " ������ ����: " + itemCountMap[item]);

            GameObject itemGo = Instantiate(itemPrefab, content);

            ItemUI itemUI = itemGo.GetComponent<ItemUI>();

            itemUI.Setup(this, item, itemCountMap[item]); // ������ ���� ����

            RectTransform rectTransform = itemGo.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(buttonSize, buttonSize);
            rectTransform.anchoredPosition = new Vector2(startX, itemY);
        }
    }


    public void PurchaseItem(Item item)
    {
        //if(money < item.price)
        //{
        //    Debug.Log("���� �����մϴ�.");

        //    return;
        //}

        //money -= item.price;

        //UpdateMoneyDisplay();
    }


    void UpdateMoneyDisplay()
    {
        moneyText.text = "������: " + money.ToString();
    }
}
