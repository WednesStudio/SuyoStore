using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BagItems : MonoBehaviour
{
    [SerializeField] Image _profileImage = null;
    [SerializeField] TextMeshProUGUI _itemName = null;
    [SerializeField] TextMeshProUGUI _description = null;
    [SerializeField] TextMeshProUGUI _itemCount = null;
    [SerializeField] Button _button = null;
    public void SetBagContent(int id, string name, Sprite sprite, string description, int count)
    {
        this._itemName.text = name;
        this._profileImage.sprite = sprite;
        this._description.text = description;
        this._itemCount.text = count.ToString();
        this._button.onClick.AddListener(() => SelectButton(id));
    }
    public void SelectButton(int id)
    {
        GameManager.GM.CheckUseItem(id);
    }
}
