using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

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
                break;
            }
        }
        item = new Item(itemName, attributes);
    }

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
