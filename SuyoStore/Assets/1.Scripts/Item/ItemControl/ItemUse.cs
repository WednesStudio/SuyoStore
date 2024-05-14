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
            {
                MyUsedItem[lightControl.GetID()].SetDURABILITY(0);
                ChangeItem(lightControl.GetID(), -1);
            }
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
                //무기 사용
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
                BagSetting(itemID);
                break;
            case smartPhone:
                UseSmartphone();
                break;
            default:
                Debug.Log("Cannot Use Item");
                break;
        }
    }

    public void SetItemDURABILITY(int itemID)
    {
        MyUsedItem[itemID].SetDURABILITY(-1);
        _uiManager.SetCurrentItemStatus(itemID, MyUsedItem[itemID]);
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
        //newItemID가 -1일 경우 : 기존 아이템 유무와 상관없이 새로 장착할 아이템 없음.

        //현재거를 삭제(기존 아이템이 있어서 교체를 해야 할 경우. 새로 교체할 아이템은 없지만 현재 장착된 아이템 해제할 경우, 현재거 삭제만)
        if (currentItemID != -1)
        {
            //UI 초기화
            int[] arr = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Item temp = new Item(MyUsedItem[currentItemID].GetItemName(), arr);     //빈 아이템 넣어줌으로써 UI Status초기화
            _uiManager.SetCurrentItemStatus(-1, temp);  //Player Status UI에 연결, 초기화

            //인벤토리 관리
            if (MyUsedItem[currentItemID].GetDURABILITY() > 0)  //아직 내구도가 0이상으로 사용할 수 있을 경우 다시 인벤토리에 넣어줌
            {
                _dataManager.AddItem(currentItemID, 1);

                if(_dataManager.GetItemSubCategory(currentItemID) == "라이트")
                {
                    MyUsedItem[currentItemID].SetDURABILITY(lightControl.StopCounter());
                }
                
                //DestroyObject(1, currentItemID);    //기존 장착된 아이템 있을 경우 찾아서 프리팹 Destroy, 인자의 1은 아직 쓸 수 있을 때 MyUsedItem 에서 삭제 방지 
            }
            else    //내구도 0 이하라 더 이상 쓸 수 없을 경우 버림
            {
                MyUsedItem.Remove(currentItemID);
                //DestroyObject(0, currentItemID);    
            }

            //기존 장착된 아이템 찾아서 삭제 - Player 쪽 아이템 아이템 해제
            if (_dataManager.GetItemSubCategory(currentItemID) == "무기")   //해제해야 할 게 무기라면
            {
                GameObject weapon = FindExactWeapon(_dataManager.GetItemFileName(currentItemID));   //player에 붙어있는 무기 배열 속 아이템 받아옴
                playerStatus.RemoveEquipWeapon(currentItemID);    //player status의 equipList 업데이트
                weapon.SetActive(false);    //받아온 무기 아이템 안 보이게
                playerController.EquipWeapon = null;
            }
            else if (_dataManager.GetItemSubCategory(currentItemID) == "라이트")    //무기와 동일한 방식으로 작동
            {
                GameObject light = FindExactLight(_dataManager.GetItemFileName(currentItemID));
                playerStatus.RemoveEquipFlashlight(currentItemID);
                light.SetActive(false);
                isLightOn = false;
                globalVolume.weight = 0.8f;
            }
            else if (_dataManager.GetItemSubCategory(currentItemID) == "가방")// 가방
            {
                GameObject bag = FindExactBag(_dataManager.GetItemFileName(currentItemID));
                playerStatus.AddEquipBag(currentItemID);
                bag.SetActive(false);
            }
            else
            {
                Debug.Log("[ChangeItem system] Player doesn't have this Item. Can't setactive");
            }
        }

        //새로운 거 장착
        if (newItemID != -1)
        {
            //UI 설정
            _uiManager.SetCurrentItemStatus(newItemID, MyUsedItem[newItemID]);

            //새로 장착할 아이템 찾아서 setActive true
            if (_dataManager.GetItemSubCategory(newItemID) == "무기")   //장착해야 할 게 무기라면
            {
                GameObject weapon = FindExactWeapon(_dataManager.GetItemFileName(newItemID));   //무기 배열 속 아이템 받아옴
                playerStatus.AddEquipWeapon(newItemID);    //player status의 equipList 업데이트
                weapon.SetActive(true);    //받아온 무기 아이템이 보이도록
                playerController.EquipWeapon = weapon;
            }
            else if (_dataManager.GetItemSubCategory(newItemID) == "라이트")    //무기와 동일한 방식으로 작동
            {
                GameObject light = FindExactLight(_dataManager.GetItemFileName(newItemID));
                playerStatus.AddEquipFlashlight(newItemID);
                light.SetActive(true);
            }
            else if(_dataManager.GetItemSubCategory(newItemID) == "가방")// 가방
            {
                GameObject bag = FindExactBag(_dataManager.GetItemFileName(newItemID));
                playerStatus.AddEquipBag(newItemID);
                bag.SetActive(true);
            }
            else
            {
                Debug.Log("[ChangeItem system] Player can't set this Item.");
            }
        } 
    }
    private void BagSetting(int itemID)
    {
        //만약 플레이어에게 이미 장착되어 있는 가방이 있다면(1 이상)
        if (playerStatus.EquipBagList.Count > 0)
        {
            foreach (int id in playerStatus.EquipBagList)
            {
                //장착되어 있는 아이템 중 가방이 있다면
                if (_dataManager.GetItemSubCategory(id) == "가방")
                {
                    ChangeItem(id, itemID);     //기존 아이템 해제(SetActive false), 새로운 아이템 장착
                    UseBag(item.GetCAPACITY(), itemID);    //플레이어의 가방 최대량(MaxCarryingBag)을 바꿔줌
                    return;
                }
            }
        }
        //이미 장착 중인 가방이 없다면
        else
        {
            UseBag(item.GetCAPACITY(), itemID);
            ChangeItem(-1, itemID);
        }
    }
    private void UseBag(int capacity, int itemID)
    {
        //capacity = 가방 크기(최대 수용가는한 적재량)
        //가방 최대량 달라지기
        playerStatus.MaxCarryingBag = capacity;
    }
    private bool CheckMustItemDays(string _msg, bool check = false)
    {
        if (_dataManager.dateControl.GetDays() < 7 || check)
        {
            GameManager.GM.UpdateMonologue(_msg);
            return false;
        }
        return true;
    }
    private void UseBattery(int itemID)
    {
        
    }
    private void UseCardKey(Item item)
    {
        
    }
    private void UseSleepingBag(Item item, int itemID)
    {
        //조건 확인해서 사용(마지막 날, 특정 위치에서)
        if (GameManager.GM.GetSceneName() == "-2")
        {
            _scenarioEvent.GetScenarioItemName("Sleeping Bag");
        }        
    }
    public void UseFood(int satiety)
    {
        playerStatus.RecoverStatus(Status.eCurStatusType.cSatiety, satiety);
    }
    private void WeaponSetting(int itemID)
    {
        //만약 플레이어에게 이미 장착되어 있는 아이템이 있다면 (무기, 라이트 둘 다 하나라도 있으면 1 초과)
        if (playerStatus.EquipWeaponList.Count > 0)
        {
            foreach (int id in playerStatus.EquipWeaponList)
            {
                //장착되어 있는 아이템 중 무기가 있다면
                if (_dataManager.GetItemSubCategory(id) == "무기")
                {
                    ChangeItem(id, itemID);     //기존 아이템 해제(SetActive false), 새로운 아이템 장착
                    UseWeapon(item.GetATTACK(), itemID);    //플레이어 공격력 올려줌
                    return;
                }
            }
        }
        //이미 장착되어 있는 무기가 없다면
        else
        {
            UseWeapon(item.GetATTACK(), itemID);
            ChangeItem(-1, itemID);
        }
    }
    public void UseWeapon(int attack, int itemID)
    {
        playerStatus.CurAttack = 10 + attack;
    }
    public void UseHeal(int heal)
    {
        playerStatus.RecoverStatus(Status.eCurStatusType.cHp, heal);
        playerStatus.isInfect = false;
    }
    private void LightSetting(int itemID)
    {
        //만약 플레이어에게 이미 장착되어 있는 라이트가 있다면
        if (playerStatus.EquipFlashlightList.Count > 0)
        {
            foreach (int id in playerStatus.EquipFlashlightList)
            {
                if (_dataManager.GetItemSubCategory(id) == "라이트")
                {
                    MyUsedItem[id].SetDURABILITY(lightControl.StopCounter());
                    isLightOn = false;
                    ChangeItem(id, itemID);
                    UseLight(item, itemID);
                    break;
                }
            }
        }
        //이미 장착되어 있는 라이트가 없다면
        else
        {
            ChangeItem(-1, itemID);
            UseLight(item, itemID);
        }
    }
    private void UseLight(Item item, int itemID)
    {
        isLightOn = true;
        if(item.GetDURABILITY() == 30 || item.GetDURABILITY() == 40)
        {
            lightControl = new LightControl(item.GetDURABILITY(), itemID);
        }
        else
        {
            float min = item.GetDURABILITY() / 60;
            float sec = item.GetDURABILITY() % 60;

            float dur = min + (sec / 100);
            lightControl = new LightControl(dur, itemID);
        }
        
        volumeObj = GameObject.FindGameObjectWithTag("GlobalVolume");
        globalVolume = volumeObj.GetComponent<Volume>();
        switch (itemID)
        {
            // 밝기 세기 : 13 < 14 < 15
            case 13:
                globalVolume.weight = 0.8f; // 10
                break;
            case 14:
                globalVolume.weight = 0.6f; // 20
                break;
            case 15:
                globalVolume.weight = 0.4f; // 40
                break;
            default:
                break;
        }
    }
    private void UseSmartphone()
    {
        CellPhoneMsgs.Instance.Show();
    }
}