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
            if (rowData.Count >= 5) // ��� 5���� ���� �ִ��� Ȯ��
            {
                Item newItem = new Item();

                // ������ �Ҵ�
                newItem.Item_ID = int.TryParse(rowData[0], out int id) ? id : 0;
                newItem.Item_Name = rowData[1];
                newItem.Item_Name_UI = rowData[2];
                newItem.Item_Icon = rowData[3];
                newItem.Item_Text = rowData[4];

                // ������ ������ ��ü�� ����Ʈ�� �߰�
                items.Add(newItem);
            }
            else
            {
                Debug.LogWarning("CSV �������� ������ �ùٸ��� �ʽ��ϴ�. �����͸� Ȯ���ϼ���.");
            }
        }
    }

    public Item[] List2Array()
    {
        return items.ToArray();
    }
}