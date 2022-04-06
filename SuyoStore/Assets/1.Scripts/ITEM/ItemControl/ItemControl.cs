using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

public class ItemControl : MonoBehaviour
{
    private PlayerTest player;
    private LoadExcel database;
    [System.NonSerialized]
    public Item item;
    private string itemName;
    private int[] attributes = new int[(int)Attributes.TOTAL];
    private const string battery = "보조배터리", food = "음식", weapon = "무기", pill = "치료제", flashLight = "라이트", sleepingBag = "침낭";

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
                break;
            }
        }
        item = new Item(itemName, attributes);
        UseItem(item);
    }
    public void UseItem(Item item)
    {
        Debug.Log(item.itemName);
        switch (item.itemName)
        {
            case battery:
                UseBattery(item.attributes[(int)Attributes.BATTERYCHARGE]);
                break;
            case food:
                UseFood(item.attributes[(int)Attributes.SATIETY]);
                break;
            case weapon:
                UseWeapon(item.attributes[(int)Attributes.ATTACK]);
                break;
            case pill:
                UseHeal(item.attributes[(int)Attributes.HEAL]);
                break;
            case flashLight:
                UseLight(item.attributes[(int)Attributes.SIGHTRANGE]);
                break;
            case sleepingBag:
                UseSleepingBag(item.attributes[(int)Attributes.HEAL], attributes[(int)Attributes.SATIETY]);
                break;
            default:
                Debug.Log("itemName doesn't exist in UseItem");
                break;
        }
        item.attributes[(int)Attributes.DURABILITY] -= 1;
        if (item.attributes[(int)Attributes.DURABILITY] == 0)
            Destroy(this.gameObject);
    }
    private CellPhoneControl GetCellPhoneComponent()
    {
        return GameObject.Find("SM_Item_SmartPhone_01").GetComponent<CellPhoneControl>();
    }
    private PlayerTest GetPlayerComponent()
    {
        return GameObject.Find("player").GetComponent<PlayerTest>();
    }
    private void UseBattery(int amount)
    {
        CellPhoneControl cellphone = GetCellPhoneComponent();
        cellphone.PhoneCharge(amount);
    }
    private void UseSleepingBag(int heal, int satiety)
    {
        int rnd = Random.Range(0, 100);
        int rate = (attributes[(int)Attributes.DEATHRATE]);
        Debug.Log("random " + rnd + " " + rate);
        if (rnd < rate)
        {
            GameOver();
            return;
        }
        UseHeal(heal);
        UseFood(satiety);
        CellPhoneControl cellphone = GetCellPhoneComponent();
        cellphone.PhoneUse();
        // change date
    }
    private void UseFood(int amount)
    {
        int satietyMax = 100;
        player.satiety = player.satiety + amount > satietyMax ? satietyMax : player.satiety + amount;
        Debug.Log("satiety " + player.satiety);
    }
    private void UseWeapon(int amount)
    {
        int attackMax = 100;
        player.attack = player.attack + amount > attackMax ? attackMax : player.attack + amount;
        Debug.Log("attack " + player.attack);
    }
    private void UseHeal(int amount)
    {
        int hpMax = 100;
        player.HP = player.HP + amount > hpMax ? hpMax : player.HP + amount;
        Debug.Log("HP " + player.HP);
    }
    private void UseLight(int amount)
    {
        player.sightRange = amount;
        Debug.Log("sightRange " + player.sightRange);
    }
    private void GameOver()
    {
        Debug.Log("GameOver");
    }
}
