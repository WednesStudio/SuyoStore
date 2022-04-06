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
        public GameObject canvas;
        public Text infoText;
        public Button closeUIButton;
        private LoadJson list;

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
        }
        public CellPhoneMsgs SetMsg(string message)
        {
            msg.message = message;
            return Instance;
        }

        public void Show(string date)
        {
            for (int i = 0; i < list.messageDatabase.Count; i++)
            {
                if (date == list.messageDatabase[i].date)
                {
                    SetMsg(list.messageDatabase[i].message);
                    infoText.text = msg.message;
                    break;
                }
            }
            canvas.SetActive(true);
        }
        public void Hide()
        {
            canvas.SetActive(false);
            // reset msg
            msg = new Messages();
        }
    }
}