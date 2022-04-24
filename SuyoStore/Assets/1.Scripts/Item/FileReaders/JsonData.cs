using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JsonData
{
    public int days;
    public string message;
    public JsonData(int _days, string _message)
    {
        days = _days;
        message = _message;
    }
}
[System.Serializable]
public class JsonList
{
    public JsonData[] info;
}

[System.Serializable]
public class JsonConditionData
{
    public int number;
    public string route;
    public string message;
    public string[] sniper;
    public string must;
    public int count;
    public string exit;
    public JsonConditionData()
    {
        number = 0;
        route = "";
        message = "";
        sniper = new string[] { };
        must = "";
        count = 0;
        exit = "";
    }
    public JsonConditionData(int n, string r, string msg, string[] s, string m, int c, string e)
    {
        number = n;
        route = r;
        message = msg;
        sniper = s;
        must = m;
        count = c;
        exit = e;
    }
    public int GetNumber() => number;
}
[System.Serializable]
public class JsonConditionList
{
    public JsonConditionData[] description;
}