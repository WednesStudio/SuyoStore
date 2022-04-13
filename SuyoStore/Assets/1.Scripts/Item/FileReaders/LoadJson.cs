using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadJson : MonoBehaviour
{
    private JsonData blankData;
    public List<JsonData> messageDatabase = new List<JsonData>();
<<<<<<< HEAD
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
=======
    private string[] routes = { "route1.json", "route2.json", "route3.json" };
    private string directory = "Data/";
    public void LoadMsgData()
    {
        // int rnd = Random.Range(0, 3);
        // string file = directory + routes[rnd];
        // print(file);
        messageDatabase.Clear();
        List<Dictionary<string, object>> data = JsonReader.Read("Data/route1");
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
>>>>>>> ba1e7674 ([BUG] merge error)
        tempData.message = message;

        messageDatabase.Add(tempData);
    }
}
