using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JsonData
{
<<<<<<< HEAD
    public string date;
    public string message;
    public JsonData(JsonData d)
    {
        date = d.date;
=======
    public int days;
    public string message;
    public JsonData(JsonData d)
    {
        days = d.days;
>>>>>>> ba1e7674 ([BUG] merge error)
        message = d.message;
    }
}
[System.Serializable]
public class JsonList
{
    public JsonData[] info;
}
