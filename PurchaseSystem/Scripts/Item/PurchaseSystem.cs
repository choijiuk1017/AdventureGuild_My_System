using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//������ ���Կ� ���� UI â �˾� �� ���� ������ �����Ͽ� ǥ���ϴ� ��ũ��Ʈ, ��ǻ� ����
public class PurchaseSystem : MonoBehaviour
{
    //���� ���� ��ȭ�� ǥ�����ִ� �ؽ�Ʈ
    public Text moneyText;

    //�� ���� ǥ�� �ؽ�Ʈ
    public Text priceText;

    //��ȭ
    public float money;

    //�� �������� ���� * ������ ������ ����
    public float itemsPrice;

    //�� ���� ���� ����
    public float totalPrice;

    public ItemSetting itemSetting;

    //������ UI ������
    public GameObject itemPrefab;

    //������ UI�� ���� ����
    public RectTransform content;

    //������ UI�� ǥ�⸦ ���� ������
    public float buttonSize = 50f;
    public float spacing = 10f;

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


    //������ UI�� �����ϴ� �Լ�
    void SpawnItems()
    {
        RectTransform contentRectTransform = content.GetComponent<RectTransform>();

        float startX = -contentRectTransform.rect.width * 0.5f;
        float startY = contentRectTransform.rect.height * 0.5f;

        float gap = spacing * 3;

        float contentHeight = itemSetting.itemList.Count * (buttonSize + spacing) + gap;

        content.sizeDelta = new Vector2(content.sizeDelta.x, Mathf.Max(content.sizeDelta.y, contentHeight));

        Dictionary<Item, int> itemCountMap = new Dictionary<Item, int>();

        foreach (Item item in itemSetting.itemList)
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

    //������ ���� ����ؼ� �� ������ �����ִ� �Լ�
    public void CalculateTotalPrice()
    {
        // �ʱ�ȭ
        totalPrice = 0;
        
        foreach (Item item in itemSetting.itemList)
        {
            ItemUI itemUI = FindItemUI(item); //�� �������� ����ִ� UI�� �ҷ���

            itemUI.UpdatePrice(item.Item_Price_Def); //������ ������ �ִٸ� �������� ����

            //���԰��� ������ ����
            float purchasePrice;

            //���԰� �Է¿��� Ȯ��
            if (!string.IsNullOrEmpty(itemUI.purchasePrice.text) && float.TryParse(itemUI.purchasePrice.text, out purchasePrice))
            {

                //���԰��� ������ ���Ͽ� �� ���ݿ� ����
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

    //�� �������� ����ִ� ������ UI�� ã�� �Լ�
    private ItemUI FindItemUI(Item item)
    {
        // content �Ʒ��� �ִ� ��� ItemUI ������Ʈ�� �˻��Ͽ� �ش� �������� ItemUI�� ��ȯ
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

    //������ ���� �Լ�
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

    //��ȭ ���� ���� ������Ʈ �Լ�
    void UpdateMoneyDisplay()
    {
        moneyText.text = "������: " + money.ToString();
    }

    //���� ������ ���� �̺�Ʈ �Լ�
    private void OnDestroy()
    {
        // ��ũ��Ʈ�� �Ҹ�� �� �ڵ鷯 ����
        ItemSetting.OnPriceChanged -= HandlePriceChanged;
    }

    //���� ������ ���� �̺�Ʈ �Լ�
    private void HandlePriceChanged()
    {
        Debug.Log("���� ������");
        CalculateTotalPrice();
    }
}
