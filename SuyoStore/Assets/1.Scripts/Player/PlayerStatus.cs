using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    PlayerController playerController;

    // ���ǵ�
    public float moveSpeed = 10.0f; // �⺻ ������ �� �̵� �ӵ�
    public float runSpeed = 5.0f; // �޸��� ������ �� �̵� �ӵ�
    public float sitSpeed = 3.0f; // �ɱ� ������ �� �̵� �ӵ�

    // �������ͽ�
    public int maxHp = 100; // �ִ� ü��
    public int maxSatiety = 100; // �ִ� ������
    public int maxFatique = 100; // �ִ� �Ƿε�

    public int curHp = 10; // ���� ü��
    public int curSatiety = 50; // ���� ������
    public int curFatigue = 50; // ���� �Ƿε�

    // �ɷ�ġ
    public int maxCarryingBag = 30; // �⺻ ���緮
    public int attack = 10; // �⺻ ���ݷ�
    public int stamina = 100; // �⺻ ������

    public int curCarryingBag = 0; // �⺻ ���緮
    public int curAttack = 10; // ���� ���ݷ�
    public int curStamina = 100; // ���� ������

    // status�� ���õ� �ð�
    private int time = 100; // �ǽð�
    private int hungerTime = 10; // �������� �����ϴ� �����ð�


    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    // ���� ���°� Max�� ���� �ʰ�
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

        // ���� ���� ����
        if (curHp <= 0)
        {
            Die();
        }
    }

    public void HpRecovery()
    {
        // ȸ��
        // ġ���� ������ ���
        // ����, ħ�� ������ ���
    }

  
    /// <summary>
    /// Satiety Status
    /// </summary>
    public void SatietyModifier()
    {
        RemainStatusValue(curSatiety, maxSatiety);

        if (curSatiety <= 0)
        {
            /*���� �ð� ��*/
            Die();
        }

        // �ð��� ���� ���� -- if�� ���� ���� �ʿ�
        if(time % hungerTime == 0)
        {
            curSatiety-= 2;
        }
    }

    public void RecoverySatiety()
    {
        // ������ ���
        // �Ϸ� ��ŵ
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
            // ���� ����
        }
        //���� �ִϸ��̼�


    }

    public void SpeedModifier()
    {
        int excessBag = (int)(maxCarryingBag * 10 / 100); // 10% �ʰ���

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
            // �ȱ� ���·� ��ȯ
        }

        if (playerController.isMove == true)
        {
            //�����ð�����
            curStamina--;
        }
        else curStamina = stamina;
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
