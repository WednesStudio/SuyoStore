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
    [SerializeField] Slider _staminaBar, _staietyBar, _fatigueBar;
    [SerializeField] TextMeshProUGUI _speedText, _attackPowerText;
    [SerializeField] GameObject _debuffPrefab;
    [SerializeField] GameObject _debuffObject;
    [SerializeField] Sprite[] _debuffImages;
    [SerializeField] Text _staminaText, _satietyText, _fatigueText, _attackText, _spText;
    private int _speed, _attackPower;
    private float _hp, _satiety, _fatigue;
    private float _staminaValue = 1.0f, _satietyValue = 1.0f, _fatigueValue = 1.0f;
    private List<int> _debuffTypeList = new List<int>();
    private List<GameObject> _debuffList = new List<GameObject>();

    private void Start() 
    {
        //CharacterInfo = _character.GetComponent<CharacterInfo>();
    }

    //Main Bar Setting
    #region 
    //Status_Stamina
    public void GetStamina(float hp, float hpMax)
    {
        _hp = hp;
        _staminaValue = hp / hpMax;
    }

    private void SetStaminaBar()
    {
        if(_staminaValue < 1.0f) _staminaBar.value = _staminaValue;
        _staminaText.text = _hp.ToString();
    }

    //Status_Satiety
    public void GetSatiety(float satiety, float satietyMax)
    {
        _satiety = satiety;
        _satietyValue = satiety / satietyMax;
    }

    private void SetSatietyBar()
    {
        if(_satietyValue < 1.0f) _staietyBar.value = _satietyValue;
        _staminaText.text = _satiety.ToString();
    }

    //Status_Fatigue
    public void GetFatigue(float fatigue, float fatigueMax)
    {
        _fatigue = fatigue;
        _fatigueValue = fatigue / fatigueMax;
    }

    private void SetFatigueBar()
    {
        if(_fatigueValue < 1.0f) _fatigueBar.value = _fatigueValue;
        _fatigueText.text = _fatigue.ToString();
    }

    //Status_Speed
    public void GetSpeed(int speed)
    {
        _speed = speed;
    }

    private void SetSpeed()
    {
        if(_speed < 100) _speedText.text = _speed.ToString();
        _spText.text = _speed.ToString();
    }

    //Status_Attack Power
    public void GetAttackPower(int attackPower)
    {
        _attackPower = attackPower;
    }

    private void SetAttackPower()
    {
        if(_attackPower != 50) _attackPowerText.text = _attackPower.ToString();
        _attackText.text = _attackPower.ToString();
    }

    /// <summary>
    /// Get information of the debuff type and set prefab to 'Player Status'
    /// </summary>
    /// <param = "debuffType"> Get debuffType and set type of debuff object </param>
    /// <param = "isActive"> if 'True' : instantiate new debuff, else : remove the debuff of that type from the list </param>
    public void GetAndSetDebuff(int debuffType, bool isActive)
    {
        if(isActive)
        {
            GameObject debuff = Instantiate(_debuffPrefab, _debuffObject.transform.position, Quaternion.identity);
            debuff.transform.parent = _debuffObject.transform;
            debuff.GetComponent<Image>().sprite = _debuffImages[debuffType];
            _debuffList.Add(debuff);
            _debuffTypeList.Add(debuffType);
        }
        else
        {
            if(_debuffTypeList.Contains(debuffType))
            {
                int index = _debuffTypeList.IndexOf(debuffType);
                _debuffTypeList.Remove(debuffType);
                _debuffList.RemoveAt(index);
                int numOfList = _debuffList.Count;    
                
                for(int i = 0; i < numOfList; i++)
                {
                    Destroy(_debuffList[i]);
                }

                for(int i = 0; i < numOfList; i++)
                {
                    GameObject debuff = Instantiate(_debuffPrefab, _debuffObject.transform.position, Quaternion.identity);
                    debuff.transform.parent = _debuffObject.transform;
                    debuff.GetComponent<Image>().sprite = _debuffImages[_debuffTypeList[i]];
                    _debuffList.Add(debuff);
                }
            }
        }
    }

    #endregion


    //Status Setting 

    public void ChangeProfileImage()
    {
        //select image code
    }

    public void SetWeapon()
    {
        //set image - sprite
        //set name - text
        //set power - text
        //set durability - text
    }
    
    public void SetLight()
    {

    }

    public void SetBag()
    {

    }

}
