using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter
{
    private bool printSwitch = true;
    private bool timeSwitch = true;
    private float timeValue = -1;
    public Counter(int time)
    {
        SetTimeValue((float)(time * 60));
    }
    public int Update()
    {
        if (timeSwitch && timeValue > 0)
        {
            DisplayTime(timeValue);
            timeValue -= Time.deltaTime;
            return (int)GetTimeValue();
        }
        else
            return -1;
    }
    public void SetTimeSwitch(bool value)
    {
        timeSwitch = value;
    }
    public void SetTimeValue(float _timeValue)
    {
        timeValue = _timeValue;
    }
    public float GetTimeValue()
    {
        return timeValue;
    }
    public void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0) timeToDisplay = 0;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        if (seconds % 10 == 0 && printSwitch)
        {
            UnityEngine.Debug.Log("TIMER   -  " + string.Format("{0:00}:{1:00}", minutes, seconds));
            printSwitch = false;
        }
        if (seconds % 10 > 0) printSwitch = true;
    }
}
