using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] DataManager _dataManager;
    [SerializeField] GameObject _statusInventoryWindow;
    [SerializeField] GameObject _checkUseItemWindow;
    [SerializeField] Button[] _categoryButtons;
    [SerializeField] GameObject[] _itemScrollViews;
    [SerializeField] TextMeshProUGUI _bagCapacity;
    [Header("Inside of Bag Contents")]
    [SerializeField] private GameObject _totalContentsParent = null;
    [SerializeField] private GameObject _weaponContentsParent = null;
    [SerializeField] private GameObject _lightContentsParent = null;
    [SerializeField] private GameObject _foodContentsParent = null;
    [SerializeField] private GameObject _medicineContentsParent = null;
    [SerializeField] private GameObject _importantContentsParent = null;
    private BagItems[] _totalContents = null;
    private BagItems[] _weaponContents = null;
    private BagItems[] _lightContents = null;
    private BagItems[] _foodContents = null;
    private BagItems[] _medicineContents = null;
    private BagItems[] _importantContents = null;
    private const string battery = "보조배터리", food = "음식", weapon = "무기", pill = "치료제", flashLight = "라이트", sleepingBag = "침낭", smartPhone = "스마트폰", bag = "가방";
    private int _currentCapacity = 0;
    private string _itemName;

    private void Start() 
    {
        ChangeScrollView(0);    
    }

    public void ChangeScrollView(int index)
    {
        for(int i = 0; i < _categoryButtons.Length; i ++)
        {
            if(i == index)
            {
                _itemScrollViews[i].SetActive(true);
                _categoryButtons[i].GetComponent<Image>().color = Color.white;
            }
            else
            {
                Color color;
                _itemScrollViews[i].SetActive(false);
                ColorUtility.TryParseHtmlString("#DEDEDE", out color);
                if(ColorUtility.TryParseHtmlString("#DEDEDE", out color))
                {
                    _categoryButtons[i].GetComponent<Image>().color = color;
                }
                
            } 
        }
        SoundManager.SM.PlaySfxSound(SfxSoundName.ButtonClick);
    }

    public void OnCheckItemUseWindow(string itemName)
    {
        _itemName = itemName;
        _checkUseItemWindow.SetActive(true);
        _checkUseItemWindow.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _itemName + " 을/(를) 사용하시겠습니까?";
        _checkUseItemWindow.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _itemName;
    }

    public void UseItem()
    {
        _checkUseItemWindow.SetActive(false);
        GameManager.GM.UseItem(_dataManager.GetItemID(_checkUseItemWindow.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text));
    }

    public void OffCheckItemUseWindow()
    {
        _checkUseItemWindow.SetActive(false);
    }

    public int GetCurrentCapacity()
    {
        return _currentCapacity;
    }

    public void SetBagCapacity(int capacity, int maxCapacity)
    {
        _currentCapacity += capacity;
        _bagCapacity.text = _currentCapacity.ToString() + "/" + maxCapacity.ToString();
        Color color;

        if((double)_currentCapacity / (double)maxCapacity > 0.8)
        {
            ColorUtility.TryParseHtmlString("#FF6026", out color);
            if(ColorUtility.TryParseHtmlString("#FF6026", out color))
            {
                _bagCapacity.color = color;
            }
            if(_currentCapacity/maxCapacity >= 1.0)
            {
                _bagCapacity.color = Color.red;
            }
        }
        else
        {
            if(ColorUtility.TryParseHtmlString("#FFFFFF", out color))
            {
                _bagCapacity.color = color;
            }
        }
    }

    /// <summary> 인벤토리 속 아이템 관리 </summary>

    public void SetTotalBagContents()
    {
        if (_totalContents == null) _totalContents = _totalContentsParent.GetComponentsInChildren<BagItems>();

        int i = 0;

        foreach (BagItems b in _totalContents)
        {
            if (GameManager.GM.GetItemCount(i) > 0 && _dataManager.GetItemSubCategory(i) != "가방")
            {
                b.SetBagContent(i, _dataManager.GetItem(i).itemName, _dataManager.GetItemImage(i), _dataManager.GetDescription(i) ,_dataManager.GetItemCount(i));                
                b.gameObject.SetActive(true);
                i++;
                continue;
            }
            b.gameObject.SetActive(false);
            i++;
        }
    }
    
    public void SetWeaponBagContents()
    {
        if (_weaponContents == null) _weaponContents = _weaponContentsParent.GetComponentsInChildren<BagItems>();

        int i = 0;
        foreach (BagItems b in _weaponContents)
        {
            if (GameManager.GM.GetItemCount(i) > 0 && _dataManager.GetItemSubCategory(i) == "무기")
            {
                b.SetBagContent(i, _dataManager.GetItem(i).itemName, _dataManager.GetItemImage(i), _dataManager.GetDescription(i) ,_dataManager.GetItemCount(i));                
                b.gameObject.SetActive(true);
                i++;
                continue;
            }
            b.gameObject.SetActive(false);
            i++;
        }
    }

    public void SetLightBagContents()
    {
        if (_lightContents == null) _lightContents = _lightContentsParent.GetComponentsInChildren<BagItems>();

        int i = 0;
        foreach (BagItems b in _lightContents)
        {
            if (GameManager.GM.GetItemCount(i) > 0 && _dataManager.GetItemSubCategory(i) == "라이트")
            {
                b.SetBagContent(i, _dataManager.GetItem(i).itemName, _dataManager.GetItemImage(i), _dataManager.GetDescription(i) ,_dataManager.GetItemCount(i));                
                b.gameObject.SetActive(true);
                i++;
                continue;
            }
            b.gameObject.SetActive(false);
            i++;
        }
    }

    public void SetFoodBagContents()
    {
        if (_foodContents == null) _foodContents = _foodContentsParent.GetComponentsInChildren<BagItems>();

        int i = 0;
        foreach (BagItems b in _foodContents)
        {
            if (GameManager.GM.GetItemCount(i) > 0 && _dataManager.GetItemSubCategory(i) == "음식")
            {
                b.SetBagContent(i, _dataManager.GetItem(i).itemName, _dataManager.GetItemImage(i), _dataManager.GetDescription(i) ,_dataManager.GetItemCount(i));                
                b.gameObject.SetActive(true);
                i++;
                continue;
            }
            b.gameObject.SetActive(false);
            i++;
        }
    }

    public void SetMedicineBagContents()
    {
        if (_medicineContents == null) _medicineContents = _medicineContentsParent.GetComponentsInChildren<BagItems>();

        int i = 0;
        foreach (BagItems b in _medicineContents)
        {
            if (GameManager.GM.GetItemCount(i) > 0 && _dataManager.GetItemSubCategory(i) == "치료제")
            {
                b.SetBagContent(i, _dataManager.GetItem(i).itemName, _dataManager.GetItemImage(i), _dataManager.GetDescription(i) ,_dataManager.GetItemCount(i));                
                b.gameObject.SetActive(true);
                i++;
                continue;
            }
            b.gameObject.SetActive(false);
            i++;
        }
    }

    public void SetImportantBagContents()
    {
        if (_importantContents == null) _importantContents = _importantContentsParent.GetComponentsInChildren<BagItems>();

        int i = 0;
        string category;
        foreach (BagItems b in _importantContents)
        {
            category = _dataManager.GetItemSubCategory(i);
            if (GameManager.GM.GetItemCount(i) > 0 && (category == "스마트폰" || category == "침낭" || category == "보조배터리" || category == "카드키"))
            {
                b.SetBagContent(i, _dataManager.GetItem(i).itemName, _dataManager.GetItemImage(i), _dataManager.GetDescription(i) ,_dataManager.GetItemCount(i));                
                b.gameObject.SetActive(true);
                i++;
                continue;
            }
            b.gameObject.SetActive(false);
            i++;
        }
    }
    
}
