using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStatus
{

    public int hp; // 체력
    public int satiety; // 포만감
    public int fatigue; // 피로도
    public int attack; // 공격력
    public int stamina; // 지구력
    public int sightRange;

    public int maxHp = 100;
    public int maxSatiety = 100;
    public int maxFatique = 100;

    private float speed; // 스피드

    public void SetSpeed(float speed)
    {
        if (speed > 0) this.speed = speed;
    }
    public float GetSpeed()
    {
        return speed;
    }
    public void IncreaseHP(int hp)
    {
        this.hp += hp;
    }
    public void IncreaseSatiety(int satiety)
    {
        this.satiety += satiety;
    }
    public void IncreaseAttack(int attack)
    {
        this.attack = attack;
    }
    public void IncreaseSight(int sightRange)
    {
        this.sightRange = sightRange;
    }
}
