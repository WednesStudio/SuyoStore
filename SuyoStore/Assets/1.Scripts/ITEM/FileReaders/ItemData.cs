using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
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
    public ItemData(ItemData d)
    {
        itemName = d.itemName;
        category = d.category;
        subCategory = d.subCategory;
        fileName = d.fileName;
        attack = d.attack;
        heal = d.heal;
        satiety = d.satiety;
        batteryCharge = d.batteryCharge;
        sightRange = d.sightRange;
        capacity = d.capacity;
        deathRate = d.deathRate;
        durability = d.durability;
        weight = d.weight;
    }
}
