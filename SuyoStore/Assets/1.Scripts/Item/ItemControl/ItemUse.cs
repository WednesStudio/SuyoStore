using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

public class ItemUse : MonoBehaviour
{
    [SerializeField] DataManager _dataManager;
    private PlayerTest player;
    private int[] attributes = new int[(int)Attributes.TOTAL];
    private const string battery = "보조배터리", food = "음식", weapon = "무기", pill = "치료제", flashLight = "라이트", sleepingBag = "침낭";
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
        {
            Destroy(this.gameObject);
            _dataManager.AddItem(item.ID, -1);
        }
            
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
