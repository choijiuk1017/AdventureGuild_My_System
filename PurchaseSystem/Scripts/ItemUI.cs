using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Text itemNameText;
    public Text priceText;
    public Text numText;
    public Text marketPrice;

    public Button rejectButton;
    public Color activeColor = Color.gray;
    public Color inactiveColor = Color.red;
    public bool isOn = false;
    public int itemCount;

    public Item item;

    private PurchaseSystem purchaseSystem;
    
    private int initialItemCount;
    
    


    private void Start()
    {
        RejectButtonClick();
    }


    public void Setup(PurchaseSystem system, Item newItem, int initialItemCount)
    {
        purchaseSystem = system;
        item = newItem;
        itemNameText.text = newItem.Item_Name;
        marketPrice.text = newItem.Item_Price_Def.ToString();
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

    public void RejectButtonClick()
    {
        isOn = !isOn;

        if(!isOn)
        {
            rejectButton.image.color = activeColor;
        }
        else
        {
            rejectButton.image.color = inactiveColor;
        }
    }


    private void UpdateNumText()
    {
        numText.text = itemCount.ToString();
    }
}
