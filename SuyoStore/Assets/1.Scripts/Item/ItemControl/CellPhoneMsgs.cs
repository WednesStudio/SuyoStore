using System.Diagnostics;
using System;
using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CellphoneUI
{
    public class Messages
    {
        public string message = "문자 알람";
    }
    public class CellPhoneMsgs : MonoBehaviour
    {
        public GameObject gameUI;
        public GameObject canvas;
        public Text infoText;
        public Button closeUIButton;
        private LoadJson list;
        private DateControl dateControl;

        Messages msg = new Messages();

        public static CellPhoneMsgs Instance;
        void Awake()
        {
            Instance = this;
            infoText.text = msg.message;
            //close event listener
            closeUIButton.onClick.RemoveAllListeners();
            closeUIButton.onClick.AddListener(Hide);
        }
        void Start()
        {
            list = FindObjectOfType<LoadJson>();
            dateControl = FindObjectOfType<DateControl>();
        }
        public CellPhoneMsgs SetMsg(string message)
        {
            msg.message = message;
            return Instance;
        }

        public void Show()
        {
            for (int i = 0; i < list.messageDatabase.Count; i++)
            {
                if (dateControl.GetDays() == list.messageDatabase[i].days)
                {
                    SetMsg(list.messageDatabase[i].message);
                    infoText.text = msg.message;
                    break;
                }
            }
            gameUI.SetActive(false);
            canvas.SetActive(true);
        }
        public void Hide()
        {
            gameUI.SetActive(true);
            canvas.SetActive(false);
            // reset msg
            // msg = new Messages();
        }
    }
}