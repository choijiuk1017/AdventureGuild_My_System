using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Text itemNameText;
    public Text priceText;

    private PurchaseSystem purchaseSystem;
    private Item item;

    public void Setup(PurchaseSystem system, Item newItem)
    {
        purchaseSystem = system;
        item = newItem;

        itemNameText.text = newItem.Item_Name;
    }

    public void OnClick()
    {
        purchaseSystem.PurchaseItem(item);
    }
}
