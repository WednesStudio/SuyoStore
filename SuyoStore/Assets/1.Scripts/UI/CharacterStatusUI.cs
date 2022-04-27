using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum DebuffType
{
    ZombieAttack, SatietyEffect, SpeedLow
}
public class CharacterStatusUI : MonoBehaviour
{
    //character info script
    [Header("Top UI")]
    [SerializeField] Slider _staminaBar, _staietyBar, _fatigueBar;
    [SerializeField] TextMeshProUGUI _speedText, _attackPowerText;
    [Header("Debuff")]
    [SerializeField] GameObject[] _debuffs;
    [SerializeField] Sprite[] _debuffImages;
    [SerializeField] TextMeshProUGUI _staminaText, _satietyText, _fatigueText, _attackText, _spText;
    [Header("Weapon Status")]
    [SerializeField] Image _weaponImage;
    [SerializeField] TextMeshProUGUI _weaponNameText, _weaponPowerText, _weaponDurabilityText;
    [Header("Light Status")]
    [SerializeField] Image  _lightImage;
    [SerializeField] TextMeshProUGUI _lightNameText, _lightSightText, _lightDurabilityText;
    [Header("Bag Status")]
    [SerializeField] Image _bagImage;
    [SerializeField] TextMeshProUGUI _bagNameText, _bagCapacityText;
    private float _staminaValue = 1.0f, _satietyValue = 1.0f, _fatigueValue = 1.0f;
    

    //Main Bar Setting
    #region 
    //Status_Stamina
    public void SetStaminaBar(float hp, float hpMax)
    {
        _staminaBar.value = hp / hpMax;
        _staminaText.text = "Stamina : " + hp.ToString() + " / " + hpMax.ToString();
    }

    //Status_Satiety
    public void SetSatietyBar(float satiety, float satietyMax)
    {
        _staietyBar.value = satiety / satietyMax;
        _satietyText.text = "Satiety : " + satiety.ToString() + " / " + satietyMax.ToString();
    }

    //Status_Fatigue

    public void SetFatigueBar(float fatigue, float fatigueMax)
    {
        _fatigueBar.value = fatigue / fatigueMax;
        _fatigueText.text = "Fatigue : " + fatigue.ToString() + " / " + fatigueMax.ToString();
    }

    //Status_Speed
    public void SetSpeed(float speed, float varSpeed)
    {
        _speedText.text = ((int)varSpeed).ToString();
        if(speed == 10) _spText.text = "Speed : " + speed + " + (" + ((int)(varSpeed - speed)).ToString() + ")";
        else _spText.text = "Speed : 10 + (" + speed.ToString() +")";
    }

    //Status_Attack Power
    public void SetAttackPower(int attackPower)
    {
        _attackPowerText.text = attackPower.ToString();
        if(attackPower == 10) _attackText.text = "Attack Power : 10 + (0)";
        else _attackText.text = "Attack Power : 10 + " + "(" + (attackPower - 10).ToString() + ")";
    }

    public void SetDebuff(DebuffType type, bool isActive)
    {
        if(isActive)
        {
            _debuffs[(int)type].SetActive(true);
        }
        else _debuffs[(int)type].SetActive(false);
    }

    #endregion


    //Status Setting 

    public void ChangeProfileImage()
    {
        //select image code
    }

    public void SetWeapon(Sprite image, string name, Item item)
    {
        //set image - sprite
        _weaponImage.sprite = image;
        if(name == "")
        {
            _weaponNameText.text = "무기 없음";
        }
        //set name - text
        else    _weaponNameText.text = name;
        //set power - text
        if(item.GetATTACK() != 0)   _weaponPowerText.text = "무기 공격력 : " + item.GetATTACK().ToString();
        else _weaponPowerText.text = "";
        //set durability - text
        if(item.GetDURABILITY() != 0)   _weaponDurabilityText.text = "무기 내구도 : " + item.GetDURABILITY().ToString();
        else _weaponDurabilityText.text = "";
    }
    
    public void SetLight(Sprite image, string name, Item item)
    {
        _lightImage.sprite = image;
        if(name == "")
        {
            _lightNameText.text = "라이트 없음";
        }
        else    _lightNameText.text = name;
        if(item.GetSIGHTRANGE() != 0)    _lightSightText.text = "라이트 시야 : " + item.GetSIGHTRANGE().ToString();
        else _lightSightText.text = "";

        if(item.GetDURABILITY() != 0)   _lightDurabilityText.text = "라이트 내구도 : " + item.GetDURABILITY().ToString();
        else _lightDurabilityText.text = "";
    }

    public void SetBag(Sprite image, string name, Item item)
    {
        _bagImage.sprite = image;
        if(name == "")
        {
            _bagNameText.text = "가방 없음";
        }
        else    _bagNameText.text = name;
        if(item.GetCAPACITY() != 0)    _bagCapacityText.text = "가방 크기 : " + item.GetCAPACITY().ToString();

    }

}
