using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public int ID;
    public string itemName;
    public string category;
    public string subCategory;
    public string fileName;
    public int attack;
    public int heal;
    public int satiety;
    public int batteryCharge;
    public int sightRange;
    public int capacity;
    public int deathRate;
    public int durability;
    public int weight;
    public GameObject prefab;
    public ItemData(int ID, string itemName, string category, string subCategory, string fileName, int attack, int heal, int satiety, int batteryCharge, int sightRange, int capacity, int deathRate, int durability, int weight)
    {
        this.ID = ID;
        this.itemName = itemName;
        this.category = category;
        this.subCategory = subCategory;
        this.fileName = fileName;
        this.attack = attack;
        this.heal = heal;
        this.satiety = satiety;
        this.batteryCharge = batteryCharge;
        this.sightRange = sightRange;
        this.capacity = capacity;
        this.deathRate = deathRate;
        this.durability = durability;
        this.weight = weight;
    }

    public void SetPrefab(GameObject prefab)
    {
        this.prefab = prefab;
    }
}
