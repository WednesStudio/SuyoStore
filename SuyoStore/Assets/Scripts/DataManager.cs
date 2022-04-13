using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [Header("Other Scripts")]
    //[SerializeField] CsvReader _csvReader = null;
    [SerializeField] CurrentStateUI _currentStateUI = null;

    [Header("Objects")]
    [SerializeField] private ItemObject[] _itemObjects = null;

    public Dictionary<int, int> MyItems; 

    //public Item GetItem(int ID) => this._itemList[ID];
    //public List<Item> GetItemList => _itemList;
    public GameObject GetItemModel(int ID) => _itemObjects[ID].ItemModel;
    public Sprite GetItemImage(int ID) => _itemObjects[ID].Profile;
    public bool IsContainItem(int ID) => MyItems.ContainsKey(ID);
    //private List<Item> _itemList = null;
    private string _date, _location;


    public void SetData() //out bool isGameDataExist)
    {
        //_csvReader.Read(out _creatureList, out _itemList);
    }

    public void SetCurrentInfo(string date, string location)
    {
        this._date = date;
        this._location = location;
        if(_currentStateUI != null) _currentStateUI.SetCurrentState(_date, _location);
    }

    public void AddItem(int itemID, int count = 1)
    {
        if (IsContainItem(itemID))
            MyItems[itemID] += count;
        else
            MyItems.Add(itemID, count);

        if (count < 0)
        {
            if (MyItems[itemID] == 0)
                MyItems.Remove(itemID);
        }
    }

    public int GetItemCount(int itemID)
    {
        if (MyItems.ContainsKey(itemID))
            return MyItems[itemID];

        return 0;
    }



    [System.Serializable]
    public struct ItemObject
    {
        public string Name;
        public Sprite Profile;
        public GameObject ItemModel;
    }
}
