using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//게임 내 데이 사이클을 관리하는 스크립트
public class DayCycle : MonoBehaviour
{
    //날짜 변수
    int year;
    int month;
    int day;
    
    //각 달마다 최대 일
    int[] maxDay = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

    //이벤트가 발생한 날짜
    int eventDay;

    //날짜를 표기할 텍스트
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

    //날짜를 업데이트 해주는 코루틴
    IEnumerator FlowDayRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); //현재 1초를 하루로 적용함

            day++;

            //7일이 지날때마다 이벤트 발생
            int daysSinceLastEvent = day - eventDay;
            int eventInterval = 7; // 7일 주기로 이벤트 발생

            if (daysSinceLastEvent >= 0)
            {
                if (daysSinceLastEvent == eventInterval)
                {
                    Debug.Log("이벤트 발생");
                    eventSystem.TriggerRandomEvent();
                    eventDay = day;
                }
                
            }
            else
            {
               if(maxDay[month-1] - eventDay + day == eventInterval)
               {
                    Debug.Log("이벤트 발생");
                    eventSystem.TriggerRandomEvent();
                    eventDay = day;
               }
            }

            //달, 해가 지나는 조건
            if (day > maxDay[month])
            {
                day = 1;
                month++;

                if (month > 12)
                {
                    month = 1;
                    year++;
                }

                //윤년에 따른 변경
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

    //날짜 업데이트 함수
    void UpdateDateText()
    {
        dateText.text = string.Format("{0:D4}-{1:D2}-{2:D2}", year, month, day); // 텍스트 업데이트
    }

    //윤년인지 확인하는 함수
    bool IsLeapYear(int year)
    {
        return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
    }
}
