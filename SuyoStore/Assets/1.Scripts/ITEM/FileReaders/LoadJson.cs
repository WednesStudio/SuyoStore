using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadJson : MonoBehaviour
{
    private JsonData blankData;
    public List<JsonData> messageDatabase = new List<JsonData>();
    public void LoadMsgData()
    {
        messageDatabase.Clear();
        List<Dictionary<string, object>> data = JsonReader.Read("SampleText");
        for (int i = 0; i < data.Count; i++)
        {
            string date = data[i]["date"].ToString();
            string message = data[i]["message"].ToString();
            AddData(date, message);
        }
    }
    void AddData(string date, string message)
    {
        JsonData tempData = new JsonData(blankData);

        tempData.date = date;
        tempData.message = message;

        messageDatabase.Add(tempData);
    }
}
