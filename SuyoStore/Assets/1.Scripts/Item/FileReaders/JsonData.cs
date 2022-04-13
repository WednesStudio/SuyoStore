using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JsonData
{
    public int days;
    public string message;
    public JsonData(JsonData d)
    {
        days = d.days;
        message = d.message;
    }
}
[System.Serializable]
public class JsonList
{
    public JsonData[] info;
}
