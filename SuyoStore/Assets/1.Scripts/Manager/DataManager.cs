using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

public class DataManager : MonoBehaviour
{
    [Header("Other Scripts")]
    [SerializeField] LoadExcel _loadExcel = null;
    [SerializeField] CurrentStateUI _currentStateUI = null;
    [SerializeField] InventoryUI _inventoryUI = null;

    [Header("Objects")]
    [SerializeField] ItemObject[] _itemObjects = null;

    [Header("Current My Data")]
    public Dictionary<int, int> MyItems = new Dictionary<int, int>(); 


    public List<ItemData> GetItemList => _totalItemList;
    public ItemData GetItem(int ID) => _totalItemList[ID];
    public GameObject GetItemModel(int ID) => _totalItemList[ID].prefab;
    public string GetItemCategory(int ID) => _totalItemList[ID].category;
    public string GetItemSubCategory(int ID) => _totalItemList[ID].subCategory;
    public string GetItemName(int ID) => _totalItemList[ID].itemName;
    public Sprite GetItemImage(int ID) => _itemObjects[ID].Profile;
    public string GetDescription(int ID) => _itemObjects[ID].description;
    public bool IsContainItem(int ID) => MyItems.ContainsKey(ID);

    //private List<Item> _itemList = null;
    private string _date, _location;
    private List<ItemData> _totalItemList;
    private const string battery = "보조배터리", food = "음식", weapon = "무기", pill = "치료제", flashLight = "라이트", sleepingBag = "침낭", smartPhone = "스마트폰", bag = "가방";
    private int maxCapacity = 30;
    
    public void SetData() //out bool isGameDataExist)
    {
        //_csvReader.Read(out _creatureList, out _itemList);
        _loadExcel.LoadItemData();
        _totalItemList = _loadExcel.itemDatabase;        
    }

    public void SetCurrentInfo(string date, string location)
    {
        this._date = date;
        this._location = location;
        if(_currentStateUI != null) _currentStateUI.SetCurrentState(_date, _location);
    }

    public Item SetNewItem(int ID)
    {
        string name = _totalItemList[ID].subCategory;
        int[] attributes = new int[(int)Attributes.TOTAL];
        attributes[(int)Attributes.ATTACK] = _totalItemList[ID].attack;
        attributes[(int)Attributes.HEAL] = _totalItemList[ID].heal;
        attributes[(int)Attributes.SATIETY] = _totalItemList[ID].satiety;
        attributes[(int)Attributes.BATTERYCHARGE] = _totalItemList[ID].batteryCharge;
        attributes[(int)Attributes.SIGHTRANGE] = _totalItemList[ID].sightRange;
        attributes[(int)Attributes.CAPACITY] = _totalItemList[ID].capacity;
        attributes[(int)Attributes.DEATHRATE] = _totalItemList[ID].deathRate;
        attributes[(int)Attributes.DURABILITY] = _totalItemList[ID].durability;
        attributes[(int)Attributes.WEIGHT] = _totalItemList[ID].weight;

        Item newItem = new Item(name, attributes);
        return newItem;
    }

    public void AddItem(int itemID, int count = 1)
    {
        print("Add item");
        int capacity = _totalItemList[itemID].weight;
        //가방 설정
        if(_inventoryUI.GetCurrentCapacity() + capacity > maxCapacity)
        {
            //경고창 띄움
            return;
        }

        string category = _totalItemList[itemID].subCategory;
        print(category);
        if (IsContainItem(itemID))
        {
            MyItems[itemID] += count;

            switch (category)
            {
                case battery:
                    _inventoryUI.SetBatteryBagContents();
                    break;
                case food:
                    _inventoryUI.SetFoodBagContents();
                    break;
                case weapon:
                    _inventoryUI.SetWeaponBagContents();
                    break;
                case pill:
                    _inventoryUI.SetMedicineBagContents();
                    break;
                case flashLight:
                    _inventoryUI.SetLightBagContents();
                    break;
                case bag:
                    _inventoryUI.SetBagOfBagContents();
                    maxCapacity = _totalItemList[itemID].capacity;
                    break;
                case sleepingBag:
                    _inventoryUI.SetSleepingBagContents();
                    break;
                case smartPhone:
                    _inventoryUI.SetImportantBagContents();
                    break;
                default:
                    Debug.Log("item Category doesn't exist!");
                    break;
            }
            _inventoryUI.SetBagCapacity(capacity, maxCapacity);
        }

        else
        {
            MyItems.Add(itemID, count);
            print("testsets " + MyItems[itemID]);
            switch (category)
            {
            case battery:
                _inventoryUI.SetBatteryBagContents();
                break;
            case food:
                print("food");
                _inventoryUI.SetFoodBagContents();
                break;
            case weapon:
                _inventoryUI.SetWeaponBagContents();
                break;
            case pill:
                _inventoryUI.SetMedicineBagContents();
                break;
            case flashLight:
                _inventoryUI.SetLightBagContents();
                break;
            case bag:
                _inventoryUI.SetBagOfBagContents();
                maxCapacity = _totalItemList[itemID].capacity;
                break;
            case sleepingBag:
                _inventoryUI.SetSleepingBagContents();
                break;
            case smartPhone:
                _inventoryUI.SetImportantBagContents();
                break;
            default:
                Debug.Log("item Category doesn't exist!");
                break;
            }
            _inventoryUI.SetTotalBagContents();
            _inventoryUI.SetBagCapacity(capacity, maxCapacity);
        }
            

        if (count < 0)
        {
            if (MyItems[itemID] == 0)
            {
                MyItems.Remove(itemID);

                switch (category)
                {
                case battery:
                    _inventoryUI.SetBatteryBagContents();
                    break;
                case food:
                    _inventoryUI.SetFoodBagContents();
                    break;
                case weapon:
                    _inventoryUI.SetWeaponBagContents();
                    break;
                case pill:
                    _inventoryUI.SetMedicineBagContents();
                    break;
                case flashLight:
                    _inventoryUI.SetLightBagContents();
                    break;
                case bag:
                    _inventoryUI.SetBagOfBagContents();
                    break;
                case sleepingBag:
                    _inventoryUI.SetSleepingBagContents();
                    break;
                case smartPhone:
                    _inventoryUI.SetImportantBagContents();
                    break;
                default:
                    Debug.Log("item Category doesn't exist!");
                    break;
                }
            }
            _inventoryUI.SetTotalBagContents();
            _inventoryUI.SetBagCapacity(-capacity, maxCapacity);
        }

    }

    public int GetItemCount(int itemID)
    {
        if (MyItems.ContainsKey(itemID))
            return MyItems[itemID];

        return 0;
    }

    public int GetItemID(string name)
    {
        foreach(ItemData i in _totalItemList)
        {
            if(i.itemName == name)
            {
                return i.ID;
            }
        }
        return -1;
    }



    [System.Serializable]
    public struct ItemObject
    {
        public int ID;
        public string Name;
        public Sprite Profile;
        public string Category;
        public string description;
    }
}