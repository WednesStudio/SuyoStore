using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Status
{
    PlayerController playerController;
    public bool isEquipWeapon = false;
    public bool isInfect = false;
    float dotInfectTimer = 10.0f;
    float timer = 0.0f;
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        // Status Initial Value
        // Speed
        walkSpeed = 3.0f;
        runAddSpeed = 3.0f;
        sitSpeed = 2.0f;

        // Status
        maxHp = 100;
        maxSatiety = 100;
        maxFatigue = 100;

        curHp = 100;
        curSatiety = 50;
        curFatigue = 50;

        // Ability
        maxCarryingBag = 30;
        attack = 10;
        stamina = 100;

        curCarryingBag = 30;
        curAttack = 10;
        curStamina = 100;

        // Time related status
        time = 100;
        hungerTime = 60;
        hungerDieTime = 120;
        useHungerTime = hungerTime;
        useHungerDieTime = hungerDieTime;
        staminaTime = 1;
        useStaminaTime = staminaTime;
        recoveryStaminaTime = 1;
        useRecoveryStaminaTime = recoveryStaminaTime;
        //sturnStaminaTime = 5;
        //useSturnStaminaTime = sturnStaminaTime;

    }

    private void Update()
    {
        if (curHp <= 0)
        {
            Die();
            Debug.Log("[GAME OVER] HP is ZERO");
        }
        SatietyModifier();
        SpeedModifier();

        if (isInfect)
        {
            //10초마다 hp -1
            timer += Time.deltaTime;
            if(timer >= dotInfectTimer)
            {
                ReduceHp(1);
                timer = 0.0f;
            }
        }
    }

    public virtual void Die()
    {
        playerController.state = PlayerController.PlayerState.Dead;
        GameManager.GM.GameOver();
    }

    /// <summary> Hp Status </summary>
    public void ReduceHp(int zomPower)
    {
        CurHp -= zomPower;  //CurHp = ReduceStatus(eCurStatusType.cHp, zomPower);
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
            UseHungerDieTime -= Time.deltaTime;
            if(useHungerDieTime <= 0)
            {
                Die();
                Debug.Log("[GAME OVER] Player is Hungry T^T");

                UseHungerDieTime = GetBackTime(UseHungerDieTime, hungerDieTime);
            }
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
        int excessBag = (int)(maxCarryingBag * 10 / 100); // 10% over weight
        int count = (curCarryingBag - maxCarryingBag) / excessBag;

        if (curCarryingBag >= maxCarryingBag)
        {
            CurSpeed = walkSpeed - 2 * count;
        }

        if(CurSpeed <= 0)
        {
            CurSpeed = 1;
        }
    }

    /// <summary> Stamina Status </summary>
    public void UseStamina(int _value)
    {
        UseStaminaTime -= Time.deltaTime;
        if (UseStaminaTime <= 0)
        {
            CurStamina -= _value;
            UseStaminaTime = GetBackTime(UseStaminaTime, StaminaTime);
        }
        if(CurStamina <= 0)
        {
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

    // 장비
    public List<int> EquipItemsList = new List<int>();

    public void AddEquipItem(int _itemID)
    {
        EquipItemsList.Add(_itemID);
        if(GameManager.GM.GetItemSubCategory(_itemID) == "무기")    playerController.hasWeapon = true;
    }

    public void RemoveEquipItem(int _itemID)
    {
        EquipItemsList.Remove(_itemID);
        if(GameManager.GM.GetItemSubCategory(_itemID) == "무기")    playerController.hasWeapon = false;
        //공격력 원래대로
        curAttack = 10;
        //시야 감소
    }
}
