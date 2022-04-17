using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

public class ItemUse : MonoBehaviour
{
    [SerializeField] DataManager _dataManager;
    private PlayerTest player;
    private LightControl lightControl;
    private const string battery = "보조배터리", food = "음식", weapon = "무기", pill = "치료제", flashLight = "라이트", sleepingBag = "침낭", bag = "가방";
    private Dictionary<int, Item> MyUsedItem = new Dictionary<int, Item>();
    public void UseItem(int itemID)
    {
        Item item;
        if (MyUsedItem.ContainsKey(itemID))
        {
            item = MyUsedItem[itemID];
        }
        else
        {
            item = _dataManager.SetNewItem(itemID);
            MyUsedItem.Add(itemID, item);
        }
        switch (item.GetItemName())
        {
            case battery:
                UseBattery(item.GetBATTERYCHARGE());
                break;
            case food:
                UseFood(item.GetSATIETY());
                break;
            case weapon:
                UseWeapon(item.GetATTACK());
                break;
            case pill:
                UseHeal(item.GetHEAL());
                break;
            case flashLight:
                UseLight(item, itemID);
                break;
            case sleepingBag:
                UseSleepingBag(item);
                break;
            case bag:
                break;
            default:
                Debug.Log("itemName doesn't exist in UseItem");
                break;
        }
        item.SetDURABILITY(-1);
        if (item.GetDURABILITY() == 0)
            DestroyObject(itemID);
    }
    private void DestroyObject(int itemID)
    {
        GameObject[] myItems = GameObject.FindGameObjectsWithTag("UsedItem");
        foreach (GameObject i in myItems)
        {
            if (i.name == _dataManager.GetItemName(itemID))
            {
                Destroy(i);
                _dataManager.AddItem(itemID, -1);
                MyUsedItem.Remove(itemID);
            }
        }
    }
    private void ChangeDate()
    {

        DateControl dateControl = FindObjectOfType<DateControl>();
        System.DateTime result = System.DateTime.Parse(dateControl.GetDate());
        result = result.AddDays(1);
        dateControl.SetDate(result.ToString("yyyy/MM/dd"));
    }
    private CellPhoneControl GetCellPhoneComponent()
    {
        return GameObject.Find("SM_Item_SmartPhone_01").GetComponent<CellPhoneControl>();
    }
    private PlayerTest GetPlayerComponent()
    {
        return GameObject.Find("player").GetComponent<PlayerTest>();
    }
    private void UseBattery(int charge)
    {
        CellPhoneControl cellphone = GetCellPhoneComponent();
        cellphone.PhoneCharge(charge);
    }
    private void UseSleepingBag(Item item)
    {
        int rnd = Random.Range(0, 100);
        int rate = item.GetDEATHRATE();
        Debug.Log("random " + rnd + " " + rate);
        if (rnd < rate)
        {
            GameOver();
            return;
        }
        UseHeal(item.GetHEAL());
        UseFood(item.GetSATIETY());
        CellPhoneControl cellphone = GetCellPhoneComponent();
        cellphone.PhoneUse();
        ChangeDate();
    }
    private void UseFood(int satiety)
    {
        int satietyMax = 100;
        player.satiety = player.satiety + satiety > satietyMax ? satietyMax : player.satiety + satiety;
        UnityEngine.Debug.Log("satiety " + player.satiety);
    }
    private void UseWeapon(int attack)
    {
        int attackMax = 100;
        player.attack = player.attack + attack > attackMax ? attackMax : player.attack + attack;
        UnityEngine.Debug.Log("attack " + player.attack);
        // 휘두를 때마다 내구도 마이나스
    }
    private void UseHeal(int heal)
    {
        int hpMax = 100;
        player.HP = player.HP + heal > hpMax ? hpMax : player.HP + heal;
        UnityEngine.Debug.Log("HP " + player.HP);
    }
    private void UseLight(Item item, int itemID)
    {
        print("uselight");
        // player.sightRange = item.GetSIGHTRANGE();
        // UnityEngine.Debug.Log("sightRange " + player.sightRange);
        // 켜져 있는 상태라면 지속적으로 내구도가 감소해야 함.....
        // lightControl = new LightControl(item.GetDURABILITY(), itemID);
        lightControl = new LightControl(2, itemID);

    }
    private void Update()
    {
        if(lightControl != null)
        {
            if (lightControl.LightDurability())
                DestroyObject(lightControl.GetID());
        }
        
    }
    private void GameOver()
    {
        UnityEngine.Debug.Log("GameOver");
    }
}