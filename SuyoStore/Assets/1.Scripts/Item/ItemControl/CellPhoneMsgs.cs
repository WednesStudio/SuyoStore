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
<<<<<<< HEAD
=======
        public GameObject gameUI;
>>>>>>> ba1e7674 ([BUG] merge error)
        public GameObject canvas;
        public Text infoText;
        public Button closeUIButton;
        private LoadJson list;
<<<<<<< HEAD
=======
        private DateControl dateControl;
>>>>>>> ba1e7674 ([BUG] merge error)

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
<<<<<<< HEAD
=======
            dateControl = FindObjectOfType<DateControl>();
>>>>>>> ba1e7674 ([BUG] merge error)
        }
        public CellPhoneMsgs SetMsg(string message)
        {
            msg.message = message;
            return Instance;
        }

<<<<<<< HEAD
        public void Show(string date)
        {
            for (int i = 0; i < list.messageDatabase.Count; i++)
            {
                if (date == list.messageDatabase[i].date)
=======
        public void Show()
        {
            for (int i = 0; i < list.messageDatabase.Count; i++)
            {
                if (dateControl.GetDays() == list.messageDatabase[i].days)
>>>>>>> ba1e7674 ([BUG] merge error)
                {
                    SetMsg(list.messageDatabase[i].message);
                    infoText.text = msg.message;
                    break;
                }
            }
<<<<<<< HEAD
=======
            gameUI.SetActive(false);
>>>>>>> ba1e7674 ([BUG] merge error)
            canvas.SetActive(true);
        }
        public void Hide()
        {
<<<<<<< HEAD
            canvas.SetActive(false);
            // reset msg
            msg = new Messages();
=======
            gameUI.SetActive(true);
            canvas.SetActive(false);
            // reset msg
            // msg = new Messages();
>>>>>>> ba1e7674 ([BUG] merge error)
        }
    }
}