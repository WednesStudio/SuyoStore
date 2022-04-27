using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject _character;
    private PlayerStatus _characterInfo;
    [Header("Other Scripts")]
    [SerializeField] CharacterStatusUI _characterStatusUI;
    [SerializeField] CurrentStateUI _currentStateUI;
    [SerializeField] OptionSettingUI _optionSettingUI;
    [SerializeField] ItemUse _itemUse;
    [SerializeField] InventoryUI _inventoryUI;
    [SerializeField] DataManager _dataManager;

    [Header("UI Objects")]
    [SerializeField] GameObject _gameStartUI;
    private bool _overCapacity;
    private int _debuffSpeed;

    private void Start() 
    {
        _character = GameObject.FindGameObjectWithTag("Player");
        _characterInfo = _character.GetComponent<PlayerStatus>();
    }

    private void Update() 
    {
        if(_characterInfo != null)
        {
          _characterStatusUI.SetStaminaBar(_characterInfo.CurHp, _characterInfo.MaxHp);
          _characterStatusUI.SetSatietyBar(_characterInfo.CurSatiety, _characterInfo.MaxSatiety);
          _characterStatusUI.SetFatigueBar(_characterInfo.CurFatigue, _characterInfo.MaxFatigue);
          if(_overCapacity)
          {
              _characterInfo.CurSpeed -= _debuffSpeed;
          }
          _characterStatusUI.SetSpeed(_characterInfo.WalkSpeed, _characterInfo.CurSpeed);
          _characterStatusUI.SetAttackPower(_characterInfo.CurAttack);
        }
    }
    public void GameStartUI()
    {
        GameManager.GM.GameStart();
        _gameStartUI.SetActive(false);
        SoundManager.SM.PlaySfxSound(SfxSoundName.ButtonClick);
    }

    public void ExitGameUI()
    {
        //Fade out
        GameManager.GM.ExitGame();
        SoundManager.SM.PlaySfxSound(SfxSoundName.ButtonClick);
    }

    public void SetTopBarUI(float hp, float satiety, float fatigue, int speed, int attackPower)
    {
        _characterStatusUI.SetStaminaBar(hp, 100);
        _characterStatusUI.SetSatietyBar(satiety, 100);
        _characterStatusUI.SetFatigueBar(fatigue, 100);
        _characterStatusUI.SetSpeed(speed, 0);
        _characterStatusUI.SetAttackPower(attackPower);
    }

    public void SetCurrentStateUI(string date, string location)
    {
        _currentStateUI.SetCurrentState(date, location);
    }

    public void SetInitialInventory()
    {
        _inventoryUI.SetTotalBagContents();
        _inventoryUI.SetFoodBagContents();
        _inventoryUI.SetLightBagContents();
        _inventoryUI.SetWeaponBagContents();
        _inventoryUI.SetMedicineBagContents();
        _inventoryUI.SetImportantBagContents();
    }

    public void SetPlayerSpeed(int curCapacity, int maxCapacity)
    {
        double excessRate = (double)curCapacity / (double) maxCapacity;
        if(excessRate >= 1.0f)
        {
            _overCapacity = true;
            _debuffSpeed = ((int)excessRate * 2);
        }
        else
        {
            _overCapacity = false;
            _debuffSpeed = 0;
        } 
    }

    public void CheckUseItem(string name)
    {
        _inventoryUI.OnCheckItemUseWindow(name);
    }

    public void ChangeWeaponState(GameObject obj)
    {
        GameObject parent = obj.transform.parent.gameObject;
        string name = parent.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
        _itemUse.ChangeItem(_dataManager.GetItemID(name), -1);
    }

    public void SetCurrentItemStatus(int id, Item item)
    {
        if(item.GetItemName() == "무기")
        {
            if(id == -1)
            {
                _characterStatusUI.SetWeapon(null, "", item);
            }
            else    _characterStatusUI.SetWeapon(_dataManager.GetItemImage(id), _dataManager.GetItemName(id), item);
        }   
        else if(item.GetItemName() == "라이트")
        {
            if(id == -1)
            {
                _characterStatusUI.SetLight(null, "", item);
            }
            else    _characterStatusUI.SetLight(_dataManager.GetItemImage(id), _dataManager.GetItemName(id), item);
        }     
        else if(item.GetItemName() == "가방")
        {
            if(id == -1)
            {
                _characterStatusUI.SetBag(null, "", item);
            }
            else    _characterStatusUI.SetBag(_dataManager.GetItemImage(id), _dataManager.GetItemName(id), item);
        }       
        
    }
}
