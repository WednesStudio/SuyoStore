using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] DataManager _dataManager;
    [SerializeField] GameObject _statusInventoryWindow;
    [SerializeField] Button[] _categoryButtons;
    [SerializeField] GameObject[] _itemScrollViews;
    [SerializeField] private GameObject _bagContentsParent = null;
    private BagItems[] _bagContents = null;

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
    }

    /// <summary> 인벤토리 속 아이템 관리 </summary>

    public void SetBagContents()
    {
        if (_bagContents == null) _bagContents = _bagContentsParent.GetComponentsInChildren<BagItems>();

        int i = 0;

        foreach (BagItems b in _bagContents)
        {
            if (GameManager.GM.GetItemCount(i) > 0)
            {
                // if (_dataManager.GetItem(i).MyType.Equals(ItemType.Catch))
                // {
                //     b.SetBagContent(i, _dataManager.GetItemImage(i), _dataManager.GetItem(i).Name, _dataManager.GetItemCount(i));
                //     i++;
                //     continue;
                // }
            }
            b.gameObject.SetActive(false);
            i++;
        }
    }
}
