using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//아이템 창의 UI에 대해 관리하는 스크립트
public class ItemUI : MonoBehaviour
{
    //아이템 창을 이루는 UI들
    public Text itemNameText;
    public Text priceText;
    public Text numText;
    public Text marketPrice;
    public Text purchasePrice;
    public Button rejectButton;
    public InputField priceInputField;

    //매입 거부 버튼 색상
    public Color activeColor = Color.gray;
    public Color inactiveColor = Color.red;

    //매입 거부 버튼 활성화 여부
    public bool isOn = false;

    //아이템의 개수
    public int itemCount;

    //아이템의 초기 개수 저장 변수, 개수 변동때 쓰기 위함
    private int initialItemCount;

    public Item item;

    private PurchaseSystem purchaseSystem;
    

    private void Start()
    {
        RejectButtonClick();
    }

    //아이템 UI를 세팅해주는 함수, PurchaseSystem 스크립트에서 불러서 세팅함.
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

    //아래 세 함수는 UI 버튼 클릭에서 불러옴

    //아이템 개수 증가 함수
    public void IncreaseNumber()
    {
        if (itemCount < initialItemCount)
        {
            itemCount++;
            UpdateNumText();
        }
    }

    //아이템 개수 감소 함수
    public void DecreaseNumber()
    {
        if (itemCount > 0) 
        {
            itemCount--;
            UpdateNumText();
        }
    }

    //매입 거부 버튼 함수
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



    //아이템 개수를 표기해주는 함수
    private void UpdateNumText()
    {
        numText.text = itemCount.ToString();
    }

    //아이템 가격을 표기해주는 함수, PurchaseSystem 스크립트에서 사용함
    public void UpdatePrice(float newPrice)
    {
        priceText.text = newPrice.ToString();
    }

    //매입가 수정 칸에 작성하는 함수
    public void UpdatePriceFromInput()
    {
        if (!string.IsNullOrEmpty(priceInputField.text))
        {
            float newPrice;
            if (float.TryParse(priceInputField.text, out newPrice))
            {
                purchasePrice.text = newPrice.ToString();
            }
            else
            {
                Debug.LogError("Invalid price input!");
            }
        }
    }
}
