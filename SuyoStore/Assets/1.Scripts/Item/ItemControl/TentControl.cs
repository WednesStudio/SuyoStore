using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentControl : MonoBehaviour
{
    private Camera _mainCamera;
    private Renderer _renderer;
    private Ray _ray;
    private RaycastHit _hit;
    //
    private bool isReady = false;
    private DataManager _dataManager;
    private Item item = null;
    private Item tent;
    void Start()
    {
        _mainCamera = Camera.main;
        _renderer = GetComponent<Renderer>();
        _dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        SetItem();
    }
    void SetItem()
    {
        item = GetComponent<ItemControl>().item;
        if (item != null)
        {
            tent = new Item(item.GetItemName(), item.GetAttributes());
            isReady = true;
        }
    }
    void Update()
    {
        if (isReady == false)
            SetItem();
        else
        {
            if (_dataManager.dateControl.GetDays() < 7 && Input.GetMouseButtonDown(0))
            {
                _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(_ray, out _hit, 1000f))
                    UseTent(tent);
            }
        }
    }

    private void UseTent(Item item)
    {
        print("== UseHeal/Food error ==");
        // itemUse.UseHeal(item.GetHEAL());
        // itemUse.UseFood(item.GetSATIETY());
        GameManager.GM.DateSetting();
    }
}
