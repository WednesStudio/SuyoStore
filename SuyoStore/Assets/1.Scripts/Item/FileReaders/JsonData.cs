using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JsonData
{
    public string date;
    public string message;
    public JsonData(JsonData d)
    {
        date = d.date;
        message = d.message;
    }
}
[System.Serializable]
public class JsonList
{
    public JsonData[] info;
}
