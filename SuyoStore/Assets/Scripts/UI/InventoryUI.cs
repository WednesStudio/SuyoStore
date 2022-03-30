using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject _statusInventoryWindow;
    [SerializeField] Button[] _categoryButtons;
    [SerializeField] GameObject[] _itemScrollViews;

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
}
