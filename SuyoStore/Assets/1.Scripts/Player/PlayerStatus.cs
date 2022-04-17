using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Status
{
    PlayerController playerController;

<<<<<<< HEAD
    // 스피드
    public float moveSpeed = 10.0f; // 기본 상태일 때 이동 속도
    public float runSpeed = 5.0f; // 달리기 상태일 때 이동 속도
    public float sitSpeed = 3.0f; // 앉기 상태일 떄 이동 속도

    // 스테이터스
    public int maxHp = 100; // 최대 체력
    public int maxSatiety = 100; // 최대 포만감
    public int maxFatique = 100; // 최대 피로도

    public int curHp = 10; // 현재 체력
    public int curSatiety = 50; // 현재 포만감
    public int curFatigue = 50; // 현재 피로도

    // 능력치
    public int maxCarryingBag = 30; // 기본 적재량
    public int attack = 10; // 기본 공격력
    public int stamina = 100; // 기본 지구력

    public int curCarryingBag = 0; // 기본 적재량
    public int curAttack = 10; // 현재 공격력
    public int curStamina = 100; // 현재 지구력

    // status와 관련된 시간
    private int time = 100; // 실시간
    private int hungerTime = 10; // 포만감이 감소하는 일정시간
=======
    public bool isGet = false;
>>>>>>> main

    //// �������� ���� 10, 20, 30������ �� ���ݷ��� �����ߴ��� ���� �Ǵ�
    //private bool[] isReduceAttack = { false, false, false }; 

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

<<<<<<< HEAD
    // 현재 상태가 Max를 넘지 않게
    void RemainStatusValue(int curVal, int maxVal)
=======
    private void Start()
>>>>>>> main
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

<<<<<<< HEAD
        // 게임 오버 기준
        if (curHp <= 0)
=======
        if (isAttack)
>>>>>>> main
        {
            curHp -= zomPower;
            Debug.Log("[Status System] HP : " + curHp);
        }
    }

    // --���� �߰� �ʿ�
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
<<<<<<< HEAD
            /*일정 시간 후*/
            Die();
=======
            UseHungerDieTime -= Time.deltaTime;
            if(useHungerDieTime <= 0)
            {
                Debug.Log("[GAME OVER] Player is Hungry�ФФФ�");
                Die();
            }
>>>>>>> main
        }
        GetBackTime(UseHungerDieTime, hungerDieTime);

<<<<<<< HEAD
        // 시간에 따라 감소 -- if문 조건 수정 필요
        if(time % hungerTime == 0)
=======
        // �д� 2����
        UseHungerTime -= Time.deltaTime;
        if (useHungerTime <= 0)
>>>>>>> main
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
<<<<<<< HEAD
        //공격 애니메이션

=======
>>>>>>> main

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
<<<<<<< HEAD
        int excessBag = (int)(maxCarryingBag * 10 / 100); // 10% 초과량
=======
        int excessBag = (int)(maxCarryingBag * 10 / 100); // 10% �ʰ���
        int count = (curCarryingBag - maxCarryingBag) / excessBag;
>>>>>>> main

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
<<<<<<< HEAD






    //private float speed; // 스피드

    //public void SetSpeed(float speed)
    //{
    //    if (speed > 0) this.speed = speed;
    //}
    //public float GetSpeed() {
    //    return speed;
    //}
=======
>>>>>>> main
}
