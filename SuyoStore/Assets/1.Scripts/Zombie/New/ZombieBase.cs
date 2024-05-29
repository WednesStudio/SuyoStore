using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZombieBase
{
    private string zombieName; //�����
    private int hp = Random.Range(50, 101); //ü��
    private int attack = Random.Range(5, 11); //����
    private int speed = Random.Range(5, 13); //�ӵ�
    private int infection; //������

    public ZombieBase(string name, int infection)
    {
        this.zombieName = name;
        this.infection = infection;
        Debug.Log(name + " ����, ������ " + infection);
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
    public NormalZombie1() : base("�Ϲ� ���� 1", 1) {}
}

public class NormalZombie2 : ZombieBase
{
    public NormalZombie2() : base("�Ϲ� ���� 2", 1) {}
}

public class NormalZombie3 : ZombieBase
{
    public NormalZombie3() : base("�Ϲ� ���� 3", 1) {}
}

public class NormalZombie4 : ZombieBase
{
    public NormalZombie4() : base("�Ϲ� ���� 4", 1) {}
}

public class GreenZombie : ZombieBase
{
    public GreenZombie() : base("�ʷ� ����", 3) {}
}

public class YellowZombie : ZombieBase
{
    public YellowZombie() : base("��� ����", 10) {}
}

public class RedZombie : ZombieBase
{
    public RedZombie() : base("���� ����", 100) {}
}