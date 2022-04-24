using System.Diagnostics;
using System;
using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellPhoneMsgs : MonoBehaviour
{
    private DataManager dataManager;
    public GameObject canvas;
    public TextMeshProUGUI infoText;
    public Button closeUIButton;
    private LoadJson list;
    private string message;
    public static CellPhoneMsgs Instance;
    void Awake()
    {
        Instance = this;
        infoText.text = message;
        //close event listener
        closeUIButton.onClick.RemoveAllListeners();
        closeUIButton.onClick.AddListener(Hide);
    }
    void Start()
    {
        list = GameObject.Find("Reader").GetComponent<LoadJson>();
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
    }
    public CellPhoneMsgs SetMsg(string message)
    {
        this.message = message;
        return Instance;
    }

    public void Show()
    {
        for (int i = 0; i < list.messageDatabase.Count; i++)
        {
            if (dataManager.dateControl.GetDays() == list.messageDatabase[i].days)
            {
                SetMsg(list.messageDatabase[i].message);
                infoText.text = message;
                break;
            }
        }
        canvas.SetActive(true);
    }
    public void Hide()
    {
        canvas.SetActive(false);
    }
}
