using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterStatusUI : MonoBehaviour
{
    //character info script
    //[SerializeField] private GameObject _character;
    //private CharacterInfo;
    [SerializeField] Slider _staminaBar, _protectBar, _staietyBar, _fatigueBar;
    [SerializeField] TextMeshProUGUI _speedText, _attackPowerText;
    private int _speed, _attackPower;
    private float _staminaValue = 1.0f, _protectValue = 1.0f, _satietyValue = 1.0f, _fatigueValue = 1.0f;
    private bool _isProtected = false;

    private void Start() 
    {
        //CharacterInfo = _character.GetComponent<CharacterInfo>();
    }

    //Status_Stamina
    public void GetStamina(float hp, float hpMax)
    {
        _staminaValue = hp / hpMax;
    }

    public void GetProtect(bool isProtected, float protect, float protectMax)
    {
        _isProtected = isProtected;
        _protectValue = protect / protectMax;
    }

    private void SetStaminaBar()
    {
        if(_staminaValue < 1.0f) _staminaBar.value = _staminaValue;
    }

    private void SetProtectBar()
    {
        if(_isProtected)
        {
            _protectBar.gameObject.SetActive(true);
            if(_protectValue < 1.0f) _protectBar.value = _protectValue;   
        }
        if(_protectValue < 0.0f)
        {
            _protectBar.gameObject.SetActive(false);
            _isProtected = false;
        }     
    }

    //Status_Satiety
    public void GetSatiety(float satiety, float satietyMax)
    {
        _satietyValue = satiety / satietyMax;
    }

    private void SetSatietyBar()
    {
        if(_satietyValue < 1.0f) _staietyBar.value = _satietyValue;
    }

    //Status_Fatigue
    public void GetFatigue(float fatigue, float fatigueMax)
    {
        _fatigueValue = fatigue / fatigueMax;
    }

    private void SetFatigueBar()
    {
        if(_fatigueValue < 1.0f) _fatigueBar.value = _fatigueValue;
    }

    //Status_Speed
    public void GetSpeed(int speed)
    {
        _speed = speed;
    }

    private void SetSpeed()
    {
        if(_speed < 100) _speedText.text = _speed.ToString();
    }

    //Status_Attack Power
    public void GetAttackPower(int attackPower)
    {
        _attackPower = attackPower;
    }

    private void SetAttackPower()
    {
        if(_attackPower != 50) _attackPowerText.text = _attackPower.ToString();
    }
}
