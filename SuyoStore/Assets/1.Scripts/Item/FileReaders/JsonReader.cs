using System.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class JsonReader
{
    public static List<Dictionary<string, object>> Read(string file)
    {
        TextAsset textJSON = Resources.Load(file) as TextAsset;
        JsonList infoList = new JsonList();
        infoList = JsonUtility.FromJson<JsonList>(textJSON.text);

        var list = new List<Dictionary<string, object>>();
        FieldInfo[] props = typeof(JsonData).GetFields();
        for (int i = 0; i < infoList.info.Length; i++)
        {
            var entry = new Dictionary<string, object>();
            for (int j = 0; j < props.Length; j++)
            {
                string header = props[j].Name;
                object value = props[j].GetValue(infoList.info[i]);
                entry[header] = value;
            }
            list.Add(entry);
        }
        return list;
    }
    public static List<Dictionary<string, object>> ReadCondition(string file)
    {
        TextAsset textJSON = Resources.Load(file) as TextAsset;
        JsonConditionList descList = new JsonConditionList();
        descList = JsonUtility.FromJson<JsonConditionList>(textJSON.text);

        var list = new List<Dictionary<string, object>>();
        FieldInfo[] props = typeof(JsonConditionData).GetFields();
        for (int i = 0; i < descList.description.Length; i++)
        {
            var entry = new Dictionary<string, object>();
            for (int j = 0; j < props.Length; j++)
            {
                string header = props[j].Name;
                object value = props[j].GetValue(descList.description[i]);
                entry[header] = value;
            }
            list.Add(entry);
        }
        return list;
    }
}
