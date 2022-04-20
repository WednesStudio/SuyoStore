using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterStatusUI : MonoBehaviour
{
    //character info script
    [SerializeField] Slider _staminaBar, _staietyBar, _fatigueBar;
    [SerializeField] TextMeshProUGUI _speedText, _attackPowerText;
    [SerializeField] GameObject _debuffPrefab;
    [SerializeField] GameObject _debuffObject;
    [SerializeField] Sprite[] _debuffImages;
    [SerializeField] TextMeshProUGUI _staminaText, _satietyText, _fatigueText, _attackText, _spText;
    private float _staminaValue = 1.0f, _satietyValue = 1.0f, _fatigueValue = 1.0f;
    private List<int> _debuffTypeList = new List<int>();
    private List<GameObject> _debuffList = new List<GameObject>();

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
        else _attackText.text = "Attack Power : " + attackPower.ToString() + "()";
    }

    /// <summary> Get information of the debuff type and set prefab to 'Player Status' /// </summary>
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
