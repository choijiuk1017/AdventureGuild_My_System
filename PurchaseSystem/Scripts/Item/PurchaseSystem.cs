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
        // ItemSetting Ŭ�������� ������ ����Ʈ�� ������ ������ ���
        while (itemSetting.items == null)
        {
            yield return null;
        }

        // ������ ����Ʈ�� ������ �Ŀ� �������� �����ϰ� ȭ�� ����
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

            Debug.Log(item.Item_ID + " ������ ����: " + itemCountMap[item]);

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
        // �ʱ�ȭ
        totalPrice = 0;
        

        foreach (Item item in itemSetting.items)
        {
            ItemUI itemUI = FindItemUI(item); // �ش� �����ۿ� ���� ItemUI�� ã���ϴ�.
            itemUI.UpdatePrice(item.Item_Price_Def);
            float purchasePrice;
            if (!string.IsNullOrEmpty(itemUI.purchasePrice.text) && float.TryParse(itemUI.purchasePrice.text, out purchasePrice))
            {
                if (itemUI != null && itemUI.isOn)
                {
                    itemsPrice = itemUI.itemCount * purchasePrice;
                    // �� �������� ������ ���Ͽ� �� ���ݿ� �߰��մϴ�.
                    totalPrice += itemsPrice;
                }
                if (!itemUI.isOn)
                {
                    itemsPrice = 0;
                }
            }
            
            
        }

        // �� ������ ȭ�鿡 ǥ���մϴ�.
        priceText.text = totalPrice.ToString();
    }

    private ItemUI FindItemUI(Item item)
    {
        // content �Ʒ��� �ִ� ��� ItemUI ������Ʈ�� �˻��Ͽ� �ش� �������� ItemUI�� ��ȯ�մϴ�.
        foreach (Transform child in content)
        {
            ItemUI itemUI = child.GetComponent<ItemUI>();
            if (itemUI != null && itemUI.item == item)
            {
                return itemUI;
            }
        }
        return null; // �ش� �������� ã�� ���� ���
    }


    public void PurchaseItem()
    {
        if(money < totalPrice)
        {
            Debug.Log("���� �����մϴ�.");

            return;
        }

        money -= totalPrice;

        UpdateMoneyDisplay();
    }


    void UpdateMoneyDisplay()
    {
        moneyText.text = "������: " + money.ToString();
    }

    private void OnDestroy()
    {
        // ��ũ��Ʈ�� �Ҹ�� �� �ڵ鷯 ����
        ItemSetting.OnPriceChanged -= HandlePriceChanged;
    }

    private void HandlePriceChanged()
    {
        Debug.Log("���� ������");
        // ������ ����Ǿ��� �� ȣ��Ǵ� �Լ�
        CalculateTotalPrice();
    }
}
