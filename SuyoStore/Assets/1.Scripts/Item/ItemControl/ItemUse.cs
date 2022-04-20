using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

public class ItemUse : MonoBehaviour
{
    [SerializeField] DataManager _dataManager;
    [SerializeField] UIManager _uiManager;
    
    private GameObject player;
    //private PlayerController player;
    private PlayerStatus playerStatus;

    private LightControl lightControl;
    private const string battery = "보조배터리", food = "음식", weapon = "무기", pill = "치료제", flashLight = "라이트", sleepingBag = "침낭", bag = "가방", smartPhone = "스마트폰";
    private Dictionary<int, Item> MyUsedItem = new Dictionary<int, Item>();
    public int GetItemDurability(int id) => MyUsedItem[id].GetDURABILITY();

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStatus = player.GetComponent<PlayerStatus>();
    }
    
    private void Update()
    {
        if (lightControl != null)
        {
            if (lightControl.LightDurability())
                DestroyObject(lightControl.GetID());
        }
    }

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
                UseBattery(item.GetBATTERYCHARGE(), itemID);
                break;
            case food:
                UseFood(item.GetSATIETY());
                break;
            case weapon:
                UseWeapon(item.GetATTACK(), itemID);
                break;
            case pill:
                UseHeal(item.GetHEAL());
                break;
            case flashLight:
                UseLight(item, itemID);
                break;
            case sleepingBag:
                UseSleepingBag(item, itemID);
                break;
            case bag:
                break;
            case smartPhone:
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
                if(_dataManager.IsContainItem(itemID))  _dataManager.AddItem(itemID, -1);
                MyUsedItem.Remove(itemID);
            }
        }
    }

    public void ChangeItem(int currentItemID, int newItemID)
    {
        //현재거를 삭제
        if(currentItemID != -1)
        {
            int[] arr = {0,0,0,0,0,0,0,0,0,0};
            Item temp = new Item(MyUsedItem[currentItemID].GetItemName(), arr);
            print(MyUsedItem[currentItemID].GetItemName());
            _uiManager.SetCurrentItemStatus(-1, temp);
            _dataManager.AddItem(currentItemID, 1);
        }

        //새로운 거 장착
        if(newItemID != -1)    _uiManager.SetCurrentItemStatus(newItemID, MyUsedItem[newItemID]);

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
    private void UseBattery(int charge, int itemID)
    {
        CellPhoneControl cellphone = GetCellPhoneComponent();
        cellphone.PhoneCharge(charge);

        //다 쓰면 바닥에 instantiate
        GameObject model = GameObject.Instantiate(_dataManager.GetItemModel(itemID), Vector3.zero, Quaternion.identity);
    }
    private void UseSleepingBag(Item item, int itemID)
    {
        //쓰려면 instantiate
        GameObject model = Instantiate(_dataManager.GetItemModel(itemID), Vector3.zero, Quaternion.identity);
        
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
        playerStatus.RecoverStatus(Status.eCurStatusType.cSatiety, satiety);
        //int satietyMax = playerStatus.MaxSatiety;
        //playerStatus.CurSatiety = playerStatus.CurSatiety + satiety > satietyMax ? satietyMax : playerStatus.CurSatiety + satiety;
        UnityEngine.Debug.Log("satiety " + playerStatus.CurSatiety);
    }
    private void UseWeapon(int attack, int itemID)
    {
        //만약 플레이어에게 이미 장착되어 있는 무기가 있다면..
        //if(player)
        //ChangeItem(player.list.item....., itemID);
        //else
        ChangeItem(-1,itemID);
        //무기 휘두를 때 효과
        UnityEngine.Debug.Log("attack " + player.attack);
        // 휘두를 때마다 내구도 마이나스 - 휘두르는 키에서 바로 useItem으로..
        int attackMax = playerStatus.Attack;
        playerStatus.CurAttack = playerStatus.CurAttack + attack > attackMax ? attackMax : playerStatus.CurAttack + attack;
        UnityEngine.Debug.Log("attack " + playerStatus.CurAttack);
        // 휘두를 때마다 내구도 마이나스
    }
    private void UseHeal(int heal)
    {
        playerStatus.RecoverStatus(Status.eCurStatusType.cHp, heal);


        //int hpMax = playerStatus.MaxHp;
        //playerStatus.CurHp = playerStatus.CurHp + heal > hpMax ? hpMax : playerStatus.CurHp + heal;
        UnityEngine.Debug.Log("HP " + playerStatus.CurHp);
    }
    private void UseLight(Item item, int itemID)
    {
        print("uselight");
        // player.sightRange = item.GetSIGHTRANGE();
        // UnityEngine.Debug.Log("sightRange " + player.sightRange);
        // 켜져 있는 상태라면 지속적으로 내구도가 감소해야 함.....
        // lightControl = new LightControl(item.GetDURABILITY(), itemID);
        
        //라이트 기존에서 교체
        //if(player)
        //ChangeItem(player.list.item....., itemID);
        ChangeItem(-1,itemID);
        lightControl = new LightControl(2, itemID);

    }


  
    private void GameOver()
    {
        UnityEngine.Debug.Log("GameOver");
    }
}