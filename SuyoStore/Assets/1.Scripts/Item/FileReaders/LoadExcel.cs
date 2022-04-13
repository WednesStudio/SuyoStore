using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadExcel : MonoBehaviour
{
    private ItemData blankItem;
    public List<ItemData> itemDatabase = new List<ItemData>();

    public void LoadItemData()
    {
        // Clear database
        itemDatabase.Clear();

        // Read CSV files
        List<Dictionary<string, object>> data = CSVReader.Read("Data/itemDatabase");
        for (int i = 0; i < data.Count; i++)
        {
            string itemName = data[i]["itemName"].ToString();
            string category = data[i]["category"].ToString();
            string subCategory = data[i]["subCategory"].ToString();
            string fileName = data[i]["fileName"].ToString();
            int attack = int.Parse(data[i]["attack"].ToString(), System.Globalization.NumberStyles.Integer);
            int heal = int.Parse(data[i]["heal"].ToString(), System.Globalization.NumberStyles.Integer);
            int satiety = int.Parse(data[i]["satiety"].ToString(), System.Globalization.NumberStyles.Integer);
            int batteryCharge = int.Parse(data[i]["batteryCharge"].ToString(), System.Globalization.NumberStyles.Integer);
            int sightRange = int.Parse(data[i]["sightRange"].ToString(), System.Globalization.NumberStyles.Integer);
            int capacity = int.Parse(data[i]["capacity"].ToString(), System.Globalization.NumberStyles.Integer);
            int deathRate = int.Parse(data[i]["deathRate"].ToString(), System.Globalization.NumberStyles.Integer);
            int durability = int.Parse(data[i]["durability"].ToString(), System.Globalization.NumberStyles.Integer);
            int weight = int.Parse(data[i]["weight"].ToString(), System.Globalization.NumberStyles.Integer);
            GameObject prefab = (GameObject)Resources.Load("Models/" + fileName, typeof(GameObject));
            AddItem(itemName, category, subCategory, fileName, attack, heal, satiety, batteryCharge, sightRange, capacity, deathRate, durability, weight, prefab);
        }
    }

    void AddItem(string itemName, string category, string subCategory, string fileName, int attack, int heal, int satiety, int batteryCharge, int sightRange, int capacity, int deathRate, int durability, int weight, GameObject prefab)
    {
        ItemData tempItem = new ItemData(blankItem);

        tempItem.itemName = itemName;
        tempItem.category = category;
        tempItem.subCategory = subCategory;
        tempItem.fileName = fileName;
        tempItem.attack = attack;
        tempItem.heal = heal;
        tempItem.satiety = satiety;
        tempItem.batteryCharge = batteryCharge;
        tempItem.sightRange = sightRange;
        tempItem.capacity = capacity;
        tempItem.deathRate = deathRate;
        tempItem.durability = durability;
        tempItem.weight = weight;
        tempItem.prefab = prefab;

        itemDatabase.Add(tempItem);
    }
}
