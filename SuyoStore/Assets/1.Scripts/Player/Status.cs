using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    // Speed
    [SerializeField]
    protected float curSpeed;
    protected float walkSpeed; // �⺻ �̵� �ӵ�
    protected float runAddSpeed; // �޸��� ������ �� �߰��� �̵� �ӵ�
    protected float sitSpeed; // �ɱ� ������ �� �̵� �ӵ�

    // Max Status
    protected int maxHp; // �ִ� ü��
    protected private int maxSatiety; // �ִ� ������
    protected int maxFatigue; // �ִ� �Ƿε�

    // Current Status
    [SerializeField]
    protected int curHp; // ���� ü��
    [SerializeField]
    protected int curSatiety; // ���� ������
    [SerializeField]
    protected int curFatigue; // ���� �Ƿε�

    // Ability
    protected int maxCarryingBag; // �⺻ ���緮
    protected int attack; // �⺻ ���ݷ�
    protected int stamina; // �⺻ ������

    // Current Ability
    [SerializeField]
    protected int curCarryingBag; // �⺻ ���緮
    [SerializeField]
    protected int curAttack; // ���� ���ݷ�
    [SerializeField]
    protected int curStamina; // ���� ������

    // status�� ���õ� �ð�
    protected float time; // �ǽð�
    protected float hungerTime; // �������� �����ϴ� �����ð�
    protected float hungerDieTime; // �������� 0�� ���ӵǸ� ����ϴ� �ð�
    [SerializeField]
    protected float useHungerTime;
    [SerializeField]
    protected float useHungerDieTime;
    protected float staminaTime; // ���׹̳��� �����ϴ� �����ð�
    [SerializeField]
    protected float useStaminaTime;


    /// [Property] Speed
    public float CurSpeed { get { return curSpeed; } set { curSpeed = value; } }
    public float WalkSpeed { get { return walkSpeed; } set { walkSpeed= value; } }
    public float RunAddSpeed { get { return runAddSpeed; } set { runAddSpeed= value; } }
    public float SitSpeed { get { return sitSpeed; } set { sitSpeed = value; } }

    /// [Property] Max Status
    public int MaxHp { get { return maxHp; } set { maxHp = value; } }
    public int MaxSatiety { get { return maxSatiety; } set { maxSatiety = value; } }
    public int MaxFatigue { get { return maxFatigue; } set { maxFatigue = value; } }

    /// [Property] Current Status
    public int CurHp { get { return curHp; } set { curHp = value; } }
    public int CurSatiety { get { return curSatiety; } set { curSatiety = value; } }
    public int CurFatigue { get { return curFatigue; } set { curFatigue = value; } }

    /// [Property] Ability
    public int MaxCarryingBag { get { return maxCarryingBag; } set { maxCarryingBag = value; } }
    public int Attack { get { return Attack; } set { Attack = value; } }
    public int Stamina { get { return Stamina; } set { Stamina = value; } }

    /// [Property] Current Ability
    public int CurCarryingBag { get { return curCarryingBag; } set { curCarryingBag = value; } }
    public int CurAttack { get { return curAttack; } set { curAttack = value; } }
    public int CurStamina { get { return curStamina; } set { curStamina = value; } }

    /// [Property] Time
    public float UseHungerTime { get { return useHungerTime; } set { useHungerTime = value; } }
    public float UseHungerDieTime { get { return useHungerDieTime; } set { useHungerDieTime = value; } }
    public float UseStaminaTime { get { return useStaminaTime; } set { useStaminaTime = value; } }


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

    // ���� ���°� Max�� 0�� ���� �ʰ� --test �ʿ�
    protected int RemainStatusValue(int curVal, int maxVal)
    {
        if (curVal >= maxVal) curVal = maxVal;
        if (curVal <= 0) curVal = 0;
        return curVal;
    }

    protected float GetBackTime(float _useTime, float _time)
    {
        if (_useTime <= 0)
        {
            _useTime = _time;
            Debug.Log("[Time System] useTime is getting back");
        }

        return _useTime;
    }
}
