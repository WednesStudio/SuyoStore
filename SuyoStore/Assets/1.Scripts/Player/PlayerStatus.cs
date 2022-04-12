using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    PlayerController playerController;

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


    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    // 현재 상태가 Max를 넘지 않게
    void RemainStatusValue(int curVal, int maxVal)
    {
        if (curVal >= maxVal) curVal = maxVal;
    }


    public virtual void Die()
    {
        Debug.Log(transform.name + " died.");
    }

    /// <summary>
    /// Hp Status
    /// </summary>
    public void HpModifier(bool isAttack, int zomPower)
    {
        RemainStatusValue(curHp, maxHp);

        if (isAttack)
        {
            curHp -= zomPower;
        }

        // 게임 오버 기준
        if (curHp <= 0)
        {
            Die();
        }
    }

    public void HpRecovery()
    {
        // 회복
        // 치료제 아이템 사용
        // 가구, 침낭 아이템 사용
    }

  
    /// <summary>
    /// Satiety Status
    /// </summary>
    public void SatietyModifier()
    {
        RemainStatusValue(curSatiety, maxSatiety);

        if (curSatiety <= 0)
        {
            /*일정 시간 후*/
            Die();
        }

        // 시간에 따라 감소 -- if문 조건 수정 필요
        if(time % hungerTime == 0)
        {
            curSatiety-= 2;
        }
    }

    public void RecoverySatiety()
    {
        // 아이템 사용
        // 하루 스킵
    }

    public void FatiqueModifier()
    {
        RemainStatusValue(curFatigue, maxFatique);

        if (curFatigue <= 0)
        {
            Die();
        }
        if (curFatigue <= 0)
        {
            curAttack = 0;
        }
        else if (curFatigue <= 10)
        { 
            curAttack -= 10;
            moveSpeed -= 8;
        }
        else if (curFatigue <= 20)
        { 
            curAttack -= 4;
            moveSpeed -= 4;
        }
        else if (curFatigue <= 30)
        { 
            curAttack -= 2;
            moveSpeed -= 2;
        }

    }

    public void AttackModifier(int itemAttack)
    {
        attack += itemAttack;
        curAttack = attack;
        if (curAttack <= 0) 
        {
            // 공격 못함
        }
        //공격 애니메이션


    }

    public void SpeedModifier()
    {
        int excessBag = (int)(maxCarryingBag * 10 / 100); // 10% 초과량

        if(curCarryingBag > maxCarryingBag)
        {
            int count = (curCarryingBag - maxCarryingBag) % excessBag;
            moveSpeed -= 2 * count;
        }
    }

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






    //private float speed; // 스피드

    //public void SetSpeed(float speed)
    //{
    //    if (speed > 0) this.speed = speed;
    //}
    //public float GetSpeed() {
    //    return speed;
    //}
}
