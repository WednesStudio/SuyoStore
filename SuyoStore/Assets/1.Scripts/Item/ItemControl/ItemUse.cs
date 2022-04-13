using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

public class ItemUse : MonoBehaviour
{
    [SerializeField] DataManager _dataManager;
    private PlayerTest player;
    private int[] attributes = new int[(int)Attributes.TOTAL];
<<<<<<< HEAD
<<<<<<< HEAD
    private const string battery = "보조배터리", food = "음식", weapon = "무기", pill = "치료제", flashLight = "라이트", sleepingBag = "침낭";
=======
    private const string battery = "보조배터리", food = "음식", weapon = "무기", pill = "치료제", flashLight = "라이트", sleepingBag = "침낭", bag="가방";
>>>>>>> f4ccbbe9 ([UPDATE] itemUse file)
=======
    private const string battery = "보조배터리", food = "음식", weapon = "무기", pill = "치료제", flashLight = "라이트", sleepingBag = "침낭", bag="가방";
>>>>>>> ba1e7674 ([BUG] merge error)
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
<<<<<<< HEAD
<<<<<<< HEAD
=======
            case bag:
                UseBag(attributes[(int)Attributes.CAPACITY]);
                break;
>>>>>>> f4ccbbe9 ([UPDATE] itemUse file)
=======
            case bag:
                UseBag(attributes[(int)Attributes.CAPACITY]);
                break;
>>>>>>> ba1e7674 ([BUG] merge error)
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
<<<<<<< HEAD
<<<<<<< HEAD
            
=======
=======
>>>>>>> ba1e7674 ([BUG] merge error)

    }
    private void ChangeDate()
    {

        DateControl dateControl = FindObjectOfType<DateControl>();
        System.DateTime result = System.DateTime.Parse(dateControl.GetDate());
        result = result.AddDays(1);
        dateControl.SetDate(result.ToString("yyyy/MM/dd"));
<<<<<<< HEAD
>>>>>>> f4ccbbe9 ([UPDATE] itemUse file)
=======
>>>>>>> ba1e7674 ([BUG] merge error)
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
<<<<<<< HEAD
<<<<<<< HEAD
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
=======
=======
>>>>>>> ba1e7674 ([BUG] merge error)
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
<<<<<<< HEAD
}
>>>>>>> f4ccbbe9 ([UPDATE] itemUse file)
=======
}
>>>>>>> ba1e7674 ([BUG] merge error)
