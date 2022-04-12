using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private readonly List<Status> statModifiers;

    public PlayerController playerController;

    public int maxHp = 100; // 최대 체력
    public int maxSatiety = 100; // 최대 포만감
    public int maxFatique = 100; // 최대 피로도

    public int attack = 100; // 기본 공격력
    public int stamina = 100; // 기본 지구력
    //public int carryingBag = 30; // 기본 적재량

    public int currentHp = 10; // 현재 체력
    public int currentSatiety; // 현재 포만감
    public int currentFatigue; // 현재 피로도
    public int currentAttack; // 현재 공격력
    public int currentStamina; // 현재 지구력

    void Update()
    {

    }


    public void HpModifier(bool isAttack)
    {
        if (isAttack)
        {

        }
    }

    public void HpModifier()
    {

    }
    public void SatietyModifier()
    {

    }

    public void FatiqueModifier()
    {

    }
    public void AttackModifier(int itemAttack)
    {
        attack += itemAttack;
        currentAttack = attack;
        if (currentAttack <= 0) 
        {
            // 공격 못함
        }
        //공격 애니메이션
        if (currentFatigue <= 0) currentAttack = 0;
        else if (currentFatigue <= 10) currentAttack -= 10;
        else if (currentFatigue <= 20) currentAttack -= 4;
        else if (currentFatigue <= 30) currentAttack -= 2;
    }

    public void StaminaModifier()
    {
        //달리기 중이라면
        if (playerController.isMove == true)
        {
            currentStamina--;
        }
        currentStamina = stamina;
    }

 
    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");
    
        // 게임 오버 기준
        if(currentHp <= 0)
        {
            Die();
        }

        // 회복
            // 치료제 아이템 사용
            // 가구, 침낭 아이템 사용
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died.");
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
