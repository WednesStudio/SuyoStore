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
        List<Dictionary<string, object>> data = JsonReader.Read("Data/SampleText");
        for (int i = 0; i < data.Count; i++)
        {
            string days = data[i]["days"].ToString();
            string message = data[i]["message"].ToString();
            AddData(days, message);
        }
    }
    void AddData(string days, string message)
    {
        JsonData tempData = new JsonData(blankData);

        tempData.days = int.Parse(days);
        tempData.message = message;

        messageDatabase.Add(tempData);
    }
}
