using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Status
{
    PlayerController playerController;

    public bool isGet = false;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        // Status Initial Value
        // Speed
        walkSpeed = 10.0f;
        runAddSpeed = 5.0f;
        sitSpeed = 3.0f;

        // Status
        maxHp = 100;
        maxSatiety = 100;
        maxFatigue = 100;

        curHp = 10;
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
        hungerTime = 60; // 60��
        hungerDieTime = 120; // 120��
        useHungerTime = hungerTime;
        useHungerDieTime = hungerDieTime;
        staminaTime = 1; // 1��
        useStaminaTime = staminaTime;
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died.");
        
    }

    /// <summary> Hp Status </summary>
    public void HpModifier(bool isAttack, int zomPower)
    {
        RemainStatusValue(curHp, maxHp);

        // GameOver
        if (curHp <= 0)
        {
            Debug.Log("[GAME OVER] HP is ZERO");
            Die();
        }

        if (isAttack)
        {
            curHp -= zomPower;
            Debug.Log("[Status System] HP : " + curHp);
        }
    }

    // -- Require more with Item Script
    public void HpRecovery()
    {
        // 회복
        // 치료제 아이템 사용
        // 가구, 침낭 아이템 사용
    }


    /// <summary> Satiety Status </summary>
    public void SatietyModifier()
    {
        RemainStatusValue(curSatiety, maxSatiety);

        // GameOver
        if (curSatiety <= 0)
        {
            UseHungerDieTime -= Time.deltaTime;
            if(useHungerDieTime <= 0)
            {
                Debug.Log("[GAME OVER] Player is Hungry�ФФФ�");
                Die();
            }
        }
        GetBackTime(UseHungerDieTime, hungerDieTime);

        // �д� 2����
        UseHungerTime -= Time.deltaTime;
        if (useHungerTime <= 0)
        {
            CurSatiety -= 2;
            Debug.Log("[Status System] Satiety : " + curSatiety);
        }
        GetBackTime(UseHungerTime, hungerTime);


    }

    // --���� �߰� �ʿ�
    public void RecoverySatiety()
    {
        // 아이템 사용
        // 하루 스킵
    }


    /// <summary> Fatigue Status </summary>
    public void FatigueModifier(int _decreaseValue, int _increaseValue)
    {
        RemainStatusValue(CurFatigue, MaxFatigue);

        CurFatigue -= _decreaseValue;
        CurFatigue += _increaseValue;

        // �Ĺ� : Fatigue--;
        // ��� : Fatigue -= 2;
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
    public void SpeedModifier(int _carryingBack, int _decreaseValue)
    {
        int excessBag = (int)(maxCarryingBag * 10 / 100); // 10% �ʰ���
        int count = (curCarryingBag - maxCarryingBag) / excessBag;

        if (curCarryingBag >= maxCarryingBag)
        {
            CurSpeed = walkSpeed - 2 * count;
        }
    }

    //public void BagWeight()
    //{

    //}

    /// <summary> Stamina Status </summary>
    public void StaminaModifier()
    {
        if(curSatiety <= 0)
        {
            // 걷기 상태로 전환
        }

        if (playerController.isMove == true)
        {
            //일정시간마다
            curStamina--;
        }
        else curStamina = stamina;
    }
}
