using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeElapsed : MonoBehaviour
{
    string morning = "AM"; // maybe rename
    string afternoon = "PM";
    int hour = 12;
    int minute = 0;
    string halfOfDay;
    string currentTime;
    bool atEndOfMinute = false;

    void Awake()
    {
        halfOfDay = morning;
        currentTime = hour.ToString() + ":" + minute.ToString() + " " + halfOfDay;
    }

    void Update()
    {
        CurrentTime();
    }

    void CurrentTime()
    {
        IncrementMinute();
        
        if (atEndOfMinute)
        {
            IncrementHour();
        }
        
        else if (minute < 10)
        {
            currentTime = hour.ToString() + ":0" + minute.ToString() + " " + halfOfDay;
        }
        else
        {
            currentTime = hour.ToString() + ":" + minute.ToString() + " " + halfOfDay;
        }
        
        Debug.Log(currentTime);
    }

    void IncrementHour()
    {
        if (hour != 12)
        {
            hour += 1;
        }
        else
        {
            hour = 1;
            IncrementHalfOfDay();
        }
        atEndOfMinute = false;
    }

    void IncrementMinute()
    {
        minute = ((int)(Time.time)) % 60; // need to reset to 0

        if (minute == 59)
        {
            atEndOfMinute = true;
        }
    }

    void IncrementHalfOfDay()
    {
        if (halfOfDay == morning)
        {
            halfOfDay = afternoon;
        }
        else
        {
            halfOfDay = morning;
        }
    }
}
