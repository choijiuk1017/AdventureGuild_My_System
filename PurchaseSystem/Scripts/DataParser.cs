using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataParser : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public List<Event> events = new List<Event>();

    public TextAsset itemData;
    public TextAsset eventData;

    void Start()
    {
        ReadData();
    }

    public void ReadData()
    {
        string itemCsvText = itemData.text;
        string eventCsvText = eventData.text;

        List<List<string>> itemCsvData = ParseCSV(itemCsvText);
        List<List<string>> eventCsvData = ParseCSV(eventCsvText);

        ParseItemData(itemCsvData);
        ParseEventData(eventCsvData);
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
                newItem.Item_Price_Def = int.TryParse(rowData[5], out int defPrice) ? defPrice : 0;
                newItem.Item_Price_Min = int.TryParse(rowData[6], out int minPrice) ? minPrice : 0;
                newItem.Item_Price_Max = int.TryParse(rowData[7], out int maxPrice) ? maxPrice : 0;
                

                // 생성된 아이템 객체를 리스트에 추가
                items.Add(newItem);
            }
            else
            {
                Debug.LogWarning("CSV 데이터의 형식이 올바르지 않습니다. 데이터를 확인하세요.");
            }
        }
    }

    void ParseEventData(List<List<string>> csvData)
    {
        foreach (List<string> rowData in csvData)
        {
            if (rowData.Count >= 5) // 적어도 5개의 열이 있는지 확인
            {
                Event newEvent = new Event();

                // 데이터 할당
                newEvent.Event_ID = int.TryParse(rowData[0], out int id) ? id : 0;
                newEvent.Event_Name = rowData[1];
                newEvent.Event_Main_Type = int.TryParse(rowData[2], out int mainType) ? mainType : 0;
                newEvent.Event_Sub_Type = int.TryParse(rowData[3], out int subType) ? subType : 0;
                newEvent.Event_Detail_Type = int.TryParse(rowData[4], out int detailType) ? detailType : 0;
                newEvent.Event_Script = rowData[5];
                newEvent.Event_Precede = int.TryParse(rowData[4], out int precede) ? precede : 0;
                newEvent.Event_Prob = int.TryParse(rowData[4], out int prob) ? prob : 0;


                // 생성된 아이템 객체를 리스트에 추가
                events.Add(newEvent);
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