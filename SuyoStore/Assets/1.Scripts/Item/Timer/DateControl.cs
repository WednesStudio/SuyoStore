using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DateControl : MonoBehaviour
{
    public string dateInput;
    private TextMeshProUGUI text;
    private DateTime dateTime;
    private int days;
    void Awake()
    {
        days = 1;
        text = this.GetComponent<TextMeshProUGUI>();
        dateTime = DateTime.Parse(dateInput);
        text.text = dateTime.ToString("yyyy/MM/dd");
    }
    public void SetDate(string newDate)
    {
        text.text = newDate;
        ++days;
    }
    public string GetDate()
    {
        return text.text;
    }
    public int GetDays()
    {
        return days;
    }
}
