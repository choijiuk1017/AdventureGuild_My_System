using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Text itemNameText;
    public Text priceText;
    public Text numText;

    private PurchaseSystem purchaseSystem;
    private Item item;
    private int initialItemCount;
    private int itemCount;

    public void Setup(PurchaseSystem system, Item newItem, int initialItemCount)
    {
        purchaseSystem = system;
        item = newItem;
        itemNameText.text = newItem.Item_Name;
        this.initialItemCount = initialItemCount;
        itemCount = initialItemCount;
        UpdateNumText();
    }

    public void IncreaseNumber()
    {
        if (itemCount < initialItemCount)
        {
            itemCount++;
            UpdateNumText();
        }
    }

    public void DecreaseNumber()
    {
        if (itemCount > 0) 
        {
            itemCount--;
            UpdateNumText();
        }
    }

    private void UpdateNumText()
    {
        numText.text = itemCount.ToString();
    }
}
