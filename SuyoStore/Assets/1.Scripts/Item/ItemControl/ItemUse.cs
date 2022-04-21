using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

public class ItemUse : MonoBehaviour
{
    [SerializeField] DataManager _dataManager;
    [SerializeField] UIManager _uiManager;
    
    private GameObject player;
    private PlayerStatus playerStatus;

    private LightControl lightControl;
    private const string battery = "보조배터리", food = "음식", weapon = "무기", pill = "치료제", flashLight = "라이트", sleepingBag = "침낭", bag = "가방", smartPhone = "스마트폰";
    private Dictionary<int, Item> MyUsedItem = new Dictionary<int, Item>();
    public int GetItemDurability(int id) => MyUsedItem[id].GetDURABILITY();
    private Item item;

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
                DestroyObject(0, lightControl.GetID());
        }
    }

    public void UseItem(int itemID)
    {
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
                UseBag(itemID);
                break;
            case smartPhone:
                break;
            default:
                Debug.Log("itemName doesn't exist in UseItem");
                break;
        }
        if(item.GetDURABILITY() > 0)    item.SetDURABILITY(-1);
        if (item.GetDURABILITY() <= 0)
            DestroyObject(0, itemID);
    }

    private void DestroyObject(int use, int itemID)
    {
        GameObject[] myItems = GameObject.FindGameObjectsWithTag("UsedItem");
        foreach (GameObject i in myItems)
        {
            if (i.name == _dataManager.GetItemName(itemID) + "(Clone)")
            {
                Destroy(i);
                if(use == 0)
                {
                    MyUsedItem.Remove(itemID);
                }
                // else
                // {
                //     if(_dataManager.IsContainItem(itemID))  _dataManager.AddItem(itemID, -1);
                // }
            }
        }
    }

    public void ChangeItem(int currentItemID, int newItemID)
    {
        //currentItemID가 기존 아이템 , newItemID가 새로운 아이템
        //currentItemID 가 -1일 경우 : 기존 아이템 없음, 새로운 아이템 장착만.
        //newItemIte가 -1일 경우 : 기존 아이템 해제, 새로 장착할 아이템은 없음.

        //새로 교체할 아이템은 없지만 현재 장착된 아이템 해제할 경우 현재거 삭제만

        //현재거를 삭제(기존 아이템이 있어서 교체를 해야 할 경우 & 새 아이템 없이 기존 장착 아이템 해제만 하는 경우)
        if(currentItemID != -1)
        {
            int[] arr = {0,0,0,0,0,0,0,0,0,0};
            Item temp = new Item(MyUsedItem[currentItemID].GetItemName(), arr);     //빈 아이템 넣어줌으로써 UI Status초기화
            _uiManager.SetCurrentItemStatus(-1, temp);  //Player Status UI에 연결, 초기화

            if(MyUsedItem[currentItemID].GetDURABILITY() > 0)  //아직 내구도가 0이상으로 사용할 수 있을 경우 다시 인벤토리에 넣어줌
            {
                _dataManager.AddItem(currentItemID, 1);
                DestroyObject(1, currentItemID);    //기존 장착된 아이템 있을 경우 찾아서 프리팹 Destroy, 인자의 1은 아직 쓸 수 있을 때 MyUsedItem 에서 삭제 방지
            }     
            DestroyObject(0, currentItemID);    //내구도 0 이하라 더 이상 쓸 수 없을 경우 버림
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
    
    private void UseBag(int itemID)
    {
        //만약 플레이어에게 이미 장착되어 있는 가방이 있다면
        if(playerStatus != null)
        {
            if(playerStatus.EquipItemsList.Count > 0)
            {
                foreach(int id in playerStatus.EquipItemsList)
                {
                    if(_dataManager.GetItemSubCategory(id) == "가방")
                    {
                        ChangeItem(id, itemID);
                    }
                }
            }
            //이미 장착되어 있는 가방이 없다면
            else
            {
                ChangeItem(-1, itemID);
                //location, rotation -> 플레이어 쪽으로 수정 필요
                GameObject newBag = Instantiate(_dataManager.GetItemModel(itemID), Vector3.zero, Quaternion.identity);
                newBag.tag = "UsedItem";
            }
        }
        else    //just for test
        {
            ChangeItem(-1, itemID);
            GameObject newBag = Instantiate(_dataManager.GetItemModel(itemID), Vector3.zero, Quaternion.identity);
            newBag.tag = "UsedItem";
        }
    }

    private void UseBattery(int charge, int itemID)
    {
        //특수 조건 만족 시(문 여는데 필요)
        CellPhoneControl cellphone = GetCellPhoneComponent();
        cellphone.PhoneCharge(charge);
    }

    private void UseSleepingBag(Item item, int itemID)
    {
        //조건 확인해서 사용(마지막 날, 특정 위치에서)

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
        if(playerStatus != null)
        {
            //만약 플레이어에게 이미 장착되어 있는 무기가 있다면
            if(playerStatus.EquipItemsList.Count > 0)
            {
                foreach(int id in playerStatus.EquipItemsList)
                {
                    if(_dataManager.GetItemSubCategory(id) == "무기")
                    {
                        ChangeItem(id, itemID);
                    }
                }
            }
            //이미 장착되어 있는 무기가 없다면
            else
            {
                ChangeItem(-1, itemID);
                //location, rotation -> 플레이어 쪽으로 수정 필요
                GameObject newWeapon = Instantiate(_dataManager.GetItemModel(itemID), Vector3.zero, Quaternion.identity);
                newWeapon.tag = "UsedItem";
            }
        }
        else    //just for test
        {
            ChangeItem(-1, itemID);
            GameObject newWeapon = Instantiate(_dataManager.GetItemModel(itemID), Vector3.zero, Quaternion.identity);
            newWeapon.tag = "UsedItem";
        }

        int attackMax = playerStatus.Attack;
        playerStatus.CurAttack = playerStatus.CurAttack + attack > attackMax ? attackMax : playerStatus.CurAttack + attack;
        UnityEngine.Debug.Log("attack " + playerStatus.CurAttack);

        // 휘두를 때마다 내구도 마이나스
        item.SetDURABILITY(-1);
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
        // 켜져 있는 상태라면 지속적으로 내구도가 감소해야 함.....
        // lightControl = new LightControl(item.GetDURABILITY(), itemID);

        if(playerStatus != null)
        {
            //만약 플레이어에게 이미 장착되어 있는 라이트가 있다면
            if(playerStatus.EquipItemsList.Count > 0)
            {
                foreach(int id in playerStatus.EquipItemsList)
                {
                    if(_dataManager.GetItemSubCategory(id) == "라이트")
                    {
                        ChangeItem(id, itemID);
                    }
                }
            }
            //이미 장착되어 있는 라이트가 없다면
            else
            {
                ChangeItem(-1, itemID);
                //location, rotation -> 플레이어 쪽으로 수정 필요
                GameObject newLight = Instantiate(_dataManager.GetItemModel(itemID), Vector3.zero, Quaternion.identity);
                newLight.tag = "UsedItem";
            }
        }
        else    //just for test
        {
            ChangeItem(-1, itemID);
            GameObject newLight = Instantiate(_dataManager.GetItemModel(itemID), Vector3.zero, Quaternion.identity);
            newLight.tag = "UsedItem";
        }
        lightControl = new LightControl(2, itemID);

    }


  
    private void GameOver()
    {
        UnityEngine.Debug.Log("GameOver");
    }
}