using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CellphoneUI;

public class Demo : MonoBehaviour
{
    public string date;
    public Button demoButton;
    // Start is called before the first frame update
    void Awake()
    {
        demoButton.onClick.RemoveAllListeners();
        demoButton.onClick.AddListener(() =>
        {
            CellPhoneMsgs.Instance.Show(date);
        });
    }
}
