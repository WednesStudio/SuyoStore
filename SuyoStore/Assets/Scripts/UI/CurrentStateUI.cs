using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrentStateUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _dateText;
    [SerializeField] TextMeshProUGUI _locationText;

    public void SetCurrentState(string date, string location)
    {
        _dateText.text = date;
        _locationText.text = location;
    } 
}
