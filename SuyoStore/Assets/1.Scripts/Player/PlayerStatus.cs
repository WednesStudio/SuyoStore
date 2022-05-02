using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Status
{
    GameObject player;
    PlayerController playerController;
    UIManager uiManager;
    public bool isEquipWeapon = false;
    public bool isInfect = false;
    float dotInfectTimer = 10.0f;
    float timer = 0.0f;
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        uiManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        // Status Initial Value
        // Speed
        walkSpeed = 3.0f;
        runAddSpeed = 3.0f;
        sitSpeed = 2.0f;
        curSpeed = WalkSpeed;

        // Status
        maxHp = 100;
        maxSatiety = 100;
        maxFatigue = 100;

        curHp = 70;
        curSatiety = 80;
        curFatigue = 100;

        // Ability
        attack = 10;
        stamina = 100;
        carryingBag = 30;
        maxCarryingBag = carryingBag;

        curCarryingBag = 0;
        curAttack = 10;
        curStamina = 100;

        // Time related status
        time = 100;
        hungerTime = 10;
        hungerDieTime = 120;
        useHungerTime = hungerTime;
        useHungerDieTime = hungerDieTime;
        staminaTime = 2;
        useStaminaTime = staminaTime;
        recoveryStaminaTime = 1;
        useRecoveryStaminaTime = recoveryStaminaTime;
        //sturnStaminaTime = 5;
        //useSturnStaminaTime = sturnStaminaTime;

    }

    private void Update()
    {
        if (GameManager.GM.isGameStart)
        {
            if (curHp <= 0)
            {
                Die();
            }
            SatietyModifier();
            SpeedModifier();

            if (isInfect)
            {
                // setActive debuff UI
                uiManager.GetComponent<CharacterStatusUI>().SetDebuff(DebuffType.ZombieAttack, true);
                //10초마다 hp -1
                timer += Time.deltaTime;
                if (timer >= dotInfectTimer)
                {
                    ReduceHp(1);
                    timer = 0.0f;
                }
            }
            else
            {
                uiManager.GetComponent<CharacterStatusUI>().SetDebuff(DebuffType.ZombieAttack, false);
            }

        }
    }

    public virtual void Die()
    {
        playerController.state = PlayerController.PlayerState.Dead;
        GameManager.GM.GameOver();
    }

    /// <summary> Hp Status </summary>
    public void ReduceHp(int hitValue)
    {
        CurHp -= hitValue;
        CurHp = RemainStatusValue(CurHp, MaxHp);
    }

    public void RecoverStatus(eCurStatusType _statusType, int _value)
    {
        switch (_statusType)
        {
            case eCurStatusType.cHp:
                CurHp += _value;
                CurHp = RemainStatusValue(CurHp, MaxHp);
                break;
            case eCurStatusType.cSatiety:
                CurSatiety += _value;
                CurSatiety = RemainStatusValue(CurSatiety, MaxSatiety);
                break;
            case eCurStatusType.cFatigue:
                CurFatigue += _value;
                CurFatigue = RemainStatusValue(CurFatigue, MaxFatigue);
                break;
            //case eCurStatusType.cCarryingBag:
            //    CurCarryingBag += _value;
            //    CurCarryingBag = RemainStatusValue(CurCarryingBag, MaxCarryingBag);
            //    break;
            //case eCurStatusType.cAttack:
            //    CurAttack += _value;
            //    CurAttack = RemainStatusValue(CurAttack, Attack);
            //    break;
            //case eCurStatusType.cStamina:
            //    CurStamina += _value;
            //    CurStamina = RemainStatusValue(CurStamina, Stamina);
            //    break;
            default:
                break;
        }
    }

    void ReduceStatus(eCurStatusType _statusType, int _value)
    {
        switch (_statusType)
        {
            case eCurStatusType.cHp:
                CurHp -= _value;
                CurHp = RemainStatusValue(CurHp, MaxHp);
                break;
            case eCurStatusType.cSatiety:
                CurSatiety -= _value;
                CurSatiety = RemainStatusValue(CurSatiety, MaxSatiety);
                break;
            case eCurStatusType.cFatigue:
                CurFatigue -= _value;
                CurFatigue = RemainStatusValue(CurFatigue, MaxFatigue);
                break;
            default:
                break;
        }
    }


    /// <summary> Satiety Status </summary>
    public void SatietyModifier()
    {
        CurSatiety = RemainStatusValue(CurSatiety, MaxSatiety);

        // GameOver
        if (curSatiety <= 0)
        {
            uiManager.GetComponent<CharacterStatusUI>().SetDebuff(DebuffType.SatietyEffect, true);
            UseHungerDieTime -= Time.deltaTime;
            if (useHungerDieTime <= 0)
            {
                Die();
                Debug.Log("[GAME OVER] Player is Hungry T^T");

                UseHungerDieTime = GetBackTime(UseHungerDieTime, hungerDieTime);
            }
        }
        else
        {
            uiManager.GetComponent<CharacterStatusUI>().SetDebuff(DebuffType.SatietyEffect, false);
        }

        UseHungerTime -= Time.deltaTime;
        if (useHungerTime <= 0)
        {
            CurSatiety -= 2;
            Debug.Log("[Status System] Satiety : " + curSatiety);

            UseHungerTime = GetBackTime(UseHungerTime, hungerDieTime);
        }
    }

    /// <summary> Attack Status </summary>
    public void AttackModifier(int _itemAttack, int _decreaseValue)
    {
        //Item item
        CurAttack = attack + _itemAttack; // (itemAttack==item.itemAttack)

        if (curAttack <= 0)
        {
            // 공격 못함
        }

        if (curSatiety <= 10)
        {
            CurAttack = Attack + _itemAttack;
            CurAttack -= 10;
        }
        else if (curSatiety <= 20)
        {
            CurAttack = Attack + _itemAttack;
            CurAttack -= 4;
        }
        else if (curSatiety <= 30)
        {
            CurAttack = Attack + _itemAttack;
            CurAttack -= 2;
        }
        else {
            CurAttack = attack + _itemAttack;
        }
    }

    /// <summary> Speed Status </summary>
    public void SpeedModifier()
    {
        int excessBag = (int)(maxCarryingBag * 10 / 100); // max의 10% 값
        int count = (curCarryingBag - maxCarryingBag) / excessBag;

        /*
         (40 - 30) / 3 = 3
        30 = 3
        33 = 1
        36
         */

        if (curCarryingBag >= maxCarryingBag)
        {
            uiManager.GetComponent<CharacterStatusUI>().SetDebuff(DebuffType.SpeedLow, true);
            CurSpeed = walkSpeed - (0.5f) * count;
        }
        else
        {
            uiManager.GetComponent<CharacterStatusUI>().SetDebuff(DebuffType.SpeedLow, false);
        }

        if(CurSpeed <= 1 && CurSpeed > 0)
        {
            Debug.Log("가방이 너무 무겁다. 더 무거워진다면 위험할 것 같다.");
        }
        else if (CurSpeed <= 0)
        {
            Debug.Log("[Game Over] 무겁다. 척추가 끊어졌다. 눈앞이 캄캄하다. - 페이드아웃");
            CurSpeed = 0;
            Die();
        }
    }

    /// <summary> Stamina Status </summary>
    public void UseStamina(int _value)
    {
        // stamina타이머(UseStaminaTime)가 0이 될 때마다 스테미나가 1씩 감소
        // 감소 후 타이머가 원상 복귀 후 다시 타이머 카운트
        UseStaminaTime -= Time.deltaTime;
        if (UseStaminaTime <= 0)
        {
            CurStamina -= _value;
            UseStaminaTime = GetBackTime(UseStaminaTime, StaminaTime);
        }
        if(CurStamina <= 0)
        {
            // 스태미나가 0 밑으로 가려하면 0으로 계속 고정
            CurStamina = RemainStatusValue(curStamina, Stamina);
        }
    }

    public void RecoveryStamina(int _value)
    {
        if(CurStamina < Stamina)
        {
            UseRecoveryStaminaTime -= Time.deltaTime;
            if (useRecoveryStaminaTime <= 0)
            {
                CurStamina += _value;
                UseRecoveryStaminaTime = GetBackTime(UseRecoveryStaminaTime, RecoveryStaminaTime);
            }
            // 스태미나가 최대값보다 커지려고 하면 최대값으로 계속 고정
            CurStamina = RemainStatusValue(curStamina, Stamina);
        }
    }

    //void OnEquipmentChange(Item newItem, Item oldItem)
    //{
    //    if(newItem != null)
    //    {

    //    }

    //    if(oldItem != null)
    //    {

    //    }
    //}

    // EuipItemList
    //public void AddEquipItem(int _itemID)
    //{
    //    EquipWeaponList.Add(_itemID);
    //    if (GameManager.GM.GetItemSubCategory(_itemID) == "무기") playerController.hasWeapon = true;
    //}
    //public void RemoveEquipItem(int _itemID)
    //{
    //    EquipWeaponList.Remove(_itemID);
    //    if (GameManager.GM.GetItemSubCategory(_itemID) == "무기") playerController.hasWeapon = false;
    //    //공격력 원래대로
    //    curAttack = 10;
    //    //시야 감소
    //}

    // 장비
    public List<int> EquipWeaponList = new List<int>();

    public void AddEquipWeapon(int _itemID)
    {
        EquipWeaponList.Add(_itemID);
        if(GameManager.GM.GetItemSubCategory(_itemID) == "무기")    playerController.hasWeapon = true;
    }
    public void RemoveEquipWeapon(int _itemID)
    {
        EquipWeaponList.Remove(_itemID);
        if(GameManager.GM.GetItemSubCategory(_itemID) == "무기")    playerController.hasWeapon = false;
        //공격력 원래대로
        curAttack = 10;
        //시야 감소
    }

    // 라이트
    public List<int> EquipFlashlighList = new List<int>();
    public void AddEquipFlashlight(int _itemID)
    {
        EquipFlashlighList.Add(_itemID);
    }
    public void RemoveEquipFlashlight(int _itemID)
    {
        EquipFlashlighList.Remove(_itemID);
    }


    // 가방
    public List<int> EquipBagList = new List<int>();
    public void AddEquipBag(int _itemID)
    {
        EquipBagList.Add(_itemID);
    }
    public void RemoveEquipBag(int _itemID)
    {
        EquipBagList.Remove(_itemID);
        // 가방 무게 원래대로
        MaxCarryingBag = CarryingBag;
    }
}
