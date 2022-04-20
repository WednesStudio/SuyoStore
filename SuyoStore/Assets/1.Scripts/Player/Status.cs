using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public enum eCurStatusType { cHp, cSatiety, cFatigue }
    public enum eCurAbilityType { cCarryingBack, cAttack, cStamina };
    public enum eUseTimeType { UseHungerT, UseHungerDieT, UseStaminaT };

    // Speed
    [SerializeField]
    protected float curSpeed;
    protected float walkSpeed; // 기본 이동 속도
    protected float runAddSpeed; // 달리기 상태일 때 추가될 이동 속도
    protected float sitSpeed; // 앉기 상태일 때 이동 속도

    // Max Status
    protected int maxHp; // 최대 체력
    protected private int maxSatiety; // 최대 포만감
    protected int maxFatigue; // 최대 피로도

    // Current Status
    [SerializeField]
    protected int curHp; // 현재 체력
    [SerializeField]
    protected int curSatiety; // 현재 포만감
    [SerializeField]
    protected int curFatigue; // 현재 피로도

    // Ability
    protected int maxCarryingBag; // 기본 적재량
    protected int attack; // 기본 공격력
    protected int stamina; // 기본 지구력

    // Current Ability
    [SerializeField]
    protected int curCarryingBag; // 기본 적재량
    [SerializeField]
    protected int curAttack; // 현재 공격력
    [SerializeField]
    protected int curStamina; // 현재 지구력

    // status와 관련된 시간
    protected float time; // 실시간
    protected float hungerTime; // 포만감이 감소하는 일정시간(기준)
    protected float hungerDieTime; // 포만감이 0이 지속되면 사망하는 시간
    [SerializeField]
    protected float useHungerTime;
    [SerializeField]
    protected float useHungerDieTime;
    protected float staminaTime; // 스테미나가 감소하는 일정시간(기준)
    [SerializeField]
    protected float useStaminaTime;
    protected float recoveryStaminaTime;

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
    public float StaminaTime { get { return StaminaTime; } }
    public float RecoveryStaminaTime { get { return recoveryStaminaTime; } }

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
        hungerTime = 60;
        hungerDieTime = 120;
        useHungerTime = hungerTime;
        useHungerDieTime = hungerDieTime;
        staminaTime = 1;
        useStaminaTime = staminaTime;
        recoveryStaminaTime = 1;
    }

    // 현재 상태가 Max나 0을 넘지 않게 --test 필요
    public int RemainStatusValue(int _curVal, int  _maxVal)
    {
        if (_curVal >= _maxVal) _curVal = _maxVal;
        if (_curVal <= 0) _curVal = 0;
        return _curVal;
    }

    public float GetBackTime(float _useTime, float _time)
    {
        if (_useTime <= 0)
        {
            _useTime = _time;
            Debug.Log("[Time System] useTime is getting back");
        }

        return _useTime;
    }

    private List<int> itemModifiers = new List<int>();

    public void AddModifier(int modifier)
    {
        if (modifier != 0) itemModifiers.Add(modifier);
    }

    public void RemoveModifier(int modifier)
    {
        if (modifier != 0) itemModifiers.Remove(modifier);
    }

     
}
