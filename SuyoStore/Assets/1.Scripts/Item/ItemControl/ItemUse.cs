using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;
using TMPro;
using UnityEngine.Rendering;

public class ItemUse : MonoBehaviour
{
    [SerializeField] DataManager _dataManager;
    [SerializeField] UIManager _uiManager;
    private GameObject player;
    private PlayerStatus playerStatus;
    private PlayerController playerController;
    private LightControl lightControl;
    private ScenarioEvent _scenarioEvent;
    private bool isLightOn = false;
    private const string battery = "보조배터리", food = "음식", weapon = "무기", pill = "치료제", flashLight = "라이트", sleepingBag = "침낭", bag = "가방", smartPhone = "스마트폰", cardKey = "카드키";
    private Dictionary<int, Item> MyUsedItem = new Dictionary<int, Item>();
    public int GetItemDurability(int id) => MyUsedItem[id].GetDURABILITY();
    private Item item;
    
    // light로 시야 확보할 Global Volume
    GameObject volumeObj;
    Volume globalVolume;

    private void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        playerStatus = player.GetComponent<PlayerStatus>();
        playerController = player.GetComponent<PlayerController>();
        _scenarioEvent = _uiManager.GetComponent<ScenarioEvent>();
    }
    private void Update()
    {
        if (isLightOn)
        {
            if (lightControl.LightDurability())
                DestroyObject(0, lightControl.GetID());
        }
    }
    public void UseItem(int itemID)
    {
        if (MyUsedItem.ContainsKey(itemID))
            item = MyUsedItem[itemID];
        else
        {
            item = _dataManager.SetNewItem(itemID);
            MyUsedItem.Add(itemID, item);
        }
        switch (item.GetItemName())
        {
            case battery:
                UseBattery(itemID);
                break;
            case cardKey:
                UseCardKey(item);
                break;
            case food:
                UseFood(item.GetSATIETY());
                break;
            case weapon:
                WeaponSetting(itemID);
                break;
            case pill:
                UseHeal(item.GetHEAL());
                break;
            case flashLight:
                LightSetting(itemID);
                break;
            case sleepingBag:
                UseSleepingBag(item, itemID);
                break;
            case bag:
                UseBag(itemID);
                break;
            case smartPhone:
                UseSmartphone();
                break;
            default:
                Debug.Log("Cannot Use Item");
                break;
        }
        // if (item.GetDURABILITY() > 0) item.SetDURABILITY(-1);
        // if (item.GetDURABILITY() == 0)
        //     DestroyObject(0, itemID);
    }
    private void DestroyObject(int use, int itemID)
    {
        GameObject[] myItems = GameObject.FindGameObjectsWithTag("UsedItem");
        foreach (GameObject i in myItems)
        {
            if (i.name == _dataManager.GetItemFileName(itemID) + "(Clone)")
            {
                Destroy(i);
                if (use == 0)
                {
                    MyUsedItem.Remove(itemID);
                }
            }
        }
    }

    public void SetItemDURABILITY(int itemID)
    {
        MyUsedItem[itemID].SetDURABILITY(-1);
        _uiManager.SetCurrentItemStatus(itemID, MyUsedItem[itemID]);
        print("Durability : " + MyUsedItem[itemID].GetDURABILITY());
    }

    private GameObject FindExactWeapon(string itemName)
    {
        GameObject[] weapon = playerController.Weapons;
        foreach (GameObject w in weapon)
        {
            if (w.name == itemName)
            {
                return w;
            }
        }
        Debug.Log("Cannot find exact weapon!");
        return null;
    }

    private GameObject FindExactLight(string itemName)
    {
        GameObject[] light = playerController.Lights;
        foreach (GameObject l in light)
        {
            if (l.name == itemName)
            {
                return l;
            }
        }
        Debug.Log("Cannot find exact light!");
        return null;
    }

    private GameObject FindExactBag(string itemName)
    {
        GameObject[] bag = playerController.Bags;
        foreach (GameObject b in bag)
        {
            if (b.name == itemName)
            {
                return b;
            }
        }
        Debug.Log("Cannot find exact Bag!");
        return null;
    }

    public void ChangeItem(int currentItemID, int newItemID)
    {
        //currentItemID가 기존 아이템 , newItemID가 새로운 아이템
        //currentItemID 가 -1일 경우 : 기존 아이템 없음, 새로운 아이템 장착만.
        //newItemID가 -1일 경우 : 기존 아이템 해제, 새로 장착할 아이템은 없음.

        //새로 교체할 아이템은 없지만 현재 장착된 아이템 해제할 경우 현재거 삭제만

        //현재거를 삭제(기존 아이템이 있어서 교체를 해야 할 경우 & 새 아이템 없이 기존 장착 아이템 해제만 하는 경우)
        if (currentItemID != -1)
        {
            int[] arr = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Item temp = new Item(MyUsedItem[currentItemID].GetItemName(), arr);     //빈 아이템 넣어줌으로써 UI Status초기화
            _uiManager.SetCurrentItemStatus(-1, temp);  //Player Status UI에 연결, 초기화

            if (MyUsedItem[currentItemID].GetDURABILITY() > 0)  //아직 내구도가 0이상으로 사용할 수 있을 경우 다시 인벤토리에 넣어줌
            {
                _dataManager.AddItem(currentItemID, 1);
                //DestroyObject(1, currentItemID);    //기존 장착된 아이템 있을 경우 찾아서 프리팹 Destroy, 인자의 1은 아직 쓸 수 있을 때 MyUsedItem 에서 삭제 방지 
            }
            else    //내구도 0 이하라 더 이상 쓸 수 없을 경우 버림
            {
                MyUsedItem.Remove(currentItemID);
                //DestroyObject(0, currentItemID);    
            }

            //기존 장착된 아이템 찾아서 삭제
            if (_dataManager.GetItemSubCategory(currentItemID) == "무기")
            {
                GameObject weapon = FindExactWeapon(_dataManager.GetItemFileName(currentItemID));
                playerStatus.RemoveEquipItem(currentItemID);
                weapon.SetActive(false);
            }
            else if (_dataManager.GetItemSubCategory(currentItemID) == "라이트")
            {
                GameObject light = FindExactLight(_dataManager.GetItemFileName(currentItemID));
                playerStatus.RemoveEquipItem(currentItemID);
                light.SetActive(false);
            }
            else // 가방
            {
                GameObject bag = FindExactBag(_dataManager.GetItemFileName(currentItemID));
                playerStatus.RemoveEquipItem(currentItemID);
                bag.SetActive(false);
            }
        }

        //새로운 거 장착
        if (newItemID != -1) _uiManager.SetCurrentItemStatus(newItemID, MyUsedItem[newItemID]);
    }

    private void UseBag(int itemID)
    {
        //만약 플레이어에게 이미 장착되어 있는 가방이 있다면
        if (playerStatus.EquipItemsList.Count > 0)
        {
            foreach (int id in playerStatus.EquipItemsList)
            {
                if (_dataManager.GetItemSubCategory(id) == "가방")
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
            // GameObject newBag = Instantiate(_dataManager.GetItemModel(itemID), Vector3.zero, Quaternion.identity);
            // newBag.tag = "UsedItem";
            GameObject bag = FindExactBag(_dataManager.GetItemFileName(itemID));
            bag.SetActive(true);
            playerStatus.AddEquipItem(itemID);
        }
    }
    IEnumerator WaitToDisappear()
    {
        yield return new WaitForSeconds(4);
        GameManager.GM.mustItemCanvas.SetActive(false);
    }
    private bool CheckMustItemDays(string _msg, bool check = false)
    {
        if (_dataManager.dateControl.GetDays() < 7 || check)
        {
            GameManager.GM.msg.text = _msg;
            GameManager.GM.mustItemCanvas.SetActive(true);
            StartCoroutine(WaitToDisappear());
            return false;
        }
        return true;
    }
    private void UseBattery(int itemID)
    {
        Dictionary<int, int> myItems = _dataManager.GetMyItems();
        // 배터리 모으는 루트가 아니어도 10개 안 모았다고 말해주는지?
        if (myItems[itemID] < 10)
        {
            string message = "배터리의 양이 부족한 것 같다. (" + myItems[itemID] + "/10)";
            CheckMustItemDays(message, true);
        }
        else
            CheckMustItemDays("아직은 구조대가 도착하지 않아 지금은 위험할 것 같다.");
    }
    private void UseCardKey(Item item)
    {
        if (CheckMustItemDays("아직은 구조대가 도착하지 않아 지금은 위험할 것 같다."))
            GameManager.GM.CheckCondition();
    }
    private void UseSleepingBag(Item item, int itemID)
    {
        //조건 확인해서 사용(마지막 날, 특정 위치에서)
        if (GameManager.GM.GetSceneName() == "00.B2")
        {
            _scenarioEvent.GetScenarioItemName("Sleeping Bag");
        }
        else
        {
            CheckMustItemDays("아직은 사용할 때가 아닌 것 같다.");
        }
        
    }
    public void UseFood(int satiety)
    {
        playerStatus.RecoverStatus(Status.eCurStatusType.cSatiety, satiety);
    }
    private void WeaponSetting(int itemID)
    {
        //만약 플레이어에게 이미 장착되어 있는 무기가 있다면
        if (playerStatus.EquipItemsList.Count > 0)
        {
            foreach (int id in playerStatus.EquipItemsList)
            {
                if (_dataManager.GetItemSubCategory(id) == "무기")
                {
                    ChangeItem(id, itemID);
                    UseWeapon(item.GetATTACK(), itemID);
                    return;
                }
            }
        }
        //이미 장착되어 있는 무기가 없다면
        else
        {
            UseWeapon(item.GetATTACK(), itemID);
            ChangeItem(-1, itemID);
            //location, rotation -> 플레이어 쪽으로 수정 필요
            //GameObject newWeapon = Instantiate(_dataManager.GetItemModel(itemID), Vector3.zero, Quaternion.identity);
            GameObject weapon = FindExactWeapon(_dataManager.GetItemFileName(itemID));
            weapon.SetActive(true);
            playerStatus.AddEquipItem(itemID);
            //newWeapon.tag = "UsedItem";
        }
    }
    public void UseWeapon(int attack, int itemID)
    {
        playerStatus.CurAttack = 10 + attack;
        // 휘두를 때마다 내구도 마이나스
        //item.SetDURABILITY(-1);
    }
    public void UseHeal(int heal)
    {
        playerStatus.RecoverStatus(Status.eCurStatusType.cHp, heal);
        playerStatus.isInfect = false;
    }
    private void LightSetting(int itemID)
    {
        //만약 플레이어에게 이미 장착되어 있는 라이트가 있다면
        if (playerStatus.EquipItemsList.Count > 0)
        {
            foreach (int id in playerStatus.EquipItemsList)

            {
                if (_dataManager.GetItemSubCategory(id) == "라이트")
                {
                    item.SetDURABILITY(lightControl.StopCounter());
                    isLightOn = false;
                    ChangeItem(id, itemID);
                    UseLight(item, itemID);
                }
            }
        }
        //이미 장착되어 있는 라이트가 없다면
        else
        {
            ChangeItem(-1, itemID);
            UseLight(item, itemID);
            //location, rotation -> 플레이어 쪽으로 수정 필요
            // GameObject newLight = Instantiate(_dataManager.GetItemModel(itemID), Vector3.zero, Quaternion.identity);
            // newLight.tag = "UsedItem";
            GameObject light = FindExactLight(_dataManager.GetItemFileName(itemID));
            light.SetActive(true);
            playerStatus.AddEquipItem(itemID);
        }

    }
    private void UseLight(Item item, int itemID)
    {
        isLightOn = true;
        lightControl = new LightControl(item.GetDURABILITY(), itemID);
        volumeObj = GameObject.FindGameObjectWithTag("GlobalVolume");
        globalVolume = volumeObj.GetComponent<Volume>();
        globalVolume.weight = 0.5f ;
    }
    private void UseSmartphone()
    {
        CellPhoneMsgs.Instance.Show();
    }
}