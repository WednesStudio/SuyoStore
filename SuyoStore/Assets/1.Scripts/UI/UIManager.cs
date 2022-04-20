using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject _character;
    private PlayerStatus _characterInfo;
    [Header("Other Scripts")]
    [SerializeField] CharacterStatusUI _characterStatusUI;
    [SerializeField] CurrentStateUI _currentStateUI;
    [SerializeField] OptionSettingUI _optionSettingUI;
    [SerializeField] InventoryUI _inventoryUI;

    [Header("UI Objects")]
    [SerializeField] GameObject _gameStartUI;
    [SerializeField] GameObject _inGameUI;
    [SerializeField] GameObject _mainCamera;

    private void Start() 
    {
        _character = GameObject.FindGameObjectWithTag("Player");
        _characterInfo = _character.GetComponent<PlayerStatus>();    
    }

    private void Update() 
    {
        _characterStatusUI.SetStaminaBar(_characterInfo.CurHp, _characterInfo.MaxHp);
        _characterStatusUI.SetSatietyBar(_characterInfo.CurSatiety, _characterInfo.MaxSatiety);
        _characterStatusUI.SetFatigueBar(_characterInfo.CurFatigue, _characterInfo.MaxFatigue);
        _characterStatusUI.SetSpeed(_characterInfo.WalkSpeed, _characterInfo.CurSpeed);
        _characterStatusUI.SetAttackPower(_characterInfo.CurAttack);
    }
    public void GameStartUI()
    {
        _gameStartUI.SetActive(false);
        _inGameUI.SetActive(true);
        _mainCamera.SetActive(true);
        GameManager.GM.GameStart();
    }

    public void ExitGameUI()
    {
        //Fade out
        GameManager.GM.ExitGame();
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
        _inventoryUI.SetBatteryBagContents();
        _inventoryUI.SetFoodBagContents();
        _inventoryUI.SetLightBagContents();
        _inventoryUI.SetWeaponBagContents();
        _inventoryUI.SetMedicineBagContents();
        _inventoryUI.SetSleepingBagContents();
        _inventoryUI.SetImportantBagContents();
    }

    public void CheckUseItem(string name)
    {
        _inventoryUI.OnCheckItemUseWindow(name);
    }
}
