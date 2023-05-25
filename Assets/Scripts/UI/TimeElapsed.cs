using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeElapsed : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;

    string[] weekdays = {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"};
    string morning = "AM"; // maybe rename
    string night = "PM";

    int currentWeekdayIndex;
    string currentWeekday;
    string halfOfDay;
    int hour;
    int minute;
    string currentTime;

    int startingWeekdayIndex = 6;
    int startingHour = 11;
    int startingMinute = 50;
    int lastSecond = 0;


    void Awake()
    {
        currentWeekday = weekdays[startingWeekdayIndex];
        currentWeekdayIndex = startingWeekdayIndex;
        halfOfDay = night;
        hour = startingHour;
        minute = startingMinute;
        currentTime = FormatTime();
    }

    void Update()
    {
        CurrentTime();
    }

    void CurrentTime()
    {
        
        if (((int)Time.time) != lastSecond)
        {
            minute += 1;
            if (minute == 60)
            {
                hour = IncrementHour();
                minute = 0;
            }
        }

        FormatTime();

        lastSecond = ((int)Time.time);

        if (timeText == null)
        {
            return;
        }

        timeText.text = currentTime;
    }

    string FormatTime()
    {
        if (minute < 10)
        {
            currentTime = currentWeekday + " " + hour.ToString() + ":0" + minute.ToString() + " " + halfOfDay; // do this is another function
        }
        else
        {
            currentTime = currentWeekday + " " + hour.ToString() + ":" + minute.ToString() + " " + halfOfDay;
        }

        return currentTime;
    }

    int IncrementHour()
    {
        if (minute == 60)
        {
            if (hour == 11 )
            {
                hour = 12;
                IncrementHalfOfDay();
            }
            else if (hour == 12)
            {
                hour = 1;
            }
            else
            {
                hour += 1;
            }
            minute = 0;
        }
    return hour;
    }

    void IncrementHalfOfDay()
    {
        if (halfOfDay == morning)
        {
            halfOfDay = night;
        }
        else
        {
            halfOfDay = morning;
            IncrementDay();
        }
    }

    void IncrementDay()
    {
        if (currentWeekdayIndex == 6)
        {
            currentWeekday = weekdays[0];
        }
        else
        {
            currentWeekday = weekdays[currentWeekdayIndex + 1];
        }
    }
}
