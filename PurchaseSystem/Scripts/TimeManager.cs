using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance; // Singleton instance

    public int currentDay = 1;
    public int currentMonth = 1;
    public int currentYear = 2024;

    private float dayLengthInSeconds = 6.0f; // 10분을 하루로 쳤을 때의 시간(초)

    private float elapsedTime = 0.0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Update elapsed time
        elapsedTime += Time.deltaTime;

        // Check if a day has passed
        if (elapsedTime >= dayLengthInSeconds)
        {
            elapsedTime -= dayLengthInSeconds; // Reset elapsed time
            AdvanceDay();
        }
    }

    private void AdvanceDay()
    {
        currentDay++;
        // You can implement logic to handle month and year transitions here
        Debug.Log("New Day: " + currentDay + "/" + currentMonth + "/" + currentYear);
    }
}
