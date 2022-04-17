using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BagControl : MonoBehaviour
{
    public GameObject inventoryWeight;
    private TextMeshProUGUI text;
    private void Awake()
    {
        LoadExcel database = FindObjectOfType<LoadExcel>();
        text = inventoryWeight.GetComponent<TextMeshProUGUI>();
    }
    public void SetCapacity(int capacity)
    {
        int currentCapacity = 0;
        string totalCapacity = currentCapacity.ToString() + " / " + capacity.ToString();
        text.text = totalCapacity;
    }
}
