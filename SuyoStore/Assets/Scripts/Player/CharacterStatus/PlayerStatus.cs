using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private readonly List<Status> statModifiers;

    public PlayerController playerController;

    public int maxHp = 100; // �ִ� ü��
    public int maxSatiety = 100; // �ִ� ������
    public int maxFatique = 100; // �ִ� �Ƿε�

    public int attack = 100; // �⺻ ���ݷ�
    public int stamina = 100; // �⺻ ������
    //public int carryingBag = 30; // �⺻ ���緮

    public int currentHp = 10; // ���� ü��
    public int currentSatiety; // ���� ������
    public int currentFatigue; // ���� �Ƿε�
    public int currentAttack; // ���� ���ݷ�
    public int currentStamina; // ���� ������

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
            // ���� ����
        }
        //���� �ִϸ��̼�
        if (currentFatigue <= 0) currentAttack = 0;
        else if (currentFatigue <= 10) currentAttack -= 10;
        else if (currentFatigue <= 20) currentAttack -= 4;
        else if (currentFatigue <= 30) currentAttack -= 2;
    }

    public void StaminaModifier()
    {
        //�޸��� ���̶��
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
    
        // ���� ���� ����
        if(currentHp <= 0)
        {
            Die();
        }

        // ȸ��
            // ġ���� ������ ���
            // ����, ħ�� ������ ���
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died.");
    }
    //private float speed; // ���ǵ�

    //public void SetSpeed(float speed)
    //{
    //    if (speed > 0) this.speed = speed;
    //}
    //public float GetSpeed() {
    //    return speed;
    //}
}
