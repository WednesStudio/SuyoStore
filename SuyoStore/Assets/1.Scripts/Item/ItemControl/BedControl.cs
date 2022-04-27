using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BedControl : MonoBehaviour
{
    public GameObject canvas;
    public Button sleepUIButton;
    public Button closeUIButton;
    public PlayerStatus playerStatus;
    // // camera settings
    // private Camera _mainCamera;
    // private Renderer _renderer;
    // private Ray _ray;
    // private RaycastHit _hit;
    // // 
    private bool isReady = false;
    private DataManager _dataManager;
    private Item item = null;
    private Item bed;
    private bool isNearPlayer = false;

    void Start()
    {
        playerStatus = FindObjectOfType<PlayerStatus>();
        // _mainCamera = Camera.main;
        // _renderer = GetComponent<Renderer>();
        _dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        SetItem();
        sleepUIButton.onClick.RemoveAllListeners();
        closeUIButton.onClick.RemoveAllListeners();
        sleepUIButton.onClick.AddListener(() =>
        {

            if (_dataManager.dateControl.GetDays() < 7)
                UseBed(item);
            else
                GameManager.GM.SetEndEventTrigger();
            canvas.SetActive(false);
        });
        closeUIButton.onClick.AddListener(() => canvas.SetActive(false));
    }
    void SetItem()
    {
        item = GetComponent<ItemControl>().item;
        if (item != null)
        {
            bed = new Item(item.GetItemName(), item.GetAttributes());
            isReady = true;
        }
    }
    void Update()
    {
        if (isReady == false)
            SetItem();
        else
        {
            if (_dataManager.dateControl.GetDays() < 8 && Input.GetKeyUp(KeyCode.G) && isNearPlayer)
            {
                // _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                // if (Physics.Raycast(_ray, out _hit, 1000f))
                canvas.SetActive(true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            isNearPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isNearPlayer = false;
        }
    }
    private void UseBed(Item item)
    {
        playerStatus.RecoverStatus(Status.eCurStatusType.cHp, item.GetHEAL());
        playerStatus.RecoverStatus(Status.eCurStatusType.cSatiety, item.GetSATIETY());
        GameManager.GM.DateSetting();
    }
}