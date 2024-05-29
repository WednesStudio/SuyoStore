using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZombieBase
{
    private string zombieName; //좀비명
    private int hp = Random.Range(50, 101); //체력
    private int attack = Random.Range(5, 11); //공격
    private int speed = Random.Range(5, 13); //속도
    private int infection; //감염률

    public ZombieBase(string name, int infection)
    {
        this.zombieName = name;
        this.infection = infection;
        Debug.Log(name + " 생성, 감염률 " + infection);
    }

    public string ZombieName 
    {
        get { return zombieName; }
    }

    public int HP
    {
        get { return hp; }
        set { hp = value; }
    }

    public int Attack
    {
        get { return attack; }
        set { attack = value; }
    }

    public int Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public int Infection
    {
        get { return infection; }
    }
}

public class NormalZombie1 : ZombieBase
{
    public NormalZombie1() : base("일반 좀비 1", 1) {}
}

public class NormalZombie2 : ZombieBase
{
    public NormalZombie2() : base("일반 좀비 2", 1) {}
}

public class NormalZombie3 : ZombieBase
{
    public NormalZombie3() : base("일반 좀비 3", 1) {}
}

public class NormalZombie4 : ZombieBase
{
    public NormalZombie4() : base("일반 좀비 4", 1) {}
}

public class GreenZombie : ZombieBase
{
    public GreenZombie() : base("초록 좀비", 3) {}
}

public class YellowZombie : ZombieBase
{
    public YellowZombie() : base("노란 좀비", 10) {}
}

public class RedZombie : ZombieBase
{
    public RedZombie() : base("빨간 좀비", 100) {}
}