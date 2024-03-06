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
    public Transform itemContainer;

    public ItemDataParser dataParser;

    public int gridSize = 3; 
    public float buttonSize = 50f;
    public float spacing = 10f;

    private void Start()
    {
        items = dataParser.List2Array();

        ShuffleItems();

        SpawnItems(gridSize * gridSize);
        UpdateMoneyDisplay(); 
    }
    void ShuffleItems()
    {
        // ������ �迭�� �����ϰ� ����
        for (int i = 0; i < items.Length; i++)
        {
            int randomIndex = Random.Range(i, items.Length);
            Item tempItem = items[randomIndex];
            items[randomIndex] = items[i];
            items[i] = tempItem;
        }
    }

    void SpawnItems(int itemCount)
    {
        float startX = -200f;
        float startY = 80f;

        for (int i = 0; i < itemCount; i++)
        {
            int row = i / gridSize;
            int col = i % gridSize;

            float rowY = startY - row * (buttonSize + spacing);
            float colX = startX + col * (buttonSize + spacing);

            Item item = items[Random.Range(0, items.Length)]; // �����ϰ� ������ ����

            GameObject itemGo = Instantiate(itemPrefab, itemContainer);

            ItemUI itemUI = itemGo.GetComponent<ItemUI>();

            itemUI.Setup(this, item);

            // ��ư�� ��ġ �� ũ�� ����
            RectTransform rectTransform = itemGo.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(buttonSize, buttonSize);
            rectTransform.anchoredPosition = new Vector2(colX, rowY);
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
