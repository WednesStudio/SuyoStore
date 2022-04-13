<<<<<<< HEAD
=======
using System.Diagnostics;
>>>>>>> ba1e7674 ([BUG] merge error)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;
<<<<<<< HEAD

public class ItemControl : MonoBehaviour
{   [SerializeField] DataManager _dataManager;
    private LoadExcel _database;
    private PlayerTest player;
    [System.NonSerialized]
    public Item item;
    private string itemName;
    private int itemID;
    private int[] attributes = new int[(int)Attributes.TOTAL];
    
    private void Start()
    {
        //player = GetPlayerComponent();
        _database = _dataManager.GetComponent<LoadExcel>();
        string name = this.name.Split()[0];
        for (int i = 0; i < _database.itemDatabase.Count; i++)
        {
            if (name == _database.itemDatabase[i].fileName)
            {
                itemID = _database.itemDatabase[i].ID;
                itemName = _database.itemDatabase[i].subCategory;
                attributes[(int)Attributes.ATTACK] = _database.itemDatabase[i].attack;
                attributes[(int)Attributes.HEAL] = _database.itemDatabase[i].heal;
                attributes[(int)Attributes.SATIETY] = _database.itemDatabase[i].satiety;
                attributes[(int)Attributes.BATTERYCHARGE] = _database.itemDatabase[i].batteryCharge;
                attributes[(int)Attributes.SIGHTRANGE] = _database.itemDatabase[i].sightRange;
                attributes[(int)Attributes.CAPACITY] = _database.itemDatabase[i].capacity;
                attributes[(int)Attributes.DEATHRATE] = _database.itemDatabase[i].deathRate;
                attributes[(int)Attributes.DURABILITY] = _database.itemDatabase[i].durability;
                attributes[(int)Attributes.WEIGHT] = _database.itemDatabase[i].weight;
=======
using TMPro;

public class ItemControl : MonoBehaviour
{
    private PlayerTest player;
    private LoadExcel database;

    [System.NonSerialized]
    public Item item;
    private string itemName;
    private int[] attributes = new int[(int)Attributes.TOTAL];
    private const string battery = "보조배터리", food = "음식", weapon = "무기", pill = "치료제", flashLight = "라이트", sleepingBag = "침낭", bag = "가방";

    private void Start()
    {
        player = GetPlayerComponent();
        database = FindObjectOfType<LoadExcel>();
        string name = this.name.Split()[0];
        for (int i = 0; i < database.itemDatabase.Count; i++)
        {
            if (name == database.itemDatabase[i].fileName)
            {
                itemName = database.itemDatabase[i].subCategory;
                attributes[(int)Attributes.ATTACK] = database.itemDatabase[i].attack;
                attributes[(int)Attributes.HEAL] = database.itemDatabase[i].heal;
                attributes[(int)Attributes.SATIETY] = database.itemDatabase[i].satiety;
                attributes[(int)Attributes.BATTERYCHARGE] = database.itemDatabase[i].batteryCharge;
                attributes[(int)Attributes.SIGHTRANGE] = database.itemDatabase[i].sightRange;
                attributes[(int)Attributes.CAPACITY] = database.itemDatabase[i].capacity;
                attributes[(int)Attributes.DEATHRATE] = database.itemDatabase[i].deathRate;
                attributes[(int)Attributes.DURABILITY] = database.itemDatabase[i].durability;
                attributes[(int)Attributes.WEIGHT] = database.itemDatabase[i].weight;
>>>>>>> ba1e7674 ([BUG] merge error)
                break;
            }
        }
        item = new Item(itemName, attributes);
    }
<<<<<<< HEAD
<<<<<<< HEAD

    public void GetThisItem()
    {
        GameManager.GM.AddItem(itemID, 1);
        print("get this item! " + itemID);
    }

    public int GetItemID()
    {
        return itemID;
    }
}
=======
}
>>>>>>> f4ccbbe9 ([UPDATE] itemUse file)
=======
}
>>>>>>> ba1e7674 ([BUG] merge error)
