using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

public class ItemUse : MonoBehaviour
{
    [SerializeField] DataManager _dataManager;
    private PlayerTest player;
    private int[] attributes = new int[(int)Attributes.TOTAL];
    private const string battery = "보조배터리", food = "음식", weapon = "무기", pill = "치료제", flashLight = "라이트", sleepingBag = "침낭", bag="가방";
    private Dictionary<int, Item> MyUsedItem = new Dictionary<int, Item>();

    public void UseItem(int itemID)
    {
        Item item;
        if(MyUsedItem.ContainsKey(itemID))
        {
            item = MyUsedItem[itemID];
        }
        else
        {
            item = _dataManager.SetNewItem(itemID);
            MyUsedItem.Add(itemID, item);
        }
        
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
            case bag:
                UseBag(attributes[(int)Attributes.CAPACITY]);
                break;
            default:
                Debug.Log("itemName doesn't exist in UseItem");
                break;
        }
        item.attributes[(int)Attributes.DURABILITY] -= 1;
        if (item.attributes[(int)Attributes.DURABILITY] == 0)
        {
            //Destroy(this.gameObject);
            GameObject[] myItems = GameObject.FindGameObjectsWithTag("UsedItem");
            foreach(GameObject i in myItems)
            {
                if(i.name == _dataManager.GetItemName(itemID))
                {
                    Destroy(i);
                    _dataManager.AddItem(item.ID, -1);
                    MyUsedItem.Remove(itemID);
                }
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
    }
    private void UseHeal(int heal)
    {
        int hpMax = 100;
        player.HP = player.HP + heal > hpMax ? hpMax : player.HP + heal;
        UnityEngine.Debug.Log("HP " + player.HP);
    }
    private void UseLight(int light)
    {
        player.sightRange = light;
        UnityEngine.Debug.Log("sightRange " + player.sightRange);
        // 켜져 있는 상태라면 지속적으로 내구도가 감소해야 함.....
    }
    private void UseBag(int capacity)
    {
        BagControl bagControl = FindObjectOfType<BagControl>();
        bagControl.SetCapacity(capacity);
    }
    private void GameOver()
    {
        UnityEngine.Debug.Log("GameOver");
    }
}