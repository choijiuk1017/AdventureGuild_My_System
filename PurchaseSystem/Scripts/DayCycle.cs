using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayCycle : MonoBehaviour
{
    int year;
    int month;
    int day;
    int eventDay;
    int[] maxDay = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

    public Text dateText;

    public EventSystem eventSystem;

    private void Start()
    {
        year = 2024;
        month = 1;
        day = 1;
        eventDay = day;
        UpdateDateText();
        StartCoroutine(FlowDayRoutine()) ;
    }
    IEnumerator FlowDayRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            day++;

            if(day - eventDay == 7)
            {
                eventSystem.TriggerRandomEvent();
                eventDay = day;
            }

            if (day > maxDay[month])
            {
                day = 1;
                month++;
                if (month > 12)
                {
                    month = 1;
                    year++;
                }

                // Check for leap year
                if (IsLeapYear(year))
                {
                    maxDay[2] = 29;
                }
                else
                {
                    maxDay[2] = 28;
                }
            }


            UpdateDateText();
        }
    }
    void UpdateDateText()
    {
        dateText.text = string.Format("{0:D4}-{1:D2}-{2:D2}", year, month, day); // 텍스트 업데이트
    }

    bool IsLeapYear(int year)
    {
        return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
    }
}
