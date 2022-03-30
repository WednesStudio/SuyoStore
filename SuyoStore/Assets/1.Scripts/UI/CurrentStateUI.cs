using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrentStateUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _dateText;
    [SerializeField] TextMeshProUGUI _locationText;
    private string _date, _location;

    public void GetCurrentState(string date, string location)
    {
        _date = date;
        _location = location;
    }

    private void SetCurrentState()
    {
        _dateText.text = _date;
        _locationText.text = _location;
    } 
}
