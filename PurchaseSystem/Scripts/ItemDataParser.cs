using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataParser : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public TextAsset itemData;

    void Start()
    {
        ReadData();
    }

    public void ReadData()
    {
        string csvText = itemData.text;

        List<List<string>> csvData = ParseCSV(csvText);

        ParseItemData(csvData);
    }

    List<List<string>> ParseCSV(string csvText)
    {
        List<List<string>> parsedData = new List<List<string>>();

        string[] lines = csvText.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            string[] rowData = lines[i].Split(',');

            List<string> row = new List<string>();
            foreach (string cell in rowData)
            {
                row.Add(cell);
            }
            parsedData.Add(row);
        }

        return parsedData;
    }

    void ParseItemData(List<List<string>> csvData)
    {
        foreach (List<string> rowData in csvData)
        {
            if (rowData.Count >= 5) // 적어도 5개의 열이 있는지 확인
            {
                Item newItem = new Item();

                // 데이터 할당
                newItem.Item_ID = int.TryParse(rowData[0], out int id) ? id : 0;
                newItem.Item_Name = rowData[1];
                newItem.Item_Name_UI = rowData[2];
                newItem.Item_Icon = rowData[3];
                newItem.Item_Text = rowData[4];

                // 생성된 아이템 객체를 리스트에 추가
                items.Add(newItem);
            }
            else
            {
                Debug.LogWarning("CSV 데이터의 형식이 올바르지 않습니다. 데이터를 확인하세요.");
            }
        }
    }

    public Item[] List2Array()
    {
        return items.ToArray();
    }
}