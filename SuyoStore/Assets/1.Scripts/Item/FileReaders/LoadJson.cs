using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoadJson : MonoBehaviour
{
    public List<JsonData> messageDatabase = new List<JsonData>();
    public JsonConditionData conditionList;
    public void LoadMsgData(string file)
    {
        messageDatabase.Clear();
        List<Dictionary<string, object>> data = JsonReader.Read(file);
        for (int i = 0; i < data.Count; i++)
        {
            string days = data[i]["days"].ToString();
            string message = data[i]["message"].ToString();
            AddData(days, message);
        }
    }
    public void LoadConditionData(string file)
    {
        Dictionary<string, object> data = JsonReader.ReadCondition(file)[0];
        int number = int.Parse(data["number"].ToString(), System.Globalization.NumberStyles.Integer);
        string[] sniper = ((IEnumerable)data["sniper"])
                            .Cast<object>()
                            .Select(x => x.ToString())
                            .ToArray();
        string must = data["must"].ToString();
        int count = int.Parse(data["count"].ToString(), System.Globalization.NumberStyles.Integer);
        string exit = data["exit"].ToString();
        AddConditionData(number, sniper, must, count, exit);
    }
    void AddData(string days, string message)
    {
        int _days = int.Parse(days);
        string _message = message;
        JsonData newJsonData = new JsonData(_days, _message);
        messageDatabase.Add(newJsonData);
    }
    void AddConditionData(int n, string[] s, string m, int c, string e)
    {
        conditionList = new JsonConditionData(n, s, m, c, e);
    }
}
