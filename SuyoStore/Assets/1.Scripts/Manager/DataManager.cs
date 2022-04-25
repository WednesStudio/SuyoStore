using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

public class DataManager : MonoBehaviour
{
    [Header("Other Scripts")]
    [SerializeField] LoadExcel _loadExcel = null;
    [SerializeField] CurrentStateUI _currentStateUI = null;
    [SerializeField] UIManager _uiManager = null;
    [SerializeField] InventoryUI _inventoryUI = null;

    [Header("Objects")]
    [SerializeField] ItemObject[] _itemObjects = null;

    [Header("Current My Data")]
    public Dictionary<int, int> MyItems = new Dictionary<int, int>();

    public Dictionary<int, int> GetMyItems() => MyItems;
    public List<ItemData> GetItemList() => _totalItemList;
    public ItemData GetItem(int ID) => _totalItemList[ID];
    public GameObject GetItemModel(int ID) => _totalItemList[ID].prefab;
    public string GetItemCategory(int ID) => _totalItemList[ID].category;
    public string GetItemSubCategory(int ID) => _totalItemList[ID].subCategory;
    public string GetItemFileName(int ID) => _totalItemList[ID].fileName;
    public string GetItemName(int ID) => _totalItemList[ID].itemName;
    public Sprite GetItemImage(int ID) => _itemObjects[ID].Profile;
    public string GetDescription(int ID) => _itemObjects[ID].description;
    public bool IsContainItem(int ID) => MyItems.ContainsKey(ID);
    public string GetConditionRoute() => jsonConditionData.route;
    public string GetConditionMsg() => jsonConditionData.message;
    public string GetConditionMust() => jsonConditionData.must;
    public int GetConditionCount() => jsonConditionData.count;
    public string GetConditionExit() => jsonConditionData.exit;
    public string GetLocation() => _location;

    //private List<Item> _itemList = null;
    private string _date, _location;
    private List<ItemData> _totalItemList;
    private const string battery = "보조배터리", food = "음식", weapon = "무기", pill = "치료제", flashLight = "라이트", sleepingBag = "침낭", smartPhone = "스마트폰", bag = "가방", cardKey = "카드키";
    private int maxCapacity = 30;

    private JsonConditionData jsonConditionData;
    private string directory = "Data/";
    private string[] routes = { "route1", "route2", "route3" };
    private int selectedRoute;
    public int GetSelectedRoute() => selectedRoute;
    public DateControl dateControl;
    public void Awake()
    {
        dateControl = FindObjectOfType<DateControl>();
    }
    public void LoadJsonData()
    {
        selectedRoute = UnityEngine.Random.Range(0, 3);
        string dir = directory + routes[selectedRoute];
        GameObject.Find("Reader").GetComponent<LoadJson>().LoadMsgData(dir);
        GameObject.Find("Reader").GetComponent<LoadJson>().LoadConditionData(dir);
        jsonConditionData = GameObject.Find("Reader").GetComponent<LoadJson>().conditionList;
    }
    public void SetData()
    {
        _loadExcel.LoadItemData();
        _totalItemList = _loadExcel.itemDatabase;

        // MyItems.Add(GetItemID("배터리1"), 10);
        MyItems.Add(GetItemID("카드키"), 1);
        // MyItems.Add(GetItemID("침낭1"), 1);
    }

    public void SetCurrentInfo(string date, string location)
    {
        this._date = date;
        if(location != "") this._location = location;
        if (_currentStateUI != null) _currentStateUI.SetCurrentState(_date, _location);
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
        int capacity = _totalItemList[itemID].weight;
        //가방 설정
        string category = _totalItemList[itemID].subCategory;
        if (count > 0)
        {
            if (IsContainItem(itemID))
            {
                MyItems[itemID] += count;

                switch (category)
                {
                    case battery:
                        _inventoryUI.SetImportantBagContents();
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
                        maxCapacity = 30 + _totalItemList[itemID].capacity;
                        GameManager.GM.UseItem(itemID);
                        break;
                    case sleepingBag:
                        _inventoryUI.SetImportantBagContents();
                        break;
                    case smartPhone:
                        _inventoryUI.SetImportantBagContents();
                        break;
                    case cardKey:
                        _inventoryUI.SetImportantBagContents();
                        break;
                    default:
                        Debug.Log("item Category doesn't exist!");
                        break;
                }
                if (category != bag) _inventoryUI.SetTotalBagContents();
                _inventoryUI.SetBagCapacity(capacity, maxCapacity);
            }
            else
            {
                MyItems.Add(itemID, count);
                switch (category)
                {
                    case battery:
                        _inventoryUI.SetImportantBagContents();
                        //_inventoryUI.SetBatteryBagContents();
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
                        maxCapacity = 30 + _totalItemList[itemID].capacity;
                        GameManager.GM.UseItem(itemID);
                        break;
                    case sleepingBag:
                        _inventoryUI.SetImportantBagContents();
                        //_inventoryUI.SetSleepingBagContents();
                        break;
                    case smartPhone:
                        _inventoryUI.SetImportantBagContents();
                        break;
                    case cardKey:
                    _inventoryUI.SetImportantBagContents();
                    break;
                    default:
                        Debug.Log("item Category doesn't exist!");
                        break;
                }
                if (category != bag) _inventoryUI.SetTotalBagContents();
                _inventoryUI.SetBagCapacity(capacity, maxCapacity);
            }
        }
        else
        {
            if (!MyItems.ContainsKey(itemID)) return;
            if (MyItems[itemID] > 0)
            {
                if (MyItems[itemID] == 1)
                {
                    MyItems.Remove(itemID);
                }
                else MyItems[itemID] -= 1;

                switch (category)
                {
                    case battery:
                        _inventoryUI.SetImportantBagContents();
                        //_inventoryUI.SetBatteryBagContents();
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
                        break;
                    case sleepingBag:
                        _inventoryUI.SetImportantBagContents();
                        //_inventoryUI.SetSleepingBagContents();
                        break;
                    case smartPhone:
                        _inventoryUI.SetImportantBagContents();
                        break;
                    case cardKey:
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

        _uiManager.SetPlayerSpeed(_inventoryUI.GetCurrentCapacity(), maxCapacity);
    }

    public int GetItemCount(int itemID)
    {
        if (MyItems.ContainsKey(itemID))
            return MyItems[itemID];

        return 0;
    }

    public int GetItemID(string name)
    {
        foreach (ItemData i in _totalItemList)
        {
            if (i.itemName == name)
            {
                return i.ID;
            }
        }
        return -1;
    }
    public List<int> GetItemIDMyList(string name)
    {
        // 선택된 루트의 must item 중 "SM_Props_Battery" 같이 앞부분 글자를 포함한 모든 아이템 아이디를 저장

        List<int> idList = new List<int>();
        foreach (ItemData i in _totalItemList)
        {
            string str = i.fileName;
            if (str.Length > name.Length && str.Substring(0, name.Length) == name)
                idList.Add(i.ID);
        }
        return idList;
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